using System;
using System.Collections.Generic;
using System.Text;

namespace GmcBank
{
    public interface IAbsctractAccount<TTransaction>
        where TTransaction : Transaction
    {
        IEnumerable<TTransaction> GetAllTransactions();
        void AddTransaction(TTransaction t);
        void Debit(double amount);
        void Credit(double amount);
        void SendMoney(double amount, long targetaccountNumber);
        IEnumerable<TTransaction> GetTransactionsByDate(string dateTime);
        IEnumerable<TTransaction> GetTransactionsByTarget(long accountNumber);
        Dictionary<Guid, TTransaction> GetTransactionsByQuery(string dynamicLink);

    }
}
