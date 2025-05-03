using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class VoucherService
    {
        private Voucher _specialVoucher;

        // Constructor khởi tạo voucher "Tặng con gấu"
        public VoucherService()
        {
            _specialVoucher = new Voucher("GIAUGAU", "Tặng 1 con gấu bông dễ thương.");
        }

        // Phương thức để random và xác định xem khách hàng có trúng voucher không
        public string GetRandomVoucher()
        {
            Random rand = new Random();
            double chance = rand.NextDouble();  // Giá trị ngẫu nhiên từ 0 đến 1

            // Ví dụ: Tỷ lệ trúng voucher là 10%
            if (chance <= 0.1)  // 10% trúng
            {
                return $"Chúc mừng! Bạn đã nhận được voucher: {_specialVoucher.Code} - {_specialVoucher.Description}";
            }
            else
            {
                return "Rất tiếc! Bạn không trúng voucher này lần này.";
            }
        }
    }

    public class Voucher
    {
        public string Code { get; set; }
        public string Description { get; set; }

        public Voucher(string code, string description)
        {
            Code = code;
            Description = description;
        }
    }
}
