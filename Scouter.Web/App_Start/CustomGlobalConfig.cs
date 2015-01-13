using System;
using Scouter.Web.Filters;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Validation.Providers;

namespace Scouter.Web
{
    public class CustomGlobalConfig
    {
        public static void Customize(HttpConfiguration config)
        {
            // Fixes issue with overly-aggressive validation provider:
            //     http://bit.ly/19jjC6N and http://bit.ly/130EqfT
            config.Services.RemoveAll(typeof(System.Web.Http.Validation.ModelValidatorProvider), v => v is InvalidModelValidatorProvider);

            // approach via @encosia ensures JSON is always returned
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // approach via @John_Papa at http://jpapa.me/NqC2HH
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.Filters.Add(new ValidationActionFilter());
        }
    }
}