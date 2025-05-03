using System;

namespace DataTransferObject
{
    public class AccountEvent : EventArgs
    {
        public Account Account { get; }

        public AccountEvent(Account account)
        {
            this.Account = account;
        }
    }
}
