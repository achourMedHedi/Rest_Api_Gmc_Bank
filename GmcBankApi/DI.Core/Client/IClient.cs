using System;
using System.Collections.Generic;
using System.Text;

namespace GmcBank
{
    interface IClient<TAbstractAccount , TTransaction> 
        where TAbstractAccount : IAbsctractAccount<TTransaction> 
        where TTransaction : Transaction
    {
        IEnumerable<TAbstractAccount> GetAllAccounts();
        TAbstractAccount GetAccount(long accountNumber);
        void CreateAccount(TAbstractAccount account);
        void CloseAccount(TAbstractAccount account);

    }
}
