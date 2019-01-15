using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GmcBank
{
    [DataContract]
    public class Client<TAbstractAccount, TTransaction> : IClient<TAbstractAccount, TTransaction>
        where TAbstractAccount : AbsctractAccount<TTransaction>
        where TTransaction : Transaction
    {
        [DataMember]
        public string name { get; set; }
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

        public void CreateAccount(TAbstractAccount a)
        {
            accounts.Value.Add(a.accountNumber , a);
        }

        public TAbstractAccount GetAccount(long accountNumber)
        {
            throw new NotImplementedException();
        }

      


    }
}
