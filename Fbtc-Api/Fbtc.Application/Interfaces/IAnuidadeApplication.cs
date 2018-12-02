using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Application.Interfaces
{
    public interface IAnuidadeApplication
    {
        IEnumerable<Anuidade> GetAll();

        IEnumerable<Anuidade> GetAnuidadesPendentesByPessoaId(int pessoaId);

        Anuidade GetAnuidadeById(int id);

        AnuidadeDao GetAnuidadeDaoById(int id);

        AnuidadeDao GetAnuidadeDaoByIdTipoPublicoId(int id, int tipoPublicoId);

        // IEnumerable<AnuidadeTipoPublicoDao> GetAnuidadeTipoPublicoDaoByAnuidadeIdTipoPublicoId(int id, int tipoPublicoId);

        Anuidade SetAnuidade();

        string DeleteById(int id);

        string Save(Anuidade anuidade);

        string SaveAnuidadeDao(AnuidadeDao anuidadeDao);

        IEnumerable<Anuidade> FindByFilters(int exercicio, bool? ativo);

        IEnumerable<AnuidadeTipoPublicoDao> GetAnuidadeTipoPublicoDaoByAnuidadeId(int id);

        IEnumerable<ValorAnuidade> GetValoresAnuidadesByAnuidadeTipoPublicoId(int id);

        IEnumerable<TipoPublico> GetTiposPublicosToAnuidade();
    }
}
