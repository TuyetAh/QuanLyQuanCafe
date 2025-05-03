using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Customer
    {
        
            public string Name { get; set; }
            public int Points { get; set; }  // Lưu trữ điểm tích lũy của khách hàng

            public Customer(string name)
            {
                Name = name;
                Points = 0;  // Mặc định khi tạo khách hàng sẽ có 0 điểm
            }

            // Phương thức cộng điểm
            public void AddPoints(int points)
            {
                Points += points;
            }
        

    }
}
