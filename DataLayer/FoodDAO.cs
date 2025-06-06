﻿using DataLayer;
using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class FoodDAO
    {
        private static FoodDAO instance;
        public static FoodDAO Instance
        {
            get { if (instance == null) instance = new FoodDAO(); return FoodDAO.instance; }
            private set { FoodDAO.instance = value; }
        }
        private FoodDAO()
        {

        }
        public List<Food> GetFoodByCategoryID(int id)
        {
            List<Food> list = new List<Food>();
            string query = "select * from Food where idCategory = " + id;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }
            return list;

        }

        public List<Food> SearchFoodByName(string name)
        {
            List<Food> list = new List<Food>();
            string query = string.Format("select * from Food where name like N'%{0}%'", name);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }
            return list;
        }

        public List<Food> GetListFood()
        {
            List<Food> list = new List<Food>();
            string query = "select * from Food";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }
            return list;

        }

        public bool InsertFood(string name, int id, float price)
        {
            string query = string.Format("INSERT dbo.Food (name, idCategory, price) VALUES (N'{0}', {1}, {2})", name, id, price);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool UpdateFood(int idFood, string name, int id, float price)
        {
            string query = string.Format("UPDATE dbo.Food SET name = N'{0}', idCategory = {1}, price = {2} WHERE id = {3}", name, id, price, idFood);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool DeleteFood(int idFood)
        {
            BillInfoDAO.Instance.DeleteBillInfoByFoodID(idFood);

            string query = string.Format("DELETE Food WHERE id = {0}", idFood);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        //
        public DataTable GetBestSellingFood(DateTime checkIn, DateTime checkOut)
        {
            string query = @"
        SELECT TOP 1 f.Name, SUM(bi.Count) AS TotalSold
        FROM BillInfo AS bi
        JOIN Bill AS b ON bi.IDBill = b.ID
        JOIN Food AS f ON bi.IDFood = f.ID
        WHERE b.DateCheckIn >= @checkIn AND b.DateCheckOut <= @checkOut AND b.Status = 1
        GROUP BY f.Name
        ORDER BY TotalSold DESC";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { checkIn, checkOut });
        }

        public DataTable GetAllSoldFoods(DateTime checkIn, DateTime checkOut)
        {
            string query = @"
        SELECT f.Name, SUM(bi.Count) AS TotalSold
        FROM BillInfo AS bi
        JOIN Bill AS b ON bi.IDBill = b.ID
        JOIN Food AS f ON bi.IDFood = f.ID
        WHERE b.DateCheckIn >= @checkIn AND b.DateCheckOut <= @checkOut AND b.Status = 1
        GROUP BY f.Name
        ORDER BY TotalSold DESC";

            return DataProvider.Instance.ExecuteQuery(query, new object[] { checkIn, checkOut });
        }

    }
}
