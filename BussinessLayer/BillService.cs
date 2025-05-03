using DataLayer;
using System;
using System.Data; // Thêm using này để dùng DataTable

namespace BusinessLayer
{
    public class BillService
    {
        public int GetUncheckBillIDByTableID(int id)
        {
            return BillDAO.Instance.GetUncheckBillIDByTableID(id);
        }

        public void InsertBill(int idTable)
        {
            BillDAO.Instance.InsertBill(idTable);
        }

        public int GetMaxIDBill()
        {
            return BillDAO.Instance.GetMaxIDBill();
        }

        public DataTable GetBillListByDate(DateTime checkIn, DateTime checkOut)
        {
            return BillDAO.Instance.GetBillListByDate(checkIn, checkOut);
        }

        public void Checkout(int id, int discount, float totalPrice)
        {
            BillDAO.Instance.CheckOut(id, discount, totalPrice);
        }
    }
}
