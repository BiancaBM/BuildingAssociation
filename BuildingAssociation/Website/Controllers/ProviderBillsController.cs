using Repositories.Entities;
using Services.Contracts;
using System;
using System.Net.Http;
using System.Web.Http;

namespace Website.Controllers
{
    public class ProviderBillsController : BaseController<ProviderBill>
    {
        private IProviderBillService _providerBillService;

        public ProviderBillsController(IProviderBillService providerBillService)
            : base(providerBillService)
        {
            _providerBillService = providerBillService;
        }
    }
}