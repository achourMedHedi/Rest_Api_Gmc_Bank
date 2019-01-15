using GmcBank.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GmcBank
{
    public static class BankExtensions
    {
        public static void AddClient(this Bank<Client<AbsctractAccount<Transaction>, Transaction>, AbsctractAccount<Transaction>, Transaction> bank, Client<AbsctractAccount<Transaction>, Transaction> client )
        {
            bank.Clients.Add(client);
        }
        public static Client<AbsctractAccount<Transaction>, Transaction> GetClient(this Bank<Client<AbsctractAccount<Transaction>, Transaction>, AbsctractAccount<Transaction>, Transaction> bank, long nbClient)
        {
            Client<AbsctractAccount<Transaction>, Transaction> client = (from c in bank.Clients where c.cin == nbClient select c).FirstOrDefault() ;
            if (client == null)
            {
                throw new Exception("account not found ");
            }
            return client;
        }
        public static void Auther(this object x)
        {
            Console.WriteLine("methods");
            
            foreach (var propertyInfo in x.GetType().GetMethods())
            {
                //var propertyValue = propertyInfo.GetType();
                var autherAttribute = propertyInfo.GetCustomAttribute<AutherAttribute>();
                if (autherAttribute != null )
                {
                    Console.WriteLine(propertyInfo.Name + " auther = " + autherAttribute.name   ); ;
                }
            }
            Console.WriteLine("attributes");

            foreach (var p in x.GetType().GetProperties())
            {
                //var propertyValue = propertyInfo.GetType();
                var autherAttribute = p.GetCustomAttribute<AutherAttribute>();
                if (autherAttribute != null)
                {
                    Console.WriteLine(p.Name + " auther = " + autherAttribute.name); ;
                }
            }
            Console.WriteLine("no auther");

        }
        /*public static void AddTransaction(this AbsctractAccount account, Transaction transaction)
        {
            account.transactions.Add(transaction.transactionNumber, transaction);
        }*/


    }
}
