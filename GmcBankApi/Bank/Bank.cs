using GmcBank.CustomAttribute;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;

namespace GmcBank
{
    [DataContract]

    public class Bank<TClient, TAbsctractAccount, TTransaction> : IEquatable<TClient> , IBank<TClient, TAbsctractAccount, TTransaction>
        where TClient : Client<TAbsctractAccount, TTransaction>
        where TAbsctractAccount : AbsctractAccount<TTransaction> 
        where TTransaction : Transaction
    {
        [Required]
        [DataMember]
        public string name { get; set; }

        [Auther(name = "achour")]
        [Required]
        [DataMember]
        public int swiftCode { get; set; }

        [DataMember]
        public int agent { get; set; }
        [DataMember]
        private List<TClient> clients;

        public  Queue transactionsQueue = new Queue();

        //public HashSet<AbsctractAccount> accounts;
        //public Hashtable transactions;

        public Bank() { }
        public Bank (string n , int sCode)
        {
            agent = 1;
            name = n;
            swiftCode = sCode;
            clients = new List<TClient>();
        }

        public List<TClient> Clients
        {
            get
            {
                return clients;
            }
        }
      

        /// <summary>
        /// add transaction to the Queue 
        /// and test if there an agents to execute the transaction 
        /// send money from sender and credit the receiver
        /// </summary>
        public void AddTransaction(TTransaction transaction)
        {
            // add transaction to the queue 
            // if theres an agent 
            //transactionsQueue.Enqueue(transaction);
            agent--;

            if (agent > 0)
            {

                // lock 
                // sender account
                // receiver account 
                // sender.sendMoney
                // receiver.credit 
                lock (transactionsQueue)
                {
                    transactionsQueue.Enqueue(transaction);
                    Console.WriteLine("queue");
                    // with thread :/
                    TAbsctractAccount sender = account(transaction.sourceAccountnNumber);
                    TAbsctractAccount receiver = account(transaction.targetAccountnNumber);
                    try
                    {
                        Console.WriteLine("d5alt");
                        Thread.Sleep(3000);
                        Console.WriteLine("lde5el");
                        sender.SendMoney(transaction.amount, transaction.targetAccountnNumber);
                        receiver.Credit(transaction.amount);
                        receiver.AddTransaction(transaction);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    transactionsQueue.Dequeue();
                    Console.WriteLine("dequeue");
                }
        
            }
            else
            {
                Console.WriteLine("no agents ");
            }
            agent++;



        }


        public bool Equals(TClient other)
        {
            throw new NotImplementedException();
        }

        //public Hashtable Transactions () { return new Hashtable(); }
        public TAbsctractAccount account (long accNumber)
        {
            foreach (TClient client in clients)
            {
                foreach (TAbsctractAccount a in client.GetAllAccounts())
                {
                    
                    if (a.accountNumber == accNumber)
                    {
                        return a;
                    }
                }
            }
            return null;
        }
        public void AddAgent() { agent++; }
        public void AddAgent(int nbAgents) { agent += nbAgents; }
        public void RemoveAgent() { agent -= 1; }
        public void RemoveAgent(int nbAgents) { agent -= nbAgents; }
        //load file
        [Auther(name = "achour")]
        public Bank<TClient, TAbsctractAccount, TTransaction> LoadFile(string path)
        {
            string filePath = File.ReadAllText(path);
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(filePath));
            DataContractJsonSerializer serRead = new DataContractJsonSerializer(typeof(Bank<TClient, TAbsctractAccount, TTransaction>));

            stream.Position = 0;
            Bank<TClient, TAbsctractAccount, TTransaction> bank = (Bank<TClient, TAbsctractAccount, TTransaction>)serRead.ReadObject(stream);
            return bank; 
            
        }
        //save file
        [Auther(name = "achour")]
        public void SaveFile(string path=@"C:\Users\achou\source\repos\GmcBank\GmcBank\data.json")
        {
            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Bank<TClient, TAbsctractAccount, TTransaction>)) ;
            ser.WriteObject(stream, this);

            stream.Position = 0;
            StreamReader seralize = new StreamReader(stream);
            File.WriteAllText(path, seralize.ReadToEnd());


        }
       
    }
}
