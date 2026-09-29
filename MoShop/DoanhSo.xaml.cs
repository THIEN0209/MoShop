using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MoShop.Models;

namespace MoShop
{
    /// <summary>
    /// Interaction logic for DoanhSo.xaml
    /// </summary>
    public partial class DoanhSo : Window
    {
        public DoanhSo()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Nạp năm từ 2026 đến 2036
            for (int i = 2026; i <= 2036; i++)
            {
                cboNam.Items.Add(i);
            }

            // Lấy năm hiện tại
            int namHienTai = DateTime.Now.Year;

            // Nếu năm hiện tại nằm trong khoảng 2026 - 2036
            if (namHienTai >= 2026 && namHienTai <= 2036)
            {
                cboNam.SelectedItem = namHienTai;
            }
            else
            {
                cboNam.SelectedItem = 2026;
            }

            // Hiển thị thống kê của năm hiện tại
            HienThiThongKeNam();
        }
        private void HienThiThongKeNam()
        {
            MoShopContext db = new MoShopContext();
            if (cboNam.SelectedItem == null)
                return;

            int nam = (int)cboNam.SelectedItem;

            // Lấy dữ liệu trong năm được chọn
            var danhSach = db.ThongKes
                .Where(x => x.Nam == nam)
                .ToList();

            // Tạo đủ 12 tháng
            var danhSachThang = Enumerable.Range(1, 12)
                .Select(thang =>
                {
                    var data = danhSach
                        .Where(x => x.Thang == thang);

                    return new CTKThang
                    {
                        Thang = thang,
                        TongThu = data.Sum(x => x.TongThu),
                        TongChi = data.Sum(x => x.TongChi)
                    };
                })
                .ToList();

            // Hiển thị lên DataGrid
            dtgDoanhSo.ItemsSource = danhSachThang;

            // Tổng cả năm
            decimal tongThu = danhSach.Sum(x => x.TongThu);
            decimal tongChi = danhSach.Sum(x => x.TongChi);
            decimal doanhThu = tongThu - tongChi;

            // Hiển thị tổng
            txtTongThu.Text = tongThu.ToString("N0") + " đ";
            txtTongChi.Text = tongChi.ToString("N0") + " đ";
            txtDoanhThu.Text = doanhThu.ToString("N0") + " đ";
        }
        private void btnXem_Click(object sender, RoutedEventArgs e)
        {
            HienThiThongKeNam();
        }
    }
}
