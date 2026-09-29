using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoShop.Models
{
    class CTKThang
    {
        public int Thang { get; set; }

        public decimal TongThu { get; set; }

        public decimal TongChi { get; set; }

        public decimal DoanhThu
        {
            get
            {
                return TongThu - TongChi;
            }
        }

        public string TongThuHienThi
        {
            get
            {
                return TongThu.ToString("N0") + " đ";
            }
        }

        public string TongChiHienThi
        {
            get
            {
                return TongChi.ToString("N0") + " đ";
            }
        }

        public string DoanhThuHienThi
        {
            get
            {
                return DoanhThu.ToString("N0") + " đ";
            }
        }
    }
}
