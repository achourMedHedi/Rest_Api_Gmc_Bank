using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace GmcBank
{
    [DataContract]
    [KnownType(typeof(Business))]
    [KnownType(typeof(Saving))]
    abstract public class AbsctractAccount<TTransaction> : IComparable , IAbsctractAccount<TTransaction>
        where TTransaction : Transaction 

    {
        [DataMember]
        public double balance { get; set; }
        [DataMember]
        public long accountNumber { get; set; }
        [DataMember]
        public string owner { get; set; }
        [DataMember]
        public DateTime creationDate { get; set; }
        [DataMember]
        public string state { get; set; }
        [DataMember]
        public virtual double TaxRatio { get; set; }

        [DataMember]
        private Lazy<Dictionary<Guid, TTransaction>> transactions;
        
        public AbsctractAccount() { } 
        public AbsctractAccount(long accNumber , Client<AbsctractAccount<TTransaction> , TTransaction> client)
        {
            balance = 1000;
            accountNumber = accNumber;
            owner = client.name;
            transactions = new Lazy<Dictionary<Guid, TTransaction>>();
            creationDate = DateTime.Now;
            state = "Active";
        }

     

        public IEnumerable<TTransaction> GetAllTransactions ()
        {
            foreach (KeyValuePair<Guid , TTransaction> t in transactions.Value)
            {
                yield return t.Value; 
            }
        }

        public void AddTransaction (TTransaction t)
        {
            transactions.Value.Add(t.transactionNumber, t);
        }

        public abstract void Debit(double amount);  

        public void Credit(double amount)
        {
            balance += amount;
        }

        public virtual void SendMoney(double amount ,long targetaccountNumber)
        {

        }

        public IEnumerable<TTransaction> GetTransactionsByDate (string dateTime)
        {
            var result = from i in transactions.Value where i.Value.date.Equals(DateTime.Parse(dateTime)) select i;
            foreach (KeyValuePair<Guid , TTransaction> transaction in result)
            {
                yield return transaction.Value;
            } 
        }
        public IEnumerable<TTransaction> GetTransactionsByTarget(long accountNumber)
        {
            var result = from i in transactions.Value where i.Value.targetAccountnNumber == accountNumber orderby i.Value.date select i;
            foreach (KeyValuePair<Guid, TTransaction> transaction in result)
            {
                yield return transaction.Value;
            }
        }
       
        /// <summary>
        /// Dynamic linq :/
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public Dictionary<Guid, TTransaction> GetTransactionsByQuery (string dynamicLink)
        {

            return new Dictionary<Guid, TTransaction>();
        }

        public int CompareTo(object obj)
        {
            return balance.CompareTo(obj);
        }
        

    }
}
