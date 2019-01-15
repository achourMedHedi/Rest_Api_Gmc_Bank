using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace GmcBank
{
    public interface IBank<TClient , TAbsctractAccount , TTransaction>
        where TClient : Client<TAbsctractAccount , TTransaction>
        where TAbsctractAccount : AbsctractAccount<TTransaction>
        where TTransaction : Transaction
    {
        void AddTransaction(TTransaction transaction);
        TAbsctractAccount account(long accNumber);
        void AddAgent();
        void AddAgent(int nbAgents);
        void RemoveAgent();
        void RemoveAgent(int nbAgents);
        Bank<TClient, TAbsctractAccount, TTransaction> LoadFile(string path);
        void SaveFile(string path );
    }
}
