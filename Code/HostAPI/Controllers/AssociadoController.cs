using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HostAPI.Controllers
{
    public class AssociadoController : ApiController
    {
        // GET: api/Associado
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Associado/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Associado
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Associado/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Associado/5
        public void Delete(int id)
        {
        }
    }
}
