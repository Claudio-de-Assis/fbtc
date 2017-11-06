using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{

    /// <summary>
    /// Anuidade's Business Class
    /// </summary>
    public class ManterAnuidade
    {
        /// <summary>
        /// Lista todas as Anuidades
        /// </summary>
        /// <returns></returns>
        public IList<Anuidade> GetAll()
        {
            List<Anuidade> lstAnuidade = new List<Anuidade>();

            /*Implementação do método*/

            return lstAnuidade;
        }

        /// <summary>
        /// Obtém uma Anuidade a partir do seu Id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Anuidade Get(int Id)
        {
            Anuidade anuidade = new Anuidade();

            /*Implementação do método*/

            return anuidade;
        }

        /// <summary>
        /// Obtém uma listagem de Anuidades a partir de um filtro. São filtros da consulta:
        /// Ano e Tipo do público.
        /// </summary>
        /// <param name="anuidadeFilter"></param>
        /// <returns></returns>
        public IList<Anuidade> Filter(Anuidade anuidadeFilter)
        {
            List<Anuidade> lstAnuidade = new List<Anuidade>();

            /*Implementação do método*/

            return lstAnuidade;
        }

        /// <summary>
        /// Salva os dados de uma Anuidade.
        /// </summary>
        /// <param name="anuidade"></param>
        /// <returns></returns>
        public string Save(Anuidade anuidade)
        {
            string resultado = string.Empty;

            /*Implementação do método*/

            return resultado;
        }
    }
}
