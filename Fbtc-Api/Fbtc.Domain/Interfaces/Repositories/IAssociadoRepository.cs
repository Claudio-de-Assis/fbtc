using Fbtc.Domain.Entities;
using System.Collections.Generic;

namespace Fbtc.Domain.Interfaces.Repositories
{
    public interface IAssociadoRepository
    {
        IEnumerable<Associado> GetAll();

        Associado GetAssociadoById(int id);

        AssociadoDao GetAssociadoDaoById(int id, int anuidadeId);

        AssociadoDao GetAssociadoDaoByPessoaId(int id);

        string DeleteById(int id);

        string Insert(Associado associado);

        string Update(int id, Associado associado);

        IEnumerable<Associado> FindByFilters(string nome, string cpf, 
            string sexo, int atcId, string crp, string tipoProfissao, 
            int tipoPublico, string estado, string cidade, bool? ativo);

        /// <summary>
        /// Busco o Associado para apresentação na página de busca de associado na home FBTC
        /// </summary>
        /// <param name="nomeCidade"></param>
        /// <param name="nomeAssociado"></param>
        /// <param name="tipoPublicoId"></param>
        /// <param name="statusCertificacao"></param>
        /// <returns>AssociadoAdimplenteDao</returns>
        ResultadoConAssociadoAdimplenteDao FindAssociadoAdimplente(int pageSize, int numPage, int anuidadeReferencia, string nomeCidade, string nomeAssociado,
        int tipoPublicoId, string statusCertificacao);

        string GetNomeFotoByPessoaId(int id);

        string RessetPasswordById(int id);

        string ValidaEMail(int associadoId, string eMail);

        Associado GetAssociadoByPessoaId(int id);
    }
}
