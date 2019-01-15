using System;
using System.Runtime.Serialization;

namespace GmcBank
{
    [DataContract]
    public class Transaction
    {
        [DataMember]
        public Guid transactionNumber { get; set; }
        [DataMember]
        public long sourceAccountnNumber { get; set; }
        [DataMember]
        public long targetAccountnNumber { get; set; }
        [DataMember]
        public double amount { get; set; }
        [DataMember]
        public DateTime date { get; set; }
        [DataMember]
        public string state { get; set; }
        [DataMember]
        public string direction { get; set; }

        public Transaction() { }
        public Transaction(long source , long target , double a )
        {
            transactionNumber = Guid.NewGuid();
            sourceAccountnNumber = source;
            targetAccountnNumber = target;
            amount = a;
            date = DateTime.Now;
            state = "Ready"; 
            direction = "Incoming ";
            // transaction direction will depend on the user
        }
    }
}
