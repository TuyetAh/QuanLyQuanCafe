using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    class Table
    {
        public Table(int id, string name, string status) 
        {
            this.ID = iD;
            this.Name = name;
            this.Status = status;
        }

        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        private string name;
        public string Name 
        {
            get { return name; }
            set { name = value; }
        }

        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

    }
}
