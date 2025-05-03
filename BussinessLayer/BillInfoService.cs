using DataLayer;

namespace BusinessLayer
{
    public class BillInfoService
    {
        public void InsertBillInfo(int idBill, int idFood, int count)
        {
            BillInfoDAO.Instance.InsertBillInfo(idBill, idFood, count);
        }

        public void DeleteBillInfoByFoodID(int id)
        {
            BillInfoDAO.Instance.DeleteBillInfoByFoodID(id);
        }
    }
}
