using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    /// <summary>
    /// Manter Controle de Recebimento's Business Class
    /// O Controle de recebimento pode ser referente a Anuidade ou a Evento
    /// </summary>
    class ManterControleRecebimento
    {
        /// <summary>
        /// Lista todos os Pagamentos
        /// </summary>
        /// <returns></returns>
        public IList<PagamentoFBTC> GetAll()
        {
            List<PagamentoFBTC> lstPagamentoFBTC = new List<PagamentoFBTC>();

            /*Implementação do método*/

            return lstPagamentoFBTC;
        }

        /// <summary>
        /// Obtém um PagamentoFBTC a partir do seu Id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public PagamentoFBTC Get(int Id)
        {
            PagamentoFBTC pagamentoFBTC = new PagamentoFBTC();

            /*Implementação do método*/

            return pagamentoFBTC;
        }

        /// <summary>
        /// Obtém uma listagem de Pagamentos, por tipo(Anuidade ou Evento) a partir de um filtro. São filtros da consulta:
        /// Pagamento de Anuidade: Nome do associado, CPF, CRP, CRM, Ano, mês e status.
        /// Pagamento de Evento: Nome do associado, CPF, CRP, CRM, status e título do evento.
        /// Os filtros se complementam.
        /// </summary>
        /// <param name="PagamentoFBTCFilter"></param>
        /// <returns></returns>
        public IList<PagamentoFBTC> Filter(PagamentoFBTC PagamentoFBTCFilter)
        {
            List<PagamentoFBTC> lstPagamentoFBTC = new List<PagamentoFBTC>();

            /*Implementação do método*/

            return lstPagamentoFBTC;
        }

        /// <summary>
        /// Salva os dados de um PagamentoFBTC.
        /// </summary>
        /// <param name="pagamentoFBTC"></param>
        /// <returns></returns>
        public string Save(PagamentoFBTC pagamentoFBTC)
        {
            string resultado = string.Empty;

            /*Implementação do método*/

            return resultado;
        }


    }
}
