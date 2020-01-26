using Services.Contracts;
using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Website.ViewModels;
using Website.Extensions;
using Website.Helpers;
using System.Security.Claims;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Net;
using System.Net.Http.Headers;

namespace Website.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    [BasicAuthentication]
    [MyAuthorize(Roles = "Admin, User")]
    public class BillGeneratorController : ApiController
    {
        private IBillGeneratorService _service;
        private IUserService _userService;

        public BillGeneratorController(
            IBillGeneratorService service,
            IUserService userService)
        {
            _service = service;
            _userService = userService;
        }

        public HttpResponseMessage Get()
        {
            var identity = (ClaimsIdentity)User.Identity;
            //Getting the ID value
            var ID = Convert.ToInt64(identity.Claims.FirstOrDefault(c => c.Type == "loggedUserId").Value);
            var isAdmin = Convert.ToBoolean(identity.Claims.FirstOrDefault(c => c.Type == "isAdmin").Value);

            var items = new List<BillGeneratorViewModel>();

            if(isAdmin)
            {
                var all = _service.GetAll().Select(x => x.ToViewModel());
                items.AddRange(all);
            } else
            {
                var user = _userService.Get(ID);
                var fromUserMansion = _service.GetAll().Where(x => x.MansionId == user.MansionId).Select(x => x.ToViewModel());
                items.AddRange(fromUserMansion);
            }

            return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, items);
        }

        // GET api/billgenerator/5
        public HttpResponseMessage Get(long id)
        {
            var item = _service.Get(id);
            string name = item.Date.ToString("MMMM-yyyy") + ".csv";

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(item.CSV);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = name };
            return response;
        }

        public HttpResponseMessage Post([FromBody]BillGeneratorViewModel item)
        {
            try
            {
                int month = DateTime.Now.Month == 1 ? 12 : DateTime.Now.Month - 1;
                _service.Generate(item.MansionId.Value, month,  DateTime.UtcNow.Year - 1);

                return Request.CreateResponse(System.Net.HttpStatusCode.Accepted);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // DELETE api/billgenerator/5
        public HttpResponseMessage Delete(long id)
        {
            try
            {
                _service.Delete(id);
                return Request.CreateResponse(System.Net.HttpStatusCode.Accepted);
            }
            catch
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
