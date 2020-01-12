using Repositories.Entities;
using Services.Contracts;
using System;
using System.Net.Http;
using System.Web.Http;

namespace Website.Controllers
{
    public class ProvidersController : BaseController<Provider>
    {
        private IProviderService _providerService;

        public ProvidersController(IProviderService providerService)
            : base(providerService)
        {
            _providerService = providerService;
        }
    }
}
