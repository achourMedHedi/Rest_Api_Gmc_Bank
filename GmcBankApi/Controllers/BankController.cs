using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GmcBank;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GmcBankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        IBank<Client<AbsctractAccount<Transaction>, Transaction>, AbsctractAccount<Transaction>, Transaction> bank;

        public BankController(IBank<Client<AbsctractAccount<Transaction>, Transaction>, AbsctractAccount<Transaction>, Transaction> bank)
        {
            this.bank = bank;
        }
        /// <summary>
        /// Get Full information about the bank
        /// </summary>
        /// <response code="200"> success ya m3alem</response>
        /// <response code="404"> file not found</response>
        /// <response code="400"> file empty</response>
        /// <returns></returns>
        // GET: api/Bank
        [ProducesResponseType(typeof(Bank<Client<AbsctractAccount<Transaction>, Transaction>, AbsctractAccount<Transaction>, Transaction>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        public ActionResult GetAll()
        {
            Bank<Client<AbsctractAccount<Transaction>, Transaction>, AbsctractAccount<Transaction>, Transaction> b;
            try
            {
                b = bank.LoadFile(@"C:\Users\achou\source\repos\GmcBankApi\GmcBankApi\Data.json");
                if (b.name == null)
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return NotFound(e);

            }
           
            var greeter = new TestNeo4j("bolt://localhost:7687", "neo4j", "Aze123qsd456");
            
            //string result = greeter.PrintGreeting();
            

            return Ok(greeter.PrintGreeting());
        }

       
        /// <summary>
        /// Create new bank 
        /// </summary>
        /// <param name="payLoad">bank model</param>
        /// <returns></returns>

        // POST: api/Bank/newBank
        [HttpPost("newBank")]
        public ActionResult Create([FromBody] BankModel payLoad)
        {
            var bank = new Bank<Client<AbsctractAccount<Transaction>, Transaction>, AbsctractAccount<Transaction>, Transaction>(payLoad.name, payLoad.swiftCode);
            bank.SaveFile(@"C:\Users\achou\source\repos\GmcBankApi\GmcBankApi\Data.json");

            return Ok(bank);
        }

        /// <summary>
        /// add one agent
        /// </summary>
        /// <returns></returns>
        [HttpPost("addAgent")]
        public ActionResult addOneAgent()
        {
            Bank<Client<AbsctractAccount<Transaction>, Transaction>, AbsctractAccount<Transaction>, Transaction> b;
            try
            {
                b = bank.LoadFile(@"C:\Users\achou\source\repos\GmcBankApi\GmcBankApi\Data.json");
                if (b.name == null)
                {
                    return Content("please Create a new bank");
                }
                b.AddAgent();
                b.SaveFile(@"C:\Users\achou\source\repos\GmcBankApi\GmcBankApi\Data.json");

            }
            catch (Exception e)
            {
                return Content("file not found " + e);

            }

            return Ok(b);
        }

        /// <summary>
        /// add number of agents 
        /// </summary>
        /// <param name="number">number of agents</param>
        /// <returns></returns>
        [HttpPost("addAgent/{number}")]
        public ActionResult addManyAgent(int number )
        {
            Bank<Client<AbsctractAccount<Transaction>, Transaction>, AbsctractAccount<Transaction>, Transaction> b;
            try
            {
                b = bank.LoadFile(@"C:\Users\achou\source\repos\GmcBankApi\GmcBankApi\Data.json");
                if (b.name == null)
                {
                    return Content("please Create a new bank");
                }
                b.AddAgent(number);
                b.SaveFile(@"C:\Users\achou\source\repos\GmcBankApi\GmcBankApi\Data.json");

            }
            catch (Exception e)
            {
                return Content("file not found " + e);

            }

            return Ok(b);
        }

        /// <summary>
        /// edit Bank Name / swiftCode
        /// </summary>
        /// <param name="payLoad">Bank model </param>
        /// <returns></returns>
        // PUT: api/Bank/
        [HttpPut]
        public ActionResult Put([FromBody] BankModel payLoad)
        {
            var b = bank.LoadFile(@"C:\Users\achou\source\repos\GmcBankApi\GmcBankApi\Data.json");
            b.name = payLoad.name;
            b.swiftCode = payLoad.swiftCode;
            b.SaveFile(@"C:\Users\achou\source\repos\GmcBankApi\GmcBankApi\Data.json");
            return Ok(b);
        }

        /// <summary>
        /// remove number of agents 
        /// </summary>
        /// <param name="id">number of agents</param>
        // DELETE: api/ApiWithActions/5
        [HttpDelete("removeAgent/{number}")]
        public ActionResult DeleteAgent(int number)
        {
            Bank<Client<AbsctractAccount<Transaction>, Transaction>, AbsctractAccount<Transaction>, Transaction> b;
            try
            {
                b = bank.LoadFile(@"C:\Users\achou\source\repos\GmcBankApi\GmcBankApi\Data.json");
                
                if (number == 1)
                {
                    b.RemoveAgent();

                }
                else
                {
                    b.RemoveAgent(number);
                }
                b.SaveFile(@"C:\Users\achou\source\repos\GmcBankApi\GmcBankApi\Data.json");

            }
            catch (Exception e)
            {
                return Content("file not found " + e);

            }
            return Ok(b);
        }
    }
}
