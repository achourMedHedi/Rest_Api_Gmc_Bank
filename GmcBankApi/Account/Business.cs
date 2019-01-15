using System;


namespace GmcBank
{
    delegate void DebitAccount(double amount);
    public class Business : AbsctractAccount<Transaction>
    {
        public override double TaxRatio { get; set; }

        public Business() : base() { }
        public Business (long accNumber , Client<AbsctractAccount<Transaction>, Transaction> client) : base(accNumber , client)
        {
            TaxRatio = 0.1; 
        }
        public override void SendMoney(double amount, long targetaccountNumber)
        {
            DebitAccount debitAccount = Debit;
            base.SendMoney(amount, targetaccountNumber);
            double debit = amount + (amount * TaxRatio);
            if ((balance - debit ) >= 0 )
            {
                debitAccount(debit);
                //balance -= (amount + (amount * TaxRatio));
                Transaction transaction = new Transaction(accountNumber, targetaccountNumber, amount);
                transaction.direction = "OutGoing";
                AddTransaction(transaction);
                Console.WriteLine("eyyyyyy B" + balance);
            }
            else
            {
                Console.WriteLine("leeeeee");
                throw new Exception("you cant balance < amount ");
            }
        }
        public override void Debit(double amount)
        {
            balance -= (amount + (amount * TaxRatio)); 
        }
        /*public static int operator+ (int a , int b)
        {
            return 1;
        }*/

    }
}
