using Microsoft.Owin.Security.OAuth;
using SimpleInjector;
using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using prmToolkit.Validation;
using Fbtc.Infra.Helpers;
using Fbtc.Domain.Entities;
using Fbtc.Application.Services;
using Fbtc.Domain.Services;
using Fbtc.Infra.Persistencia.AdoNet;

namespace Fbtc.Api.Security
{
    public class AuthorizationApi : OAuthAuthorizationServerProvider
    {
        private readonly Container _container;
        private readonly string _appUserFBTC;
        private readonly string _appPassword;
        private readonly UserProfileApplication _userProfileApplication;

        public AuthorizationApi(Container container)
        {
            _container = container;
            _userProfileApplication = new UserProfileApplication(new UserProfileService(new UserProfileRepository()));

            // Dados do usuário da fbtc.org:
            _appUserFBTC = ConfigHelper.GetKeyAppSetting("AppUserFBTC");
            _appPassword = ConfigHelper.GetKeyAppSetting("AppPassword");
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

                var _usuarioLogado = new { id = 0, nome = "" };
                
                if (context.UserName.Equals(_appUserFBTC))
                {
                    //Obter o token com o usuário de consulta do site da fbtc.org.br:
                    ArgumentsValidator.RaiseExceptionOfInvalidArguments(
                        RaiseException.IfTrue(context.Password != _appPassword, "Senha Inválida")
                    );

                    _usuarioLogado = new { id = 0, nome = "fbtc.org.br" };
                }
                else
                {
                    //Obter o Token de acesso com o login e a senha do usuário que está se logando no sistema:
                    if (string.IsNullOrWhiteSpace(context.UserName)) throw new InvalidOperationException("email não informado!");
                    if (string.IsNullOrWhiteSpace(context.Password)) throw new InvalidOperationException("password não informada!");

                    UserProfileLogin _userProfileLogin = new UserProfileLogin()
                    {
                        EMail = context.UserName,
                        PasswordHash = context.Password
                    };

                    var usuarioLogado = _userProfileApplication.LoginUser(_userProfileLogin);

                    ArgumentsValidator.RaiseExceptionOfInvalidArguments(
                        RaiseException.IfNull(usuarioLogado, "Usuário ou Senha inválidos")
                    );

                    _usuarioLogado = new { id = usuarioLogado.PessoaId, nome = usuarioLogado.Nome };
                }

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                // Definindo o Claims:
                identity.AddClaim(new Claim("Logado", Newtonsoft.Json.JsonConvert.SerializeObject(_usuarioLogado)));

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