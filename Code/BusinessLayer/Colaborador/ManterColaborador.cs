using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;

namespace BusinessLayer
{
    /// <summary>
    /// Colaborador's Business Class.
    /// </summary>
    public class ManterColaborador
    {
        /// <summary>
        /// Lista todos os Colaboradores
        /// </summary>
        /// <returns></returns>
        public IList<Colaborador> GetAll()
        {
            List<Colaborador> lstColaborador = new List<Colaborador>();

            /*Implementação do método*/

            return lstColaborador;
        }

        /// <summary>
        /// Obtém um Colaborador a partir do seu Id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Colaborador Get(int Id)
        {
            Colaborador colaborador = new Colaborador();

            /*Implementação do método*/

            return colaborador;
        }

        /// <summary>
        /// Obtém uma listagem de Colaboradores a partir de um filtro. São filtros da consulta:
        /// Nome, CPF, E-Mail, Gênero, ATC de Origem e CRP.
        /// Os filtros se complementam.
        /// </summary>
        /// <param name="colaboradorFilter"></param>
        /// <returns></returns>
        public IList<Colaborador> Filter(Colaborador colaboradorFilter)
        {
            List<Colaborador> lstColaborador = new List<Colaborador>();

            /*Implementação do método*/

            return lstColaborador;
        }

        /// <summary>
        /// Salva os dados de um Colaborador.
        /// </summary>
        /// <param name="colaborador"></param>
        /// <returns></returns>
        public string Save(Colaborador colaborador)
        {
            string resultado = string.Empty;

            /*Implementação do método*/

            return resultado;
        }

    }
}