
using DataTransferObject;
using System.Collections.Generic;
using DataLayer;


namespace BusinessLayer
{
    public class AccountService
    {
        public bool Login(string username, string password)
        {
            return AccountDAO.Instance.Login(username, password);
        }

        public Account GetAccountByUsername(string username)
        {
            return AccountDAO.Instance.GetAccountByUserName(username);
        }

        public bool UpdateAccount(string username, string displayName, string password, string newPassword)
        {
            return AccountDAO.Instance.UpdateAccount(username, displayName, password, newPassword);
        }

        public List<Account> GetListAccount()
        {
            return AccountDAO.Instance.GetListAccount();
        }

        public bool InsertAccount(string username, string displayName, int type)
        {
            return AccountDAO.Instance.InsertAccount(username, displayName, type);
        }

        public bool UpdateAccount(string username, string displayName, int type)
        {
            return AccountDAO.Instance.UpdateAccount(username, displayName, type);
        }

        public bool DeleteAccount(string username)
        {
            return AccountDAO.Instance.DeleteAccount(username);
        }

        public void ResetPassword(string username)
        {
            AccountDAO.Instance.ResetPassWord(username);
        }
    }
}
