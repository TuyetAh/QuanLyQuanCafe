using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyQuanCafe.DTO;


namespace QuanLyQuanCafe.DAO
{
    public static class AccountSession
    {
        public static Account LoggedInAccount { get; set; }
    }
}
