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

        /// <summary>
        /// Get one account
        /// </summary>
        /// <returns></returns>
        // GET: api/Account/{id}
        [HttpGet("{id}")]
        public IEnumerable<AbsctractAccount<Transaction>> GetAccount(long id)
        {
            Bank<Client<AbsctractAccount<Transaction>, Transaction>, AbsctractAccount<Transaction>, Transaction> b = bank.LoadFile(@"C:\Users\achou\source\repos\GmcBankApi\GmcBankApi\Data.json");
            foreach (Client<AbsctractAccount<Transaction>, Transaction> client in b.Clients)
            {
                yield return client.GetAccount(id);
            }
        }

        
        /// <summary>
        /// create new account
        /// </summary>
        /// <param name="payload">accountModel</param>
        /// <returns></returns>
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
        public ActionResult Put(int id, [FromBody] AccountModel payload)
        {
            AbsctractAccount<Transaction> result = null;
            Bank<Client<AbsctractAccount<Transaction>, Transaction>, AbsctractAccount<Transaction>, Transaction> b = bank.LoadFile(@"C:\Users\achou\source\repos\GmcBankApi\GmcBankApi\Data.json");
            foreach (Client<AbsctractAccount<Transaction>, Transaction> client in b.Clients)
            {
                result =  client.GetAccount(id);
                if (result != null)
                {
                    result.accountNumber = payload.accountNumber;
                    result.TaxRatio = payload.type == "business" ? 0.01 : 0.1;
                }
            }
            b.SaveFile(@"C:\Users\achou\source\repos\GmcBankApi\GmcBankApi\Data.json");
            return Ok(result);
        }
    

        // DELETE: api/account/id
        [HttpPut("close/{id}")]
        public ActionResult Close(long id)
        {
            bool deleted = false;

            Bank<Client<AbsctractAccount<Transaction>, Transaction>, AbsctractAccount<Transaction>, Transaction> b = bank.LoadFile(@"C:\Users\achou\source\repos\GmcBankApi\GmcBankApi\Data.json");
            foreach (Client<AbsctractAccount<Transaction>, Transaction> client in b.Clients)
            {
                AbsctractAccount<Transaction> a = (from account in client.GetAllAccounts() where account.accountNumber == id select account).FirstOrDefault();
                try
                {
                    client.CloseAccount(a);
                    deleted = true;

                }
                catch (Exception e)
                {
                    continue;
                }


            }
            b.SaveFile(@"C:\Users\achou\source\repos\GmcBankApi\GmcBankApi\Data.json");
            if (deleted == false)
            {
                return BadRequest();
            }

            return Ok();
        }

        // DELETE: api/account/id
        [HttpDelete("delete/{id}")]
        public ActionResult Delete(long id)
        {
            bool deleted = false;

            Bank<Client<AbsctractAccount<Transaction>, Transaction>, AbsctractAccount<Transaction>, Transaction> b = bank.LoadFile(@"C:\Users\achou\source\repos\GmcBankApi\GmcBankApi\Data.json");
            foreach (Client<AbsctractAccount<Transaction>, Transaction> client in b.Clients)
            {
                AbsctractAccount<Transaction> a = (from account in client.GetAllAccounts() where account.accountNumber == id select account).FirstOrDefault();
                try
                {
                    client.DeleteAccount(a);
                    deleted = true;

                }
                catch (Exception e)
                {
                    continue;
                }


            }
            b.SaveFile(@"C:\Users\achou\source\repos\GmcBankApi\GmcBankApi\Data.json");
            if (deleted == false)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
