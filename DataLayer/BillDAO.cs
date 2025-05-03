using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance
        {
            get { if (instance == null) instance = new BillDAO(); return BillDAO.instance; }
            private set { BillDAO.instance = value; }
        }

        private BillDAO() { }
        /// <summary>
        /// Thành công: bill ID
        /// Thất bại : -1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>


        public int GetUncheckBillIDByTableID(int id)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.Bill WHERE idTable = " + id + " AND status = 0");

            if (data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.ID;
            }

            return -1;
        }
        public void CheckOut(int id, int discount, float totalPrice)
        {
            string query = "UPDATE dbo.Bill SET dateCheckOut = GETDATE(), status = 1, " + "discount = " + discount + ", totalPrice = " + totalPrice + " WHERE id = " + id;
            DataProvider.Instance.ExecuteNonQuery(query);
        }
        public void InsertBill(int id)
        {
            DataProvider.Instance.ExecuteNonQuery("exec USP_INSERTBILL @idTable", new object[] { id });

        }

        public DataTable GetBillListByDate(DateTime checkIn, DateTime checkOut)
        {
            return DataProvider.Instance.ExecuteQuery(
                "exec USP_GetListBillByDate @checkIn , @checkOut",
                new object[] { checkIn, checkOut });
        }

        public DataTable GetBillListByDateAndPage(DateTime checkIn, DateTime checkOut, int pageNum)
        {
            return DataProvider.Instance.ExecuteQuery(
                "exec USP_GetListBillByDateAndPage @checkIn , @checkOut , @page",
                new object[] { checkIn, checkOut, pageNum });
        }

        public int GetNumBillListByDate(DateTime checkIn, DateTime checkOut)
        {
            return (int)DataProvider.Instance.ExecuteScalar(
                "exec USP_GetNumBillByDate @checkIn , @checkOut",
                new object[] { checkIn, checkOut });
        }
        public int GetMaxIDBill()
        {
            try
            {
                return (int)DataProvider.Instance.ExecuteScalar("SELECT MAX(id) FROM dbo.Bill");
            }
            catch
            {
                return 1;
            }
        }
        // noted 
        public float GetRevenueByDate(DateTime checkIn, DateTime checkOut)
        {
            float revenue = 0;
            string query = "SELECT SUM(b.TotalPrice) FROM Bill AS b WHERE b.DateCheckIn >= @checkIn AND b.DateCheckOut <= @checkOut AND b.Status = 1";
            object result = DataProvider.Instance.ExecuteScalar(query, new object[] { checkIn, checkOut });
            if (result != DBNull.Value)
                revenue = Convert.ToSingle(result);
            return revenue;
        }
        public int GetTotalBills(DateTime checkIn, DateTime checkOut)
        {
            string query = "SELECT COUNT(*) FROM Bill WHERE DateCheckIn >= @checkIn AND DateCheckOut <= @checkOut AND Status = 1";
            return (int)DataProvider.Instance.ExecuteScalar(query, new object[] { checkIn, checkOut });
        }

        public DataTable GetDailyRevenue(DateTime checkIn, DateTime checkOut)
        {
            string query = @"
        SELECT 
            CAST(DateCheckIn AS DATE) AS Ngay,
            SUM(TotalPrice) AS DoanhThu
        FROM Bill
        WHERE DateCheckIn >= @checkIn AND DateCheckOut <= @checkOut AND Status = 1
        GROUP BY CAST(DateCheckIn AS DATE)
        ORDER BY Ngay";

            return DataProvider.Instance.ExecuteQuery(query, new object[] { checkIn, checkOut });
        }
        public DataTable GetDailyTotalBills(DateTime checkIn, DateTime checkOut)
        {
            string query = @"
        SELECT 
            CAST(DateCheckIn AS DATE) AS Ngay,
            COUNT(*) AS SoHoaDon
        FROM Bill
        WHERE DateCheckIn >= @checkIn AND DateCheckOut <= @checkOut AND Status = 1
        GROUP BY CAST(DateCheckIn AS DATE)
        ORDER BY Ngay";

            return DataProvider.Instance.ExecuteQuery(query, new object[] { checkIn, checkOut });
        }


    }
}
