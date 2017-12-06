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
            container.Register<IAtcApplication, AtcApplication>(Lifestyle.Scoped);
            container.Register<ITipoPublicoApplication, TipoPublicoApplication>(Lifestyle.Scoped);
            container.Register<IColaboradorApplication, ColaboradorApplication>(Lifestyle.Scoped);
            container.Register<IEventoApplication, EventoApplication>(Lifestyle.Scoped);
            container.Register<IEnderecoApplication, EnderecoApplication>(Lifestyle.Scoped);
            container.Register<IRecebimentoApplication, RecebimentoApplication>(Lifestyle.Scoped);
            container.Register<IIsencaoApplication, IsencaoApplication>(Lifestyle.Scoped);

            // Servicços de domínio:
            container.Register<IAssociadoService, AssociadoService>(Lifestyle.Scoped);
            container.Register<IAtcService, AtcService>(Lifestyle.Scoped);
            container.Register<ITipoPublicoService, TipoPublicoService>(Lifestyle.Scoped);
            container.Register<IColaboradorService, ColaboradorService>(Lifestyle.Scoped);
            container.Register<IEventoService, EventoService>(Lifestyle.Scoped);
            container.Register<IEnderecoService, EnderecoService>(Lifestyle.Scoped);
            container.Register<IRecebimentoService, RecebimentoService>(Lifestyle.Scoped);
            container.Register<IIsencaoService, IsencaoService>(Lifestyle.Scoped);

            // Infra Repositórios:
            container.Register<IAssociadoRepository, AssociadoRepository>(Lifestyle.Scoped);
            container.Register<IAtcRepository, AtcRepository>(Lifestyle.Scoped);
            container.Register<ITipoPublicoRepository, TipoPublicoRepository>(Lifestyle.Scoped);
            container.Register<IColaboradorRepository, ColaboradorRepository>(Lifestyle.Scoped);
            container.Register<IEventoRepository, EventoRepository>(Lifestyle.Scoped);
            container.Register<IEnderecoRepository, EnderecoRepository>(Lifestyle.Scoped);
            container.Register<IRecebimentoRepository, RecebimentoRepository>(Lifestyle.Scoped);
            container.Register<IIsencaoRepository, IsencaoRepository>(Lifestyle.Scoped);
        }
    }
}
