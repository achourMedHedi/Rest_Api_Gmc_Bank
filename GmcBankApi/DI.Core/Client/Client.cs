using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace GmcBank
{
    [DataContract]
    public class Client<TAbstractAccount, TTransaction> : IClient<TAbstractAccount, TTransaction>
        where TAbstractAccount : AbsctractAccount<TTransaction>
        where TTransaction : Transaction
    {
        /// <summary>
        /// name of the client
        /// </summary>
        [DataMember]
        public string name { get; set; }

        /// <summary>
        /// cin number must be 8 numbers
        /// </summary>
        [DataMember]
        public int cin { get; set; }


        [DataMember]
        public Lazy<Dictionary<long, TAbstractAccount>> accounts; 

        public Client() { }
        public Client(string n , int c)
        {
            accounts = new Lazy<Dictionary<long, TAbstractAccount>>();
            name = n;
            cin = c;
        }

         

        public IEnumerable<TAbstractAccount> GetAllAccounts ()
        {
            foreach (KeyValuePair<long , TAbstractAccount> a in accounts.Value )
            {
                yield return a.Value;
            }
        }
     
        public void CloseAccount(TAbstractAccount account)
        {
            accounts.Value[account.accountNumber].state = "Closed";
        }
        public void DeleteAccount(TAbstractAccount account)
        {
            accounts.Value.Remove(account.accountNumber);
        }

        public void CreateAccount(TAbstractAccount a)
        {
            accounts.Value.Add(a.accountNumber , a);
        }

        public TAbstractAccount GetAccount(long accountNumber)
        {
            var result = (from a in accounts.Value.Values where a.accountNumber.Equals(accountNumber) select a).FirstOrDefault();
            return result;
        }

      


    }
}
