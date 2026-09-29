using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MoShop.Models;

namespace MoShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void HienThiTongThuChi()
        {
            MoShopContext db = new MoShopContext();

            DateTime homNay = DateTime.Today;
            DateTime ngayMai = homNay.AddDays(1);

            var list = db.Chitiets
                         .Where(x => x.NgayGioNhap >= homNay &&
                                     x.NgayGioNhap < ngayMai)
                         .ToList();

            decimal tongThu = list
                .Where(x => x.Loai == true)
                .Sum(x => x.GiaTriTien);

            decimal tongChi = list
                .Where(x => x.Loai == false)
                .Sum(x => x.GiaTriTien);

            txtTongThu.Text = "Tổng thu: " + tongThu.ToString("#,##0") + " đ";
            txtTongChi.Text = "Tổng chi: " + tongChi.ToString("#,##0") + " đ";
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MoShopContext db = new MoShopContext();

            DateTime homNay = DateTime.Today;
            DateTime ngayMai = homNay.AddDays(1);

            var list = db.Chitiets
                         .Where(x => x.NgayGioNhap >= homNay &&
                                     x.NgayGioNhap < ngayMai)
                         .ToList()
                         .Select(x => CCT.ChuyenDoi(x))
                         .ToList();

            dgCT.ItemsSource = list;
            HienThiTongThuChi();
            dondulieu();
        }

        private void btnThu_Click(object sender, RoutedEventArgs e)
        {
            decimal soTien;

            if (!decimal.TryParse(txtGiaTriTien.Text, out soTien))
            {
                MessageBox.Show("Vui lòng nhập số tiền hợp lệ!");
                return;
            }

            if (soTien <= 0)
            {
                MessageBox.Show("Số tiền phải lớn hơn 0!");
                return;
            }

            if (soTien > 10000)
            {
                MessageBox.Show("Số tiền không được vượt quá 10.000.000 đ!");
                return;
            }

            // Nhân 1000 vì người dùng nhập theo đơn vị nghìn
            soTien = soTien * 1000;

            MoShopContext db = new MoShopContext();

            Chitiet ct = new Chitiet
            {
                Loai = true,
                GiaTriTien = soTien,
                NgayGioNhap = DateTime.Now
            };

            db.Chitiets.Add(ct);
            db.SaveChanges();

            txtGiaTriTien.Clear();

            CapNhatThongKeHomNay();
            Window_Loaded(null, null);
        }


        private void btnChi_Click(object sender, RoutedEventArgs e)
        {
            decimal soTien;

            if (!decimal.TryParse(txtGiaTriTien.Text, out soTien))
            {
                MessageBox.Show("Vui lòng nhập số tiền hợp lệ!");
                return;
            }

            if (soTien <= 0)
            {
                MessageBox.Show("Số tiền phải lớn hơn 0!");
                return;
            }

            if (soTien > 10000)
            {
                MessageBox.Show("Số tiền không được vượt quá 10.000.000 đ!");
                return;
            }

            // Nhân 1000 vì người dùng nhập theo đơn vị nghìn
            soTien = soTien * 1000;

            MoShopContext db = new MoShopContext();

            Chitiet ct = new Chitiet
            {
                Loai = false,
                GiaTriTien = soTien,
                NgayGioNhap = DateTime.Now
            };

            db.Chitiets.Add(ct);
            db.SaveChanges();

            txtGiaTriTien.Clear();

            CapNhatThongKeHomNay();
            Window_Loaded(null, null);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            int ma = Convert.ToInt32(btn.Tag);

            MoShopContext db = new MoShopContext();

            Chitiet ct = db.Chitiets.FirstOrDefault(x => x.Ma == ma);

            if (ct == null)
            {
                MessageBox.Show("Không tìm thấy dữ liệu!");
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa khoản này không?",
                "Xác nhận xóa",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
                return;

            db.Chitiets.Remove(ct);
            db.SaveChanges();

            CapNhatThongKeHomNay();
            Window_Loaded(null, null);
        }
        private void CapNhatThongKeHomNay()
        {
            MoShopContext db = new MoShopContext();

            DateTime homNay = DateTime.Today;
            DateTime ngayMai = homNay.AddDays(1);

            // Xóa thống kê của hôm nay nếu đã có
            var thongKeCu = db.ThongKes
                .FirstOrDefault(x => x.Nam == homNay.Year
                                  && x.Thang == homNay.Month
                                  && x.Ngay == homNay.Day);

            if (thongKeCu != null)
            {
                db.ThongKes.Remove(thongKeCu);
                db.SaveChanges();
            }

            // Lấy toàn bộ giao dịch của hôm nay
            var list = db.Chitiets
                .Where(x => x.NgayGioNhap >= homNay &&
                            x.NgayGioNhap < ngayMai)
                .ToList();

            // Tính tổng Thu
            decimal tongThu = list
                .Where(x => x.Loai == true)
                .Sum(x => x.GiaTriTien);

            // Tính tổng Chi
            decimal tongChi = list
                .Where(x => x.Loai == false)
                .Sum(x => x.GiaTriTien);

            // Tạo thống kê mới
            ThongKe tk = new ThongKe
            {
                Nam = homNay.Year,
                Thang = homNay.Month,
                Ngay = homNay.Day,
                TongThu = tongThu,
                TongChi = tongChi
            };

            db.ThongKes.Add(tk);
            db.SaveChanges();
        }
        private void btnThongKeThang_Click(object sender, RoutedEventArgs e)
        {
            ThongKeThang f=new ThongKeThang();
            f.Show();
        }

        private void btnDoanhSo_Click(object sender, RoutedEventArgs e)
        {
            DoanhSo f=new DoanhSo();
            f.Show();
        }
        private void dondulieu()
        {
            MoShopContext db = new MoShopContext();

            DateTime ngayHienTai = DateTime.Now;

            // Chỉ dọn dữ liệu vào ngày 15 hàng tháng
            if (ngayHienTai.Day == 16)
            {
                // Lấy tháng cần xóa: 3 tháng trước
                DateTime thangCanXoa = ngayHienTai.AddMonths(-3);

                int nam = thangCanXoa.Year;
                int thang = thangCanXoa.Month;

                // Lấy toàn bộ dữ liệu của tháng đó
                var danhSach = db.Chitiets
                    .Where(x => x.NgayGioNhap.Year == nam
                             && x.NgayGioNhap.Month == thang)
                    .ToList();

                // Xóa
                if (danhSach.Count > 0)
                {
                    db.Chitiets.RemoveRange(danhSach);
                    db.SaveChanges();
                }
            }
        }
    }
}