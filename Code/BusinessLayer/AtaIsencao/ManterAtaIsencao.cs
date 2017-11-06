using EntityLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BusinessLayer
{
    /// <summary>
    /// Ata Isençao's Business Class
    /// A Ata de Isenção pode ser para um Evento ou para uma Anuidade.
    /// </summary>
    class ManterAtaIsencao
    {
        /// <summary>
        /// Lista todas a Atas de Isenções
        /// </summary>
        /// <returns>lstAtaIsencao</returns>
        public IList<AtaIsencao> GetAll()
        {
            List<AtaIsencao> lstAtaIsencao = new List<AtaIsencao>();

            /*Implementação do método*/

            return lstAtaIsencao;
        }

        /// <summary>
        /// Obtém uma Ata de Isenção a partir do seu Id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>ataIsencao</returns>
        public AtaIsencao Get(int Id) {

            AtaIsencao ataIsencao = new AtaIsencao();

            /*Implementação do método*/

            return ataIsencao;
        }

        /// <summary>
        /// Obtém uma listagem de Atas de Isenções, por Tipo de Ata, a partir de um filtro. São filtros da consulta:
        /// ATA DE ANUIDADE: Nome do associado, Identificação da Ata, Código da Anuidade.
        /// ATA DE EVENTO: Nome do associado, Identificação da Ata, Ano da Isenção, Título do evento.
        /// </summary>
        /// <param name="ataIsencao"></param>
        /// <returns>lstAtaIsencao</returns>
        public IList<AtaIsencao> Filter(AtaIsencao ataIsencao)
        {
            List<AtaIsencao> lstAtaIsencao = new List<AtaIsencao>();

            /*Implementação do método*/

            return lstAtaIsencao;
        }

        /// <summary>
        /// Salva os dados de uma Ata de Isenção
        /// </summary>
        /// <param name="ataIsencao"></param>
        /// <returns>resultado</returns>
        public string Save(AtaIsencao ataIsencao)
        {
            string resultado = string.Empty;

            /*Implementação do método*/

            return resultado;
        }

    }
}