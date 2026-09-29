using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoShop.Models
{
    class CCT
    {
        public int Ma { get; set; }

        public bool? Loai { get; set; }

        public decimal GiaTriTien { get; set; }

        public DateTime NgayGioNhap { get; set; }

        // Giá trị tiền đã định dạng để hiển thị
        public string GiaTriTienHienThi
        {
            get
            {
                if (Loai == false)
                    return "-" + GiaTriTien.ToString("#,##0") + " đ";

                return GiaTriTien.ToString("#,##0") + " đ";
            }
        }

        // Hiển thị ngày giờ đẹp hơn
        public string NgayGioHienThi
        {
            get
            {
                return NgayGioNhap.ToString("HH:mm:ss");
            }
        }
        public static CCT ChuyenDoi(Chitiet chitiet)
        {
            return new CCT
            {
                Ma = chitiet.Ma,
                Loai = chitiet.Loai,
                GiaTriTien = chitiet.GiaTriTien,
                NgayGioNhap = chitiet.NgayGioNhap
            };
        }
    }
}



