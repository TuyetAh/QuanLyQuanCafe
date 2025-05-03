using DataLayer;
using DataTransferObject;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class MenuService
    {
        public List<Menu> GetListMenuByTable(int id)
        {
            return MenuDAO.Instance.GetListMenuByTable(id);
        }
    }
}
