using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using EntityLayer;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fbtc_web.Controllers
{
    [Route("api/[controller]")]
    public class AssociadoController : Controller
    {
        private ManterAssociado manterAssociado = new ManterAssociado();

        // GET: api/values
        [HttpGet]
        public Associado Get(int Id)
        {
            return manterAssociado.Get(Id);
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Associado> GetAll()
        {
            return manterAssociado.GetAll();
        }



    }
}
