using System;

namespace MoShop.Models
{
    class CTK
    {
        public int Nam { get; set; }
        public int Thang { get; set; }
        public int Ngay { get; set; }

        public decimal TongThu { get; set; }

        public decimal TongChi { get; set; }

        public decimal DoanhThu
        {
            get
            {
                return TongThu - TongChi;
            }
        }

        // Hiển thị tiền đẹp
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

        // Hàm chuyển đổi từ ThongKe sang CTK
        public static CTK ChuyenDoi(ThongKe thongKe)
        {
            return new CTK
            {
                Ngay=thongKe.Ngay,
                Thang=thongKe.Thang,
                Nam=thongKe.Nam,

                TongThu = thongKe.TongThu,
                TongChi = thongKe.TongChi
            };
        }
    }
}