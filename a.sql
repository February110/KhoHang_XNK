USE [Database_KhoHangXNK]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 19/05/2025 1:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 19/05/2025 1:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 19/05/2025 1:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 19/05/2025 1:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 19/05/2025 1:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 19/05/2025 1:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 19/05/2025 1:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[AvatarUrl] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 19/05/2025 1:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietDonNhaps]    Script Date: 19/05/2025 1:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietDonNhaps](
	[MaDonNhap] [int] NOT NULL,
	[MaHangHoa] [int] NOT NULL,
	[SoLuong] [int] NOT NULL,
	[DonGia] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_ChiTietDonNhaps] PRIMARY KEY CLUSTERED 
(
	[MaDonNhap] ASC,
	[MaHangHoa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietDonXuats]    Script Date: 19/05/2025 1:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietDonXuats](
	[MaDonXuat] [int] NOT NULL,
	[MaHangHoa] [int] NOT NULL,
	[SoLuong] [int] NOT NULL,
	[DonGia] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_ChiTietDonXuats] PRIMARY KEY CLUSTERED 
(
	[MaDonXuat] ASC,
	[MaHangHoa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietPhieuKiemKes]    Script Date: 19/05/2025 1:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietPhieuKiemKes](
	[MaKiemKe] [int] NOT NULL,
	[MaHangHoa] [int] NOT NULL,
	[SoLuongThucTe] [int] NOT NULL,
	[SoLuongChenhLech] [int] NULL,
 CONSTRAINT [PK_ChiTietPhieuKiemKes] PRIMARY KEY CLUSTERED 
(
	[MaKiemKe] ASC,
	[MaHangHoa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DonNhapHangs]    Script Date: 19/05/2025 1:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonNhapHangs](
	[MaDonNhap] [int] IDENTITY(1,1) NOT NULL,
	[NgayNhap] [datetime2](7) NOT NULL,
	[MaNCC] [int] NOT NULL,
	[MaKho] [int] NOT NULL,
	[MaNV] [int] NOT NULL,
	[trangthaithanhtoan] [int] NOT NULL,
 CONSTRAINT [PK_DonNhapHangs] PRIMARY KEY CLUSTERED 
(
	[MaDonNhap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DonXuatHangs]    Script Date: 19/05/2025 1:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonXuatHangs](
	[MaDonXuat] [int] IDENTITY(1,1) NOT NULL,
	[NgayXuat] [datetime2](7) NOT NULL,
	[MaKhachHang] [int] NOT NULL,
	[MaNV] [int] NOT NULL,
	[MaKho] [int] NOT NULL,
	[trangthaithanhtoan] [int] NOT NULL,
 CONSTRAINT [PK_DonXuatHangs] PRIMARY KEY CLUSTERED 
(
	[MaDonXuat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HangHoas]    Script Date: 19/05/2025 1:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HangHoas](
	[MaHangHoa] [int] IDENTITY(1,1) NOT NULL,
	[TenHangHoa] [nvarchar](100) NOT NULL,
	[MoTa] [nvarchar](max) NOT NULL,
	[DonViTinh] [nvarchar](max) NOT NULL,
	[HanSuDung] [datetime2](7) NOT NULL,
	[ImageUrl] [nvarchar](max) NULL,
	[MaLoaiHang] [int] NOT NULL,
 CONSTRAINT [PK_HangHoas] PRIMARY KEY CLUSTERED 
(
	[MaHangHoa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KhachHangs]    Script Date: 19/05/2025 1:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhachHangs](
	[MaKH] [int] IDENTITY(1,1) NOT NULL,
	[TenKH] [nvarchar](255) NOT NULL,
	[DiaChi] [nvarchar](255) NULL,
	[SoDienThoai] [nvarchar](15) NULL,
	[Email] [nvarchar](100) NULL,
	[MaLoaiKH] [int] NOT NULL,
 CONSTRAINT [PK_KhachHangs] PRIMARY KEY CLUSTERED 
(
	[MaKH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KhoHangs]    Script Date: 19/05/2025 1:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhoHangs](
	[MaKho] [int] IDENTITY(1,1) NOT NULL,
	[TenKho] [nvarchar](255) NOT NULL,
	[DiaChi] [nvarchar](max) NULL,
	[SucChua] [int] NOT NULL,
	[ImageUrl] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NULL,
 CONSTRAINT [PK_KhoHangs] PRIMARY KEY CLUSTERED 
(
	[MaKho] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiHangHoas]    Script Date: 19/05/2025 1:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiHangHoas](
	[MaLoaiHang] [int] IDENTITY(1,1) NOT NULL,
	[TenLoaiHang] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_LoaiHangHoas] PRIMARY KEY CLUSTERED 
(
	[MaLoaiHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiKhachHangs]    Script Date: 19/05/2025 1:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiKhachHangs](
	[MaLoaiKH] [int] IDENTITY(1,1) NOT NULL,
	[TenLoai] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_LoaiKhachHangs] PRIMARY KEY CLUSTERED 
(
	[MaLoaiKH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhaCungCaps]    Script Date: 19/05/2025 1:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhaCungCaps](
	[MaNCC] [int] IDENTITY(1,1) NOT NULL,
	[TenNCC] [nvarchar](255) NOT NULL,
	[DiaChi] [nvarchar](255) NULL,
	[SoDienThoai] [nvarchar](15) NULL,
	[Email] [nvarchar](100) NULL,
 CONSTRAINT [PK_NhaCungCaps] PRIMARY KEY CLUSTERED 
(
	[MaNCC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhanViens]    Script Date: 19/05/2025 1:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhanViens](
	[MaNV] [int] IDENTITY(1,1) NOT NULL,
	[HoTen] [nvarchar](255) NOT NULL,
	[NgaySinh] [datetime2](7) NULL,
	[ChucVu] [nvarchar](100) NULL,
	[SoDienThoai] [nvarchar](15) NULL,
	[Email] [nvarchar](100) NULL,
	[MaKho] [int] NULL,
	[ImageUrl] [nvarchar](max) NULL,
 CONSTRAINT [PK_NhanViens] PRIMARY KEY CLUSTERED 
(
	[MaNV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhieuKiemKes]    Script Date: 19/05/2025 1:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuKiemKes](
	[MaKiemKe] [int] IDENTITY(1,1) NOT NULL,
	[MaNV] [int] NOT NULL,
	[MaKho] [int] NOT NULL,
	[NgayKiemKe] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_PhieuKiemKes] PRIMARY KEY CLUSTERED 
(
	[MaKiemKe] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TonKhos]    Script Date: 19/05/2025 1:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TonKhos](
	[MaKho] [int] NOT NULL,
	[MaHangHoa] [int] NOT NULL,
	[SoLuong] [int] NOT NULL,
 CONSTRAINT [PK_TonKhos] PRIMARY KEY CLUSTERED 
(
	[MaKho] ASC,
	[MaHangHoa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DonNhapHangs] ADD  DEFAULT ((0)) FOR [trangthaithanhtoan]
GO
ALTER TABLE [dbo].[DonXuatHangs] ADD  DEFAULT ((0)) FOR [trangthaithanhtoan]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[ChiTietDonNhaps]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietDonNhaps_DonNhapHangs_MaDonNhap] FOREIGN KEY([MaDonNhap])
REFERENCES [dbo].[DonNhapHangs] ([MaDonNhap])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietDonNhaps] CHECK CONSTRAINT [FK_ChiTietDonNhaps_DonNhapHangs_MaDonNhap]
GO
ALTER TABLE [dbo].[ChiTietDonNhaps]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietDonNhaps_HangHoas_MaHangHoa] FOREIGN KEY([MaHangHoa])
REFERENCES [dbo].[HangHoas] ([MaHangHoa])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietDonNhaps] CHECK CONSTRAINT [FK_ChiTietDonNhaps_HangHoas_MaHangHoa]
GO
ALTER TABLE [dbo].[ChiTietDonXuats]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietDonXuats_DonXuatHangs_MaDonXuat] FOREIGN KEY([MaDonXuat])
REFERENCES [dbo].[DonXuatHangs] ([MaDonXuat])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietDonXuats] CHECK CONSTRAINT [FK_ChiTietDonXuats_DonXuatHangs_MaDonXuat]
GO
ALTER TABLE [dbo].[ChiTietDonXuats]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietDonXuats_HangHoas_MaHangHoa] FOREIGN KEY([MaHangHoa])
REFERENCES [dbo].[HangHoas] ([MaHangHoa])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietDonXuats] CHECK CONSTRAINT [FK_ChiTietDonXuats_HangHoas_MaHangHoa]
GO
ALTER TABLE [dbo].[ChiTietPhieuKiemKes]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietPhieuKiemKes_HangHoas_MaHangHoa] FOREIGN KEY([MaHangHoa])
REFERENCES [dbo].[HangHoas] ([MaHangHoa])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietPhieuKiemKes] CHECK CONSTRAINT [FK_ChiTietPhieuKiemKes_HangHoas_MaHangHoa]
GO
ALTER TABLE [dbo].[ChiTietPhieuKiemKes]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietPhieuKiemKes_PhieuKiemKes_MaKiemKe] FOREIGN KEY([MaKiemKe])
REFERENCES [dbo].[PhieuKiemKes] ([MaKiemKe])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietPhieuKiemKes] CHECK CONSTRAINT [FK_ChiTietPhieuKiemKes_PhieuKiemKes_MaKiemKe]
GO
ALTER TABLE [dbo].[DonNhapHangs]  WITH CHECK ADD  CONSTRAINT [FK_DonNhapHangs_KhoHangs_MaKho] FOREIGN KEY([MaKho])
REFERENCES [dbo].[KhoHangs] ([MaKho])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DonNhapHangs] CHECK CONSTRAINT [FK_DonNhapHangs_KhoHangs_MaKho]
GO
ALTER TABLE [dbo].[DonNhapHangs]  WITH CHECK ADD  CONSTRAINT [FK_DonNhapHangs_NhaCungCaps_MaNCC] FOREIGN KEY([MaNCC])
REFERENCES [dbo].[NhaCungCaps] ([MaNCC])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DonNhapHangs] CHECK CONSTRAINT [FK_DonNhapHangs_NhaCungCaps_MaNCC]
GO
ALTER TABLE [dbo].[DonNhapHangs]  WITH CHECK ADD  CONSTRAINT [FK_DonNhapHangs_NhanViens_MaNV] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NhanViens] ([MaNV])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DonNhapHangs] CHECK CONSTRAINT [FK_DonNhapHangs_NhanViens_MaNV]
GO
ALTER TABLE [dbo].[DonXuatHangs]  WITH CHECK ADD  CONSTRAINT [FK_DonXuatHangs_KhachHangs_MaKhachHang] FOREIGN KEY([MaKhachHang])
REFERENCES [dbo].[KhachHangs] ([MaKH])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DonXuatHangs] CHECK CONSTRAINT [FK_DonXuatHangs_KhachHangs_MaKhachHang]
GO
ALTER TABLE [dbo].[DonXuatHangs]  WITH CHECK ADD  CONSTRAINT [FK_DonXuatHangs_KhoHangs_MaKho] FOREIGN KEY([MaKho])
REFERENCES [dbo].[KhoHangs] ([MaKho])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DonXuatHangs] CHECK CONSTRAINT [FK_DonXuatHangs_KhoHangs_MaKho]
GO
ALTER TABLE [dbo].[DonXuatHangs]  WITH CHECK ADD  CONSTRAINT [FK_DonXuatHangs_NhanViens_MaNV] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NhanViens] ([MaNV])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DonXuatHangs] CHECK CONSTRAINT [FK_DonXuatHangs_NhanViens_MaNV]
GO
ALTER TABLE [dbo].[HangHoas]  WITH CHECK ADD  CONSTRAINT [FK_HangHoas_LoaiHangHoas_MaLoaiHang] FOREIGN KEY([MaLoaiHang])
REFERENCES [dbo].[LoaiHangHoas] ([MaLoaiHang])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HangHoas] CHECK CONSTRAINT [FK_HangHoas_LoaiHangHoas_MaLoaiHang]
GO
ALTER TABLE [dbo].[KhachHangs]  WITH CHECK ADD  CONSTRAINT [FK_KhachHangs_LoaiKhachHangs_MaLoaiKH] FOREIGN KEY([MaLoaiKH])
REFERENCES [dbo].[LoaiKhachHangs] ([MaLoaiKH])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[KhachHangs] CHECK CONSTRAINT [FK_KhachHangs_LoaiKhachHangs_MaLoaiKH]
GO
ALTER TABLE [dbo].[KhoHangs]  WITH CHECK ADD  CONSTRAINT [FK_KhoHangs_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[KhoHangs] CHECK CONSTRAINT [FK_KhoHangs_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[NhanViens]  WITH CHECK ADD  CONSTRAINT [FK_NhanViens_KhoHangs_MaKho] FOREIGN KEY([MaKho])
REFERENCES [dbo].[KhoHangs] ([MaKho])
GO
ALTER TABLE [dbo].[NhanViens] CHECK CONSTRAINT [FK_NhanViens_KhoHangs_MaKho]
GO
ALTER TABLE [dbo].[PhieuKiemKes]  WITH CHECK ADD  CONSTRAINT [FK_PhieuKiemKes_KhoHangs_MaKho] FOREIGN KEY([MaKho])
REFERENCES [dbo].[KhoHangs] ([MaKho])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PhieuKiemKes] CHECK CONSTRAINT [FK_PhieuKiemKes_KhoHangs_MaKho]
GO
ALTER TABLE [dbo].[PhieuKiemKes]  WITH CHECK ADD  CONSTRAINT [FK_PhieuKiemKes_NhanViens_MaNV] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NhanViens] ([MaNV])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PhieuKiemKes] CHECK CONSTRAINT [FK_PhieuKiemKes_NhanViens_MaNV]
GO
ALTER TABLE [dbo].[TonKhos]  WITH CHECK ADD  CONSTRAINT [FK_TonKhos_HangHoas_MaHangHoa] FOREIGN KEY([MaHangHoa])
REFERENCES [dbo].[HangHoas] ([MaHangHoa])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TonKhos] CHECK CONSTRAINT [FK_TonKhos_HangHoas_MaHangHoa]
GO
ALTER TABLE [dbo].[TonKhos]  WITH CHECK ADD  CONSTRAINT [FK_TonKhos_KhoHangs_MaKho] FOREIGN KEY([MaKho])
REFERENCES [dbo].[KhoHangs] ([MaKho])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TonKhos] CHECK CONSTRAINT [FK_TonKhos_KhoHangs_MaKho]
GO
