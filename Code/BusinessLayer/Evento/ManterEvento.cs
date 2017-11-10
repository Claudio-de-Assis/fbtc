using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    /// <summary>
    /// Evento's Business Class
    /// </summary>
    public class ManterEvento
    {
        /// <summary>
        /// Lista todos os Eventos
        /// </summary>
        /// <returns></returns>
        public IList<Evento> GetAll()
        {
            List<Evento> lstEvento = new List<Evento>();

            /*Implementação do método*/

            return lstEvento;
        }

        /// <summary>
        /// Obtém um Evento a partir do seu Id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Evento Get(int Id)
        {
            Evento evento = new Evento();

            /*Implementação do método*/

            return evento;
        }

        /// <summary>
        /// Obtém uma listagem de Eventos a partir de um filtro. São filtros da consulta:
        /// Título, Ano e Tipo do Evento.
        /// </summary>
        /// <param name="eventoFilter"></param>
        /// <returns></returns>
        public IList<Evento> Filter(Evento eventoFilter)
        {
            List<Evento> lstEvento = new List<Evento>();

            /*Implementação do método*/

            return lstEvento;
        }

        /// <summary>
        /// Salva os dados de um Evento.
        /// </summary>
        /// <param name="evento"></param>
        /// <returns></returns>
        public string Save(Evento evento)
        {
            string resultado = string.Empty;

            /*Implementação do método*/

            return resultado;
        }
    }
}
