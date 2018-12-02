using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Domain.Interfaces.Services
{
    public interface IAnuidadeService
    {
        IEnumerable<Anuidade> GetAll();

        IEnumerable<Anuidade> GetAnuidadesPendentesByPessoaId(int pessoaId);

        Anuidade GetAnuidadeById(int id);

        AnuidadeDao GetAnuidadeDaoById(int id);

        AnuidadeDao GetAnuidadeDaoByIdTipoPublicoId(int id, int tipoPublicoId);

        string DeleteById(int id);

        string Insert(Anuidade anuidade);

        string Update(int id, Anuidade anuidade);

        string InsertAnuidadeDao(AnuidadeDao anuidadeDao);

        string UpdateAnuidadeDao(int id, AnuidadeDao anuidadeDao);

        IEnumerable<Anuidade> FindByFilters(int exercicio, bool? ativo);

        IEnumerable<AnuidadeTipoPublicoDao> GetAnuidadeTipoPublicoDaoByAnuidadeId(int id);

   //   IEnumerable<AnuidadeTipoPublicoDao> GetAnuidadeTipoPublicoDaoByAnuidadeIdTipoPublicoId(int id, int tipoPublicoId);

        IEnumerable<ValorAnuidade> GetValoresAnuidadesByAnuidadeTipoPublicoId(int id);

        IEnumerable<TipoPublico> GetTiposPublicosToAnuidade();
    }
}
