using Repositories;
using System.Linq;
using System.Web.Mvc;

namespace Website.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            using (var db = new BuildingAssociationContext())
            {
                var users = db.Users.ToList();
                var apartments = db.Apartments.ToList();
                var bills = db.Bills.ToList();
                var cons = db.Consumptions.ToList();
                var providers = db.Providers.ToList();
            }

            return View();
        }
    }
}
