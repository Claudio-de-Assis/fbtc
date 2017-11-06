using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;

namespace BusinessLayer
{
    public class ManterAssociado
    {
        public IList<Associado> GetAll()
        {
            List<Associado> lstAssociado = new List<Associado>();

            return lstAssociado;
        }


        public Associado Get(int Id)
        {
            Associado associado = new Associado();

            return associado;
        }

        public IList<Associado> Filter(Associado associadoFilter)
        {
            List<Associado> lstAssociado = new List<Associado>();

            return lstAssociado;
        }
    }
}
