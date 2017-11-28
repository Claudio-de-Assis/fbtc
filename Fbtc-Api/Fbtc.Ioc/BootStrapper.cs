using Fbtc.Application.Interfaces;
using Fbtc.Application.Services;
using Fbtc.Domain.Interfaces.Repositories;
using Fbtc.Domain.Interfaces.Services;
using Fbtc.Domain.Services;
using Fbtc.Infra.Persistencia.AdoNet;
using SimpleInjector;

namespace Fbtc.Ioc
{
    public class BootStrapper
    {
        // Lifestyle.Transient => Um instancia para cada transação;
        // Lifestyle.Singleton => Um instância única da classe;
        // Lifestyle.Scoped => Um instancia única para cada request;

        public static void RegistrerWebApi(Container container)
        {
            // Servicços de aplicação:
            container.Register<IAssociadoApplication, AssociadoApplication > (Lifestyle.Scoped);

            // Servicços de domínio:
            container.Register<IAssociadoService, AssociadoService>(Lifestyle.Scoped);

            // Infra Repositórios:
            container.Register<IAssociadoRepository, AssociadoRepository>(Lifestyle.Scoped);
        }
    }
}
