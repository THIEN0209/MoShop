using System;
using System.Collections.Generic;

namespace MoShop.Models
{
    public partial class ThongKe
    {
        public int MaTk { get; set; }
        public int Nam { get; set; }
        public int Thang { get; set; }
        public int Ngay { get; set; }
        public decimal TongThu { get; set; }
        public decimal TongChi { get; set; }
    }
}
