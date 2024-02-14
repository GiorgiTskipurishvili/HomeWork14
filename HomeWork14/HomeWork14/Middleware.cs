using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;

namespace HomeWork14
{
    public class Middleware :Attribute, IAuthorizationFilter
    {
        private IConfiguration _configuration;

        public Middleware(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var appSwitch = _configuration.GetValue<bool>("BookingNotAllowed") ;
            if (appSwitch)
            {
                context.Result = new ViewResult { ViewName = "BookingNotAllowed" };
            }
        }
    }


}
