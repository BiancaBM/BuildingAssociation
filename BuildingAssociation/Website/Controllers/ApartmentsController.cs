using Repositories.Entities;
using Services.Contracts;
using System;
using System.Net.Http;
using System.Web.Http;

namespace Website.Controllers
{
    public class ApartmentsController : BaseController<Apartment>
    {
        private IApartmentService _apartmentService;

        public ApartmentsController(IApartmentService apartmentService)
            : base(apartmentService)
        {
            _apartmentService = apartmentService;
        }
    }
}