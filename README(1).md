# MoShop - Ứng dụng quản lý thu chi cá nhân

MoShop là ứng dụng quản lý thu chi cá nhân được xây dựng bằng:

- C#
- WPF
- Entity Framework
- SQL Server

Ứng dụng cho phép:

- Thêm khoản Thu
- Thêm khoản Chi
- Xem tổng Thu
- Xem tổng Chi
- Tính doanh thu
- Thống kê theo ngày, tháng, năm

---

# Yêu cầu môi trường

- Visual Studio 2022
- .NET
- SQL Server
- SQL Server Management Studio (SSMS)

---

# Bước 1: Tạo Database

Mở SQL Server Management Studio (SSMS) và chạy:

```sql
CREATE DATABASE MoShop
GO
```

---

# Bước 2: Tạo 2 bảng

Chọn database `MoShop` và chạy toàn bộ đoạn SQL sau:

```sql
USE MoShop
GO

CREATE TABLE Chitiet
(
    Ma INT IDENTITY(1,1) PRIMARY KEY,
    Loai BIT NULL,
    GiaTriTien DECIMAL(18,2) NOT NULL,
    NgayGioNhap DATETIME NOT NULL
)

CREATE TABLE ThongKe
(
    MaTK INT IDENTITY(1,1) PRIMARY KEY,
    Nam INT NOT NULL,
    Thang INT NOT NULL,
    Ngay INT NOT NULL,
    TongThu DECIMAL(18,2) NOT NULL,
    TongChi DECIMAL(18,2) NOT NULL
)
GO
```

Sau khi tạo xong, database sẽ có cấu trúc:

```text
MoShop
│
├── Chitiet
│   ├── Ma
│   ├── Loai
│   ├── GiaTriTien
│   └── NgayGioNhap
│
└── ThongKe
    ├── MaTK
    ├── Nam
    ├── Thang
    ├── Ngay
    ├── TongThu
    └── TongChi
```

---

# Bước 3: Sửa Connection String

Mở file:

```text
Models/MoShopContext.cs
```

Tìm đoạn kết nối SQL Server trong `OnConfiguring`.

Ví dụ:

```csharp
optionsBuilder.UseSqlServer(
    "Server=YOUR_SERVER;Database=MoShop;Trusted_Connection=True;TrustServerCertificate=True");
```

Đổi `YOUR_SERVER` thành tên SQL Server trên máy của bạn.

Ví dụ nếu SQL Server của bạn là:

```text
LAPTOP-TOPRUO27\MAY1
```

thì dùng:

```csharp
optionsBuilder.UseSqlServer(
    "Server=LAPTOP-TOPRUO27\\MAY1;Database=MoShop;Trusted_Connection=True;TrustServerCertificate=True");
```

Hoặc nếu bạn sử dụng SQL Server Express:

```csharp
optionsBuilder.UseSqlServer(
    "Server=.\\SQLEXPRESS;Database=MoShop;Trusted_Connection=True;TrustServerCertificate=True");
```

> Lưu ý: tên Server phải đúng với SQL Server đang chạy trên máy của bạn.

---

# Bước 4: Kiểm tra Database

Trong SSMS, kiểm tra database `MoShop` có 2 bảng:

```text
MoShop
└── Tables
    ├── dbo.Chitiet
    └── dbo.ThongKe
```

Bảng `Chitiet` gồm:

| Cột | Kiểu dữ liệu |
|---|---|
| Ma | int, Primary Key, Identity |
| Loai | bit, nullable |
| GiaTriTien | decimal(18,2) |
| NgayGioNhap | datetime |

Bảng `ThongKe` gồm:

| Cột | Kiểu dữ liệu |
|---|---|
| MaTK | int, Primary Key, Identity |
| Nam | int |
| Thang | int |
| Ngay | int |
| TongThu | decimal(18,2) |
| TongChi | decimal(18,2) |

---

# Bước 5: Chạy ứng dụng

Mở project bằng Visual Studio.

Mở file:

```text
MoShop.sln
```

Sau đó build project:

```text
Build → Build Solution
```

Hoặc nhấn:

```text
Ctrl + Shift + B
```

Chạy ứng dụng:

```text
F5
```

hoặc:

```text
Ctrl + F5
```

---

# Chức năng chính

## Quản lý thu chi

- Thêm Thu
- Thêm Chi
- Xóa giao dịch
- Hiển thị các giao dịch trong ngày
- Hiển thị tổng Thu
- Hiển thị tổng Chi

## Thống kê

Ứng dụng hỗ trợ xem dữ liệu thống kê theo:

- Ngày
- Tháng
- Năm

## Doanh thu

Doanh thu được tính theo công thức:

```text
Doanh thu = Tổng thu - Tổng chi
```

---

# Công nghệ sử dụng

- C#
- WPF
- Entity Framework
- SQL Server
- XAML
- MVVM

---

# Tác giả

**Nguyễn Thiên**

Project được xây dựng nhằm phục vụ quá trình học tập và thực hành lập trình C#, WPF, Entity Framework và SQL Server.
