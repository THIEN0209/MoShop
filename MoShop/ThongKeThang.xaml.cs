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
    /// Interaction logic for ThongKeThang.xaml
    /// </summary>
    public partial class ThongKeThang : Window
    {
        public ThongKeThang()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Thêm 12 tháng
            for (int i = 1; i <= 12; i++)
            {
                cboThang.Items.Add(i);
            }

            // Thêm năm từ 2026 đến 2036
            for (int i = 2026; i <= 2036; i++)
            {
                cboNam.Items.Add(i);
            }

            // Lấy tháng và năm hiện tại
            int thangHienTai = DateTime.Now.Month;
            int namHienTai = DateTime.Now.Year;

            // Chọn tháng hiện tại
            cboThang.SelectedItem = thangHienTai;

            // Nếu năm hiện tại nằm trong khoảng 2026 - 2036
            if (namHienTai >= 2026 && namHienTai <= 2036)
            {
                cboNam.SelectedItem = namHienTai;
            }
            else
            {
                cboNam.SelectedItem = 2026;
            }

            // Tự động lọc dữ liệu khi mở cửa sổ
            LocThongKe();
        }
        private void btnLoc_Click(object sender, RoutedEventArgs e)
        {
            LocThongKe();
        }
        private void LocThongKe()
        {
            MoShopContext db=new MoShopContext();
            if (cboThang.SelectedItem == null || cboNam.SelectedItem == null)
                return;

            int thang = (int)cboThang.SelectedItem;
            int nam = (int)cboNam.SelectedItem;

            var danhSach = db.ThongKes
                .Where(x => x.Thang == thang && x.Nam == nam)
                .OrderBy(x => x.Ngay)
                .ToList();

            // Chuyển sang CTK
            var ketQua = danhSach
                .Select(x => CTK.ChuyenDoi(x))
                .ToList();

            // Hiển thị lên DataGrid
            dtgThongKe.ItemsSource = ketQua;

            // Tính tổng
            decimal tongThu = ketQua.Sum(x => x.TongThu);
            decimal tongChi = ketQua.Sum(x => x.TongChi);
            decimal doanhThu = tongThu - tongChi;

            // Hiển thị tổng
            txtTongThu.Text = tongThu.ToString("N0") + " đ";
            txtTongChi.Text = tongChi.ToString("N0") + " đ";
            txtDoanhThu.Text = doanhThu.ToString("N0") + " đ";
        }
    }
}
