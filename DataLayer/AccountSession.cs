using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;


namespace DataLayer
{
    public static class AccountSession
    {
        public static Account LoggedInAccount { get; set; }
    }
}
