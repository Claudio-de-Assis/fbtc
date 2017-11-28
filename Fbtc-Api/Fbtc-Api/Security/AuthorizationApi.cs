using Microsoft.Owin.Security.OAuth;
using SimpleInjector;
using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using prmToolkit.Validation;

namespace Fbtc.Api.Security
{
    public class AuthorizationApi : OAuthAuthorizationServerProvider
    {
        private readonly Container _container;

        public AuthorizationApi(Container container)
        {
            _container = container;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

                ArgumentsValidator.RaiseExceptionOfInvalidArguments(
                    RaiseException.IfTrue(context.UserName != "Claudio", "Usuário Inválido"),
                    RaiseException.IfTrue(context.Password != "123", "Senha Invalida")
                );

                var usuarioLogado = new { id = 26, nome = "Cláudio" };

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                // Definindo o Claims:
                identity.AddClaim(new Claim("Logado", Newtonsoft.Json.JsonConvert.SerializeObject(usuarioLogado)));

                GenericPrincipal principal = new GenericPrincipal(identity, new string[] { });

                Thread.CurrentPrincipal = principal;

                context.Validated(identity);

            }
            catch (Exception ex)
            {
                context.SetError("invalid_grant", ex.Message);
                return;
            }
        }
    }
}