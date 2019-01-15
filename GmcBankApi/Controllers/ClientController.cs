using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GmcBank;
using GmcBankApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GmcBankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        IBank<Client<AbsctractAccount<Transaction>, Transaction>, AbsctractAccount<Transaction>, Transaction> bank;

        public ClientController(IBank<Client<AbsctractAccount<Transaction>, Transaction>, AbsctractAccount<Transaction>, Transaction> bank)
        {
            this.bank = bank;
        }
        /// <summary>
        /// Get all clients 
        /// </summary>
        /// <returns>list of all client in the bank</returns>

        // GET : api/Client
        [HttpGet]
        public IEnumerable<Client<AbsctractAccount<Transaction>, Transaction>> GetClients()
        {
            Bank<Client<AbsctractAccount<Transaction>, Transaction>, AbsctractAccount<Transaction>, Transaction> b = bank.LoadFile(@"C:\Users\achou\source\repos\GmcBankApi\GmcBankApi\Data.json");
            foreach (Client<AbsctractAccount<Transaction>,Transaction> client in b.Clients)
            {
                yield return client;
            }
        }
        /// <summary>
        /// Get Client based on his Cin 
        /// </summary>
        /// <param name="id">Cin number</param>
        /// <returns></returns>
        // GET : api/Client/{id}
        [HttpGet("{id}")]
        public IEnumerable<Client<AbsctractAccount<Transaction>, Transaction>> GetClient(int id)
        {
            Bank<Client<AbsctractAccount<Transaction>, Transaction>, AbsctractAccount<Transaction>, Transaction> b = bank.LoadFile(@"C:\Users\achou\source\repos\GmcBankApi\GmcBankApi\Data.json");
            var result = (from i in b.Clients where i.cin.Equals(id) select i);
            return result;
        }
        /// <summary>
        /// create new client 
        /// </summary>
        /// <param name="payload">client model</param>
        /// <response code="200"> success ya m3alem</response>
        /// <response code="404"> not found mafamech</response>
        /// <response code="401"> Unauthorized access Arja3 8odwa </response>
        /// <returns></returns>
        // POST: api/Client
        [ProducesResponseType(typeof(Client<AbsctractAccount<Transaction>, Transaction>), (int)HttpStatusCode.OK)]
        [HttpPost]  
        public ActionResult Post([FromBody] ClientModel payload)
        {
            Bank<Client<AbsctractAccount<Transaction>, Transaction>, AbsctractAccount<Transaction>, Transaction> b = bank.LoadFile(@"C:\Users\achou\source\repos\GmcBankApi\GmcBankApi\Data.json");
            var client = new Client<AbsctractAccount<Transaction>, Transaction>(payload.name, payload.cin);
            b.AddClient(client);
            b.SaveFile(@"C:\Users\achou\source\repos\GmcBankApi\GmcBankApi\Data.json");
            return Ok(client);
        }
        /// <summary>
        /// update a client
        /// </summary>
        /// <param name="id">cin number </param>
        /// <param name="payload">Client model </param>
        /// <response code="200"> success ya m3alem</response>
        /// <response code="404"> not found mafamech</response>
        /// <response code="401"> Unauthorized access Arja3 8odwa </response>
        /// <returns></returns>
        // PUT: api/Client/{id}
        [ProducesResponseType(typeof(Client<AbsctractAccount<Transaction>, Transaction>), (int)HttpStatusCode.OK)]

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ClientModel payload)
        {
            Bank<Client<AbsctractAccount<Transaction>, Transaction>, AbsctractAccount<Transaction>, Transaction> b = bank.LoadFile(@"C:\Users\achou\source\repos\GmcBankApi\GmcBankApi\Data.json");
            Client<AbsctractAccount<Transaction>, Transaction> result = (from i in b.Clients where i.cin.Equals(id) select i).FirstOrDefault();
            result.cin = payload.cin;
            result.name = payload.name;
            b.SaveFile(@"C:\Users\achou\source\repos\GmcBankApi\GmcBankApi\Data.json");
            return Ok(result);
        }

        /// <summary>
        /// close a client 
        /// </summary>
        /// <param name="id">cin number</param>
        /// <response code="200"> success ya m3alem</response>
        /// <response code="404"> not found mafamech</response>
        /// <response code="401"> Unauthorized access Arja3 8odwa </response>
        // DELETE: api/ApiWithActions/5
        [ProducesResponseType(typeof(Client<AbsctractAccount<Transaction>, Transaction>), (int)HttpStatusCode.OK)]

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Bank<Client<AbsctractAccount<Transaction>, Transaction>, AbsctractAccount<Transaction>, Transaction> b = bank.LoadFile(@"C:\Users\achou\source\repos\GmcBankApi\GmcBankApi\Data.json");
            Client<AbsctractAccount<Transaction>, Transaction> result = (from i in b.Clients where i.cin.Equals(id) select i).FirstOrDefault();
            b.Clients.Remove(result); 
            b.SaveFile(@"C:\Users\achou\source\repos\GmcBankApi\GmcBankApi\Data.json");
            return Ok(result);

        }
    }
}
