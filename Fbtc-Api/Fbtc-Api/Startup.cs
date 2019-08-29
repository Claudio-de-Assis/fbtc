using Fbtc.Api.Security;
using Fbtc.Infra.Helpers;
using Fbtc.Ioc;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System;
using System.Web.Http;

namespace Fbtc.Api
{
    public class Startup
    {
        private readonly int _sessionDurationMinutes;

        public Startup()
        {
            _sessionDurationMinutes = Int32.Parse(ConfigHelper.GetKeyAppSetting("SessionDurationMinutes"));
        }

        public void Configuration(IAppBuilder app)
        {
            // Classe responsável por configurar a WebApi:
            HttpConfiguration config = new HttpConfiguration();

            // Container da inversão de controle:
            var container = new Container();

            // Configura a inversão de controle:
            ConfigureDependencyInjection(config, container);

            ConfigureWebApi(config);
            ConfigureOAuth(app, container);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            app.UseWebApi(config);
        }

        private void ConfigureOAuth(IAppBuilder app, Container container)
        {
            OAuthAuthorizationServerOptions oAuthServerOption = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(_sessionDurationMinutes),
                Provider = new AuthorizationApi(container)
            };

            app.UseOAuthAuthorizationServer(oAuthServerOption);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        private void ConfigureWebApi(HttpConfiguration config)
        {
            // Remove o suporte a XML:
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            var jsonSettings = config.Formatters.JsonFormatter.SerializerSettings;
            jsonSettings.Formatting = Formatting.Indented;
            jsonSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
            jsonSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            config.MapHttpAttributeRoutes();
        }

        private void ConfigureDependencyInjection(HttpConfiguration config, Container container)
        {
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
            BootStrapper.RegistrerWebApi(container);
            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}