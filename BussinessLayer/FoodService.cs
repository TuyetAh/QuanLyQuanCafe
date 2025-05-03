using DataLayer;
using DataTransferObject;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class FoodService
    {
        public List<Food> GetFoodByCategoryID(int id)
        {
            return FoodDAO.Instance.GetFoodByCategoryID(id);
        }

        public List<Food> GetListFood()
        {
            return FoodDAO.Instance.GetListFood();
        }

        public bool InsertFood(string name, int categoryID, float price)
        {
            return FoodDAO.Instance.InsertFood(name, categoryID, price);
        }

        public bool UpdateFood(int id, string name, int categoryID, float price)
        {
            return FoodDAO.Instance.UpdateFood(id, name, categoryID, price);
        }

        public bool DeleteFood(int id)
        {
            return FoodDAO.Instance.DeleteFood(id);
        }

        public List<Food> SearchFoodByName(string name)
        {
            return FoodDAO.Instance.SearchFoodByName(name);
        }
    }
}
