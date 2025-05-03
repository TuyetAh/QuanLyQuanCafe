using DataLayer;
using DataTransferObject;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class TableService
    {
        public List<Table> LoadTableList()
        {
            return TableDAO.Instance.LoadTableList();
        }

        public bool InsertTable(string name)
        {
            return TableDAO.Instance.InsertTable(name);
        }

        public bool UpdateTable(int id, string name)
        {
            return TableDAO.Instance.UpdateTable(id, name);
        }

        public bool DeleteTable(int id)
        {
            return TableDAO.Instance.DeleteTable(id);
        }

        public void SwitchTable(int id1, int id2)
        {
            TableDAO.Instance.SwitchTable(id1, id2);
        }
    }
}
