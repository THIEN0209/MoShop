using System;
using System.Collections.Generic;

namespace MoShop.Models
{
    public partial class Chitiet
    {
        public int Ma { get; set; }
        public bool? Loai { get; set; }
        public decimal GiaTriTien { get; set; }
        public DateTime NgayGioNhap { get; set; }
    }
}
