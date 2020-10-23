using Papara;
using System;
using System.Configuration;
using System.Web.Http;
using System.Web.Http.Results;

namespace PaparaNetFrameworkSamples.Controllers
{
    public class AccountController : ApiController
    {
        private readonly PaparaClient paparaClient;

        public AccountController()
        {
            paparaClient = new PaparaClient(ConfigurationManager.AppSettings["Papara:ApiKey"], PaparaEnv.Test);
        }

        public JsonResult<Account> GetAccount()
        {
            var accountResult = this.paparaClient.AccountService.GetAccount();

            if (!accountResult.Succeeded)
            {
                throw new Exception(accountResult.Error.Message);
            }

            return Json(accountResult.Data);
        }
    }
}
