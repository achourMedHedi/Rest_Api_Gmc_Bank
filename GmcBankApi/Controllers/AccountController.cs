using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GmcBank;
using GmcBankApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GmcBankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        IBank<Client<AbsctractAccount<Transaction>, Transaction>, AbsctractAccount<Transaction>, Transaction> bank;

        public AccountController(IBank<Client<AbsctractAccount<Transaction>, Transaction>, AbsctractAccount<Transaction>, Transaction> bank)
        {
            this.bank = bank;
        }

        /// <summary>
        /// Get All accounts 
        /// </summary>
        /// <returns></returns>
        // GET: api/Account
        [HttpGet]
        public IEnumerable<AbsctractAccount<Transaction>> GetAccounts()
        {
            Bank<Client<AbsctractAccount<Transaction>, Transaction>, AbsctractAccount<Transaction>, Transaction> b = bank.LoadFile(@"C:\Users\achou\source\repos\GmcBankApi\GmcBankApi\Data.json");
            foreach (Client<AbsctractAccount<Transaction>, Transaction> client in b.Clients)
            {
                foreach (AbsctractAccount<Transaction> account in client.GetAllAccounts())
                {
                    yield return account;
                }
            }
        }

        
        // GET: api/Account/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Account
        [HttpPost]
        public ActionResult Post([FromBody] AccountModel payload)
        {
            Bank<Client<AbsctractAccount<Transaction>, Transaction>, AbsctractAccount<Transaction>, Transaction> b = bank.LoadFile(@"C:\Users\achou\source\repos\GmcBankApi\GmcBankApi\Data.json");
            Client<AbsctractAccount<Transaction>, Transaction> client = b.GetClient(payload.ownerCin);
            AbsctractAccount<Transaction> account;
            if (payload.type.ToLower() == "business" )
            {
                account = new Business(payload.accountNumber, client); 
            }
            else if (payload.type.ToLower() == "saving")
            {
                account = new Saving(payload.accountNumber, client);
            }
            else
            {
                return BadRequest();
            }

            try
            {
                client.CreateAccount(account);
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
            b.SaveFile(@"C:\Users\achou\source\repos\GmcBankApi\GmcBankApi\Data.json");
            return Ok(account);
        }

        // PUT: api/Account/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
