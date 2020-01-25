using Services.Contracts;
using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;
using Website.Extensions;
using Website.Helpers;
using Website.ViewModels;

namespace Website.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    [BasicAuthentication]
    public class WaterConsumptionsController : ApiController
    {
        private IWaterConsumptionService _waterConsumptionService;

        public WaterConsumptionsController(IWaterConsumptionService service)
        {
            _waterConsumptionService = service;
        }

        [MyAuthorize(Roles = "Admin, User")]
        // GET api/waterconsumptions
        public HttpResponseMessage Get()
        {
            var identity = (ClaimsIdentity)User.Identity;
            //Getting the ID value
            var ID = Convert.ToInt64(identity.Claims.FirstOrDefault(c => c.Type == "loggedUserId").Value);
            var isAdmin = Convert.ToBoolean(identity.Claims.FirstOrDefault(c => c.Type == "isAdmin").Value);

            var items = _waterConsumptionService.GetAll();

            if(!isAdmin)
            {
                items = items.Where(x => x.UserId == ID);
            }

            var result = items.OrderByDescending(x => x.CreationDate).Select(x => x.ToViewModel());

            return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, result);
        }

        [MyAuthorize(Roles = "Admin")]
        // GET api/waterconsumptions/5
        public HttpResponseMessage Get(long id)
        {
            var item = _waterConsumptionService.Get(id).ToViewModel();
            return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, item);
        }

        [MyAuthorize(Roles = "Admin, User")]
        public HttpResponseMessage Post([FromBody]WaterConsumptionViewModel item)
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                //Getting the ID value
                var ID = Convert.ToInt64(identity.Claims.FirstOrDefault(c => c.Type == "loggedUserId").Value);
                var isAdmin = Convert.ToBoolean(identity.Claims.FirstOrDefault(c => c.Type == "isAdmin").Value);

                var entity = item.FromViewModel();
                if(!isAdmin) entity.UserId = ID;

                if (entity.UniqueId.HasValue)
                {
                    _waterConsumptionService.Update(entity);
                }
                else
                {
                    _waterConsumptionService.Insert(entity);
                }

                return Request.CreateResponse(System.Net.HttpStatusCode.Accepted);
            }
            catch (Exception)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        [MyAuthorize(Roles = "Admin")]
        // DELETE api/waterconsumptions/5
        public HttpResponseMessage Delete(long id)
        {
            try
            {
                _waterConsumptionService.Delete(id);
                return Request.CreateResponse(System.Net.HttpStatusCode.Accepted);
            }
            catch
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}