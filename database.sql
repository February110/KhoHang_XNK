USE [Database_KhoHangXNK]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 12/06/2025 1:41:03 AM ******/
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
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 12/06/2025 1:41:04 AM ******/
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
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 12/06/2025 1:41:04 AM ******/
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
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 12/06/2025 1:41:04 AM ******/
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
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 12/06/2025 1:41:04 AM ******/
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
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 12/06/2025 1:41:04 AM ******/
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
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 12/06/2025 1:41:04 AM ******/
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
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 12/06/2025 1:41:04 AM ******/
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
/****** Object:  Table [dbo].[ChiTietDonNhaps]    Script Date: 12/06/2025 1:41:04 AM ******/
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
/****** Object:  Table [dbo].[ChiTietDonXuats]    Script Date: 12/06/2025 1:41:04 AM ******/
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
/****** Object:  Table [dbo].[ChiTietPhieuKiemKes]    Script Date: 12/06/2025 1:41:04 AM ******/
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
/****** Object:  Table [dbo].[DonNhapHangs]    Script Date: 12/06/2025 1:41:04 AM ******/
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
/****** Object:  Table [dbo].[DonXuatHangs]    Script Date: 12/06/2025 1:41:04 AM ******/
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
/****** Object:  Table [dbo].[HangHoas]    Script Date: 12/06/2025 1:41:04 AM ******/
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
/****** Object:  Table [dbo].[KhachHangs]    Script Date: 12/06/2025 1:41:04 AM ******/
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
/****** Object:  Table [dbo].[KhoHangs]    Script Date: 12/06/2025 1:41:04 AM ******/
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
/****** Object:  Table [dbo].[LoaiHangHoas]    Script Date: 12/06/2025 1:41:04 AM ******/
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
/****** Object:  Table [dbo].[LoaiKhachHangs]    Script Date: 12/06/2025 1:41:04 AM ******/
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
/****** Object:  Table [dbo].[NhaCungCaps]    Script Date: 12/06/2025 1:41:04 AM ******/
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
/****** Object:  Table [dbo].[NhanViens]    Script Date: 12/06/2025 1:41:04 AM ******/
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
/****** Object:  Table [dbo].[PhieuKiemKes]    Script Date: 12/06/2025 1:41:04 AM ******/
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
/****** Object:  Table [dbo].[TonKhos]    Script Date: 12/06/2025 1:41:04 AM ******/
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
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250404163853_InitDatabase', N'9.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250406002746_avatarUser', N'9.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250407032147_thanhtoan', N'9.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250407045104_thanhtoandonxuat', N'9.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250515073421_Database_KhoHangXNK', N'9.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250515090622_Init2', N'9.0.3')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'1b4e503d-c2c9-4bb0-b4e1-1f7e9d83b338', N'User', N'USER', NULL)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'f6f224ca-b5ca-4fd9-a0eb-50438abc98cb', N'Admin', N'ADMIN', NULL)
GO
INSERT [dbo].[AspNetUserLogins] ([LoginProvider], [ProviderKey], [ProviderDisplayName], [UserId]) VALUES (N'Google', N'117418379995988460941', N'Google', N'f2a9559e-ebbd-42a3-99ca-6716fb66d5c4')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'3b2e99ba-b20b-42e0-aaeb-0fb267c61286', N'1b4e503d-c2c9-4bb0-b4e1-1f7e9d83b338')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'f2a9559e-ebbd-42a3-99ca-6716fb66d5c4', N'1b4e503d-c2c9-4bb0-b4e1-1f7e9d83b338')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'5b455120-373a-42d6-8efd-f717f565fbb7', N'f6f224ca-b5ca-4fd9-a0eb-50438abc98cb')
GO
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [AvatarUrl]) VALUES (N'3b2e99ba-b20b-42e0-aaeb-0fb267c61286', N'Default User', N'defaultuser@example.com', N'DEFAULTUSER@EXAMPLE.COM', N'defaultuser@example.com', N'DEFAULTUSER@EXAMPLE.COM', 0, N'AQAAAAIAAYagAAAAEAaWBvajbPjGK0NzsHcKzFk8lZ/6PXtIQIdRJkHIUXunXDI6R97bd+6NtDTFT+qoRA==', N'KVQ6FUKWJ4P3JQXRXIKL4VB3DQ2OHXNU', N'82cd2216-33d6-4159-aec6-785b525428f3', NULL, 0, 0, NULL, 1, 0, N'/images/avatars/default.png')
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [AvatarUrl]) VALUES (N'5b455120-373a-42d6-8efd-f717f565fbb7', N'Administrator', N'admin@example.com', N'ADMIN@EXAMPLE.COM', N'admin@example.com', N'ADMIN@EXAMPLE.COM', 0, N'AQAAAAIAAYagAAAAEPCRq6BYxJmqrnhhegxMy0WoIOzCxdB11ow6iXtqfIe4j3/ovqvanhaU6tHa1xwcBg==', N'IZNOKPLR5VQBTRTSQ5JXVBOCNWLWB6GR', N'5aebc186-5ad3-49b0-b448-259c48aecd4e', NULL, 0, 1, NULL, 1, 0, N'/images/avatars/default.png')
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [AvatarUrl]) VALUES (N'f2a9559e-ebbd-42a3-99ca-6716fb66d5c4', N'8567_Trần Anh Tài', N'anhtai1879@gmail.com', N'ANHTAI1879@GMAIL.COM', N'anhtai1879@gmail.com', N'ANHTAI1879@GMAIL.COM', 1, NULL, N'MROE2AYAHJ26G6FVLM2JLSTPG3ZPAAMG', N'accd5c14-543b-40e8-a3c1-c0cafd56957d', NULL, 0, 0, NULL, 1, 0, N'https://lh3.googleusercontent.com/a/ACg8ocLxAaj6e4LJDUl12o_8y9PZXhT-XE9Rq3f3iCSEC-ZAvvLPcqk=s96-c')
GO
INSERT [dbo].[AspNetUserTokens] ([UserId], [LoginProvider], [Name], [Value]) VALUES (N'5b455120-373a-42d6-8efd-f717f565fbb7', N'[AspNetUserStore]', N'AuthenticatorKey', N'A2JTF5ZYHO4XSHEEIIRN4N54RSAZRQUX')
INSERT [dbo].[AspNetUserTokens] ([UserId], [LoginProvider], [Name], [Value]) VALUES (N'5b455120-373a-42d6-8efd-f717f565fbb7', N'[AspNetUserStore]', N'RecoveryCodes', N'TFF4N-CV52H;CN2T9-RY66J;3QKTF-73H3T;RT3HG-RX6Q2;FCJK5-H6BXY;9BPP6-4DX7D;FGXDQ-XHYRJ;7W43X-TWBW3;Y4R88-K77YB;QK2Y7-TG5MN')
GO
INSERT [dbo].[ChiTietDonNhaps] ([MaDonNhap], [MaHangHoa], [SoLuong], [DonGia]) VALUES (1, 1, 100, CAST(125000.00 AS Decimal(18, 2)))
INSERT [dbo].[ChiTietDonNhaps] ([MaDonNhap], [MaHangHoa], [SoLuong], [DonGia]) VALUES (1, 2, 25, CAST(118000.00 AS Decimal(18, 2)))
INSERT [dbo].[ChiTietDonNhaps] ([MaDonNhap], [MaHangHoa], [SoLuong], [DonGia]) VALUES (1, 4, 50, CAST(180000.00 AS Decimal(18, 2)))
INSERT [dbo].[ChiTietDonNhaps] ([MaDonNhap], [MaHangHoa], [SoLuong], [DonGia]) VALUES (2, 5, 10, CAST(10000000.00 AS Decimal(18, 2)))
INSERT [dbo].[ChiTietDonNhaps] ([MaDonNhap], [MaHangHoa], [SoLuong], [DonGia]) VALUES (2, 8, 30, CAST(200000.00 AS Decimal(18, 2)))
INSERT [dbo].[ChiTietDonNhaps] ([MaDonNhap], [MaHangHoa], [SoLuong], [DonGia]) VALUES (4, 9, 20, CAST(70000.00 AS Decimal(18, 2)))
INSERT [dbo].[ChiTietDonNhaps] ([MaDonNhap], [MaHangHoa], [SoLuong], [DonGia]) VALUES (4, 11, 50, CAST(50000.00 AS Decimal(18, 2)))
INSERT [dbo].[ChiTietDonNhaps] ([MaDonNhap], [MaHangHoa], [SoLuong], [DonGia]) VALUES (6, 8, 200, CAST(150000.00 AS Decimal(18, 2)))
INSERT [dbo].[ChiTietDonNhaps] ([MaDonNhap], [MaHangHoa], [SoLuong], [DonGia]) VALUES (6, 12, 50, CAST(2000.00 AS Decimal(18, 2)))
INSERT [dbo].[ChiTietDonNhaps] ([MaDonNhap], [MaHangHoa], [SoLuong], [DonGia]) VALUES (7, 7, 5, CAST(15000000.00 AS Decimal(18, 2)))
INSERT [dbo].[ChiTietDonNhaps] ([MaDonNhap], [MaHangHoa], [SoLuong], [DonGia]) VALUES (7, 10, 50, CAST(70000.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[ChiTietDonXuats] ([MaDonXuat], [MaHangHoa], [SoLuong], [DonGia]) VALUES (2, 1, 25, CAST(150000.00 AS Decimal(18, 2)))
INSERT [dbo].[ChiTietDonXuats] ([MaDonXuat], [MaHangHoa], [SoLuong], [DonGia]) VALUES (3, 5, 3, CAST(12000000.00 AS Decimal(18, 2)))
INSERT [dbo].[ChiTietDonXuats] ([MaDonXuat], [MaHangHoa], [SoLuong], [DonGia]) VALUES (4, 2, 22, CAST(130000.00 AS Decimal(18, 2)))
INSERT [dbo].[ChiTietDonXuats] ([MaDonXuat], [MaHangHoa], [SoLuong], [DonGia]) VALUES (4, 4, 10, CAST(250000.00 AS Decimal(18, 2)))
INSERT [dbo].[ChiTietDonXuats] ([MaDonXuat], [MaHangHoa], [SoLuong], [DonGia]) VALUES (5, 4, 5, CAST(280000.00 AS Decimal(18, 2)))
INSERT [dbo].[ChiTietDonXuats] ([MaDonXuat], [MaHangHoa], [SoLuong], [DonGia]) VALUES (6, 8, 50, CAST(200000.00 AS Decimal(18, 2)))
INSERT [dbo].[ChiTietDonXuats] ([MaDonXuat], [MaHangHoa], [SoLuong], [DonGia]) VALUES (6, 12, 45, CAST(5000.00 AS Decimal(18, 2)))
INSERT [dbo].[ChiTietDonXuats] ([MaDonXuat], [MaHangHoa], [SoLuong], [DonGia]) VALUES (8, 7, 2, CAST(20000000.00 AS Decimal(18, 2)))
INSERT [dbo].[ChiTietDonXuats] ([MaDonXuat], [MaHangHoa], [SoLuong], [DonGia]) VALUES (8, 10, 10, CAST(100000.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[ChiTietPhieuKiemKes] ([MaKiemKe], [MaHangHoa], [SoLuongThucTe], [SoLuongChenhLech]) VALUES (2, 5, 7, 0)
INSERT [dbo].[ChiTietPhieuKiemKes] ([MaKiemKe], [MaHangHoa], [SoLuongThucTe], [SoLuongChenhLech]) VALUES (3, 11, 50, 0)
INSERT [dbo].[ChiTietPhieuKiemKes] ([MaKiemKe], [MaHangHoa], [SoLuongThucTe], [SoLuongChenhLech]) VALUES (5, 10, 40, 0)
GO
SET IDENTITY_INSERT [dbo].[DonNhapHangs] ON 

INSERT [dbo].[DonNhapHangs] ([MaDonNhap], [NgayNhap], [MaNCC], [MaKho], [MaNV], [trangthaithanhtoan]) VALUES (1, CAST(N'2025-05-16T00:00:00.0000000' AS DateTime2), 1, 2, 4, 1)
INSERT [dbo].[DonNhapHangs] ([MaDonNhap], [NgayNhap], [MaNCC], [MaKho], [MaNV], [trangthaithanhtoan]) VALUES (2, CAST(N'2025-05-16T00:00:00.0000000' AS DateTime2), 2, 3, 1, 1)
INSERT [dbo].[DonNhapHangs] ([MaDonNhap], [NgayNhap], [MaNCC], [MaKho], [MaNV], [trangthaithanhtoan]) VALUES (3, CAST(N'2025-05-28T00:00:00.0000000' AS DateTime2), 3, 3, 1, 0)
INSERT [dbo].[DonNhapHangs] ([MaDonNhap], [NgayNhap], [MaNCC], [MaKho], [MaNV], [trangthaithanhtoan]) VALUES (4, CAST(N'2025-05-29T00:00:00.0000000' AS DateTime2), 1, 3, 2, 1)
INSERT [dbo].[DonNhapHangs] ([MaDonNhap], [NgayNhap], [MaNCC], [MaKho], [MaNV], [trangthaithanhtoan]) VALUES (5, CAST(N'2025-05-29T00:00:00.0000000' AS DateTime2), 2, 2, 4, 0)
INSERT [dbo].[DonNhapHangs] ([MaDonNhap], [NgayNhap], [MaNCC], [MaKho], [MaNV], [trangthaithanhtoan]) VALUES (6, CAST(N'2025-06-12T00:00:00.0000000' AS DateTime2), 1, 2, 4, 1)
INSERT [dbo].[DonNhapHangs] ([MaDonNhap], [NgayNhap], [MaNCC], [MaKho], [MaNV], [trangthaithanhtoan]) VALUES (7, CAST(N'2025-06-12T00:00:00.0000000' AS DateTime2), 3, 3, 2, 1)
SET IDENTITY_INSERT [dbo].[DonNhapHangs] OFF
GO
SET IDENTITY_INSERT [dbo].[DonXuatHangs] ON 

INSERT [dbo].[DonXuatHangs] ([MaDonXuat], [NgayXuat], [MaKhachHang], [MaNV], [MaKho], [trangthaithanhtoan]) VALUES (2, CAST(N'2025-05-16T00:00:00.0000000' AS DateTime2), 1, 4, 2, 1)
INSERT [dbo].[DonXuatHangs] ([MaDonXuat], [NgayXuat], [MaKhachHang], [MaNV], [MaKho], [trangthaithanhtoan]) VALUES (3, CAST(N'2025-05-16T00:00:00.0000000' AS DateTime2), 2, 1, 3, 1)
INSERT [dbo].[DonXuatHangs] ([MaDonXuat], [NgayXuat], [MaKhachHang], [MaNV], [MaKho], [trangthaithanhtoan]) VALUES (4, CAST(N'2025-08-12T00:00:00.0000000' AS DateTime2), 2, 4, 2, 1)
INSERT [dbo].[DonXuatHangs] ([MaDonXuat], [NgayXuat], [MaKhachHang], [MaNV], [MaKho], [trangthaithanhtoan]) VALUES (5, CAST(N'2025-06-12T00:00:00.0000000' AS DateTime2), 1, 4, 2, 1)
INSERT [dbo].[DonXuatHangs] ([MaDonXuat], [NgayXuat], [MaKhachHang], [MaNV], [MaKho], [trangthaithanhtoan]) VALUES (6, CAST(N'2025-06-12T00:00:00.0000000' AS DateTime2), 2, 4, 2, 1)
INSERT [dbo].[DonXuatHangs] ([MaDonXuat], [NgayXuat], [MaKhachHang], [MaNV], [MaKho], [trangthaithanhtoan]) VALUES (8, CAST(N'2025-06-12T00:00:00.0000000' AS DateTime2), 2, 2, 3, 1)
SET IDENTITY_INSERT [dbo].[DonXuatHangs] OFF
GO
SET IDENTITY_INSERT [dbo].[HangHoas] ON 

INSERT [dbo].[HangHoas] ([MaHangHoa], [TenHangHoa], [MoTa], [DonViTinh], [HanSuDung], [ImageUrl], [MaLoaiHang]) VALUES (1, N'Gạo ST25', N'Gạo ST25', N'Kg', CAST(N'2030-05-05T00:00:00.0000000' AS DateTime2), N'/images/e04ba5b3-bccc-4a2e-bbf1-7812f37cd011_ST25-XANH-copy.jpg', 1)
INSERT [dbo].[HangHoas] ([MaHangHoa], [TenHangHoa], [MoTa], [DonViTinh], [HanSuDung], [ImageUrl], [MaLoaiHang]) VALUES (2, N'Mì gói Hảo Hảo', N'Mì gói Hảo Hảo', N'Thùng', CAST(N'2029-02-03T00:00:00.0000000' AS DateTime2), N'/images/8805f61a-44aa-407f-86e0-b938348fc068_bc807eb5970005ab95da5d73372ddd92.webp', 1)
INSERT [dbo].[HangHoas] ([MaHangHoa], [TenHangHoa], [MoTa], [DonViTinh], [HanSuDung], [ImageUrl], [MaLoaiHang]) VALUES (3, N'Nước suối Lavie', N'Nước suối Lavie', N'Thùng', CAST(N'2030-04-03T00:00:00.0000000' AS DateTime2), N'/images/1dfd67d2-60be-4416-b3de-eb047943ef82_Nuoc-Lavie-500ml.webp', 2)
INSERT [dbo].[HangHoas] ([MaHangHoa], [TenHangHoa], [MoTa], [DonViTinh], [HanSuDung], [ImageUrl], [MaLoaiHang]) VALUES (4, N'Nước ngọt Coca-Cola lon', N'Nước ngọt Coca-Cola lon', N'Thùng', CAST(N'2033-03-10T00:00:00.0000000' AS DateTime2), N'/images/b76eea27-14ce-4b30-bf85-46d7211fcfdc_vn-11134207-7ras8-m2ubwv1ie77q41@resize_w900_nl.webp', 2)
INSERT [dbo].[HangHoas] ([MaHangHoa], [TenHangHoa], [MoTa], [DonViTinh], [HanSuDung], [ImageUrl], [MaLoaiHang]) VALUES (5, N'Samsung Smart TV 55 inch', N'Samsung Smart TV 55 inch', N'Cái', CAST(N'2050-01-01T00:00:00.0000000' AS DateTime2), N'/images/b38fc2af-dded-47ab-85bc-b64e5b7cee08_tivi-samsung-4k-ua55au7700kxxv.jpg', 3)
INSERT [dbo].[HangHoas] ([MaHangHoa], [TenHangHoa], [MoTa], [DonViTinh], [HanSuDung], [ImageUrl], [MaLoaiHang]) VALUES (7, N'Máy ảnh Canon EOS M50', N'Máy ảnh Canon EOS M50', N'Cái', CAST(N'2029-02-03T00:00:00.0000000' AS DateTime2), N'/images/21b6124d-5b32-4698-96e6-426c8d06b219_images.jpg', 3)
INSERT [dbo].[HangHoas] ([MaHangHoa], [TenHangHoa], [MoTa], [DonViTinh], [HanSuDung], [ImageUrl], [MaLoaiHang]) VALUES (8, N'Nồi cơm điện Sharp', N'Nồi cơm điện Sharp', N'Cái', CAST(N'2051-10-21T00:00:00.0000000' AS DateTime2), N'/images/bf21612e-994c-450b-a236-b246713e0510_noi-com-dien-tu-sharp-18-lit-ks-com194ev-bk-thumb-600x600.jpg', 4)
INSERT [dbo].[HangHoas] ([MaHangHoa], [TenHangHoa], [MoTa], [DonViTinh], [HanSuDung], [ImageUrl], [MaLoaiHang]) VALUES (9, N'Sữa rửa mặt Senka', N'Sữa rửa mặt Senka', N'Cái', CAST(N'2045-10-01T00:00:00.0000000' AS DateTime2), N'/images/fff9a231-bd76-4038-b2b2-af033ac66945_f564e469-0691-4d09-9a38-77f95ab1d146.webp', 7)
INSERT [dbo].[HangHoas] ([MaHangHoa], [TenHangHoa], [MoTa], [DonViTinh], [HanSuDung], [ImageUrl], [MaLoaiHang]) VALUES (10, N'Dầu gội Dove', N'Dầu gội Dove', N'Cái', CAST(N'2040-10-01T00:00:00.0000000' AS DateTime2), N'/images/e2fb90d9-9910-4f71-ad53-067b052000b8_09_f57f4acecf9a48c594f8e7716e11a53e_1024x1024.webp', 7)
INSERT [dbo].[HangHoas] ([MaHangHoa], [TenHangHoa], [MoTa], [DonViTinh], [HanSuDung], [ImageUrl], [MaLoaiHang]) VALUES (11, N'Áo thun Uniqlo', N'Áo thun Uniqlo', N'Cái', CAST(N'2075-02-03T00:00:00.0000000' AS DateTime2), N'/images/ab7297d2-f2f0-46c1-8fee-e8518774efa4_db8f9999152227762ea1365f63d7b9f3.jpg', 5)
INSERT [dbo].[HangHoas] ([MaHangHoa], [TenHangHoa], [MoTa], [DonViTinh], [HanSuDung], [ImageUrl], [MaLoaiHang]) VALUES (12, N'Bút bi Thiên Long', N'Bút bi Thiên Long', N'Cái', CAST(N'2065-10-21T00:00:00.0000000' AS DateTime2), N'/images/36373eb7-3ccc-4611-9d1c-f96e430960de_tl027-1_1c30035a05d74ccbb3fa05713c5a309f.webp', 6)
SET IDENTITY_INSERT [dbo].[HangHoas] OFF
GO
SET IDENTITY_INSERT [dbo].[KhachHangs] ON 

INSERT [dbo].[KhachHangs] ([MaKH], [TenKH], [DiaChi], [SoDienThoai], [Email], [MaLoaiKH]) VALUES (1, N'Nguyễn Văn A', N'Hà Nội', N'039123456', N'khachhang1@gmail.com', 1)
INSERT [dbo].[KhachHangs] ([MaKH], [TenKH], [DiaChi], [SoDienThoai], [Email], [MaLoaiKH]) VALUES (2, N'Công ty A', N'Hải Phòng', N'028282828', N'khachhang2@gmail.com', 2)
INSERT [dbo].[KhachHangs] ([MaKH], [TenKH], [DiaChi], [SoDienThoai], [Email], [MaLoaiKH]) VALUES (3, N'Công ty B', N'Hải Phòng', N'0286812345', N'congtyb@gmail.com', 2)
SET IDENTITY_INSERT [dbo].[KhachHangs] OFF
GO
SET IDENTITY_INSERT [dbo].[KhoHangs] ON 

INSERT [dbo].[KhoHangs] ([MaKho], [TenKho], [DiaChi], [SucChua], [ImageUrl], [UserId]) VALUES (2, N'Kho Long Biên', N'Long Biên', 5000, N'/images/warehouses/ebf760a4-36a8-4d89-b5e3-358cce855c5a.jpg', N'f2a9559e-ebbd-42a3-99ca-6716fb66d5c4')
INSERT [dbo].[KhoHangs] ([MaKho], [TenKho], [DiaChi], [SucChua], [ImageUrl], [UserId]) VALUES (3, N'Kho Thủ Đức', N'Hồ Chí Minh', 6500, N'/images/warehouses/4e0ada57-c915-4456-9ec2-3113f5083f88.png', N'3b2e99ba-b20b-42e0-aaeb-0fb267c61286')
SET IDENTITY_INSERT [dbo].[KhoHangs] OFF
GO
SET IDENTITY_INSERT [dbo].[LoaiHangHoas] ON 

INSERT [dbo].[LoaiHangHoas] ([MaLoaiHang], [TenLoaiHang]) VALUES (1, N'Thực phẩm')
INSERT [dbo].[LoaiHangHoas] ([MaLoaiHang], [TenLoaiHang]) VALUES (2, N'Đồ uống')
INSERT [dbo].[LoaiHangHoas] ([MaLoaiHang], [TenLoaiHang]) VALUES (3, N'Điện tử')
INSERT [dbo].[LoaiHangHoas] ([MaLoaiHang], [TenLoaiHang]) VALUES (4, N'Gia dụng')
INSERT [dbo].[LoaiHangHoas] ([MaLoaiHang], [TenLoaiHang]) VALUES (5, N'Thời trang')
INSERT [dbo].[LoaiHangHoas] ([MaLoaiHang], [TenLoaiHang]) VALUES (6, N'Văn phòng phẩm')
INSERT [dbo].[LoaiHangHoas] ([MaLoaiHang], [TenLoaiHang]) VALUES (7, N'Mỹ phẩm')
INSERT [dbo].[LoaiHangHoas] ([MaLoaiHang], [TenLoaiHang]) VALUES (8, N'Đồ chơi')
INSERT [dbo].[LoaiHangHoas] ([MaLoaiHang], [TenLoaiHang]) VALUES (9, N'Vật liệu xây dựng')
SET IDENTITY_INSERT [dbo].[LoaiHangHoas] OFF
GO
SET IDENTITY_INSERT [dbo].[LoaiKhachHangs] ON 

INSERT [dbo].[LoaiKhachHangs] ([MaLoaiKH], [TenLoai]) VALUES (1, N'Khách lẻ')
INSERT [dbo].[LoaiKhachHangs] ([MaLoaiKH], [TenLoai]) VALUES (2, N'Doanh nghiệp')
SET IDENTITY_INSERT [dbo].[LoaiKhachHangs] OFF
GO
SET IDENTITY_INSERT [dbo].[NhaCungCaps] ON 

INSERT [dbo].[NhaCungCaps] ([MaNCC], [TenNCC], [DiaChi], [SoDienThoai], [Email]) VALUES (1, N'Nhà Cung Cấp 1', N'Long Biên', N'0248888888', N'ncc1@gmail.com')
INSERT [dbo].[NhaCungCaps] ([MaNCC], [TenNCC], [DiaChi], [SoDienThoai], [Email]) VALUES (2, N'Nhà Cung Cấp 2', N'Hồ Chí Minh', N'0268686868', N'ncc2@gmail.com')
INSERT [dbo].[NhaCungCaps] ([MaNCC], [TenNCC], [DiaChi], [SoDienThoai], [Email]) VALUES (3, N'Nhà Cung Cấp 3', N'Đà Nẵng', N'0212345678', N'ncc3@gmail.com')
SET IDENTITY_INSERT [dbo].[NhaCungCaps] OFF
GO
SET IDENTITY_INSERT [dbo].[NhanViens] ON 

INSERT [dbo].[NhanViens] ([MaNV], [HoTen], [NgaySinh], [ChucVu], [SoDienThoai], [Email], [MaKho], [ImageUrl]) VALUES (1, N'Trần Anh Tài', CAST(N'2004-10-01T00:00:00.0000000' AS DateTime2), N'Nhân Viên', N'0963803546', N'anhtai1879@gmail.com', 3, N'/images/3d2cdfbe-a1c1-492b-8fd8-c7271b378910_59788e0e-27e3-4c5c-b0dc-40c4c7c8bf74_avatar.png')
INSERT [dbo].[NhanViens] ([MaNV], [HoTen], [NgaySinh], [ChucVu], [SoDienThoai], [Email], [MaKho], [ImageUrl]) VALUES (2, N'Nguyễn Văn A', CAST(N'1209-02-02T00:00:00.0000000' AS DateTime2), N'Nhân Viên', N'0932503123', N'example@gmail.com', 3, N'/images/16a2fa3a-ad5f-4871-9f57-4f37ca08be1b_59788e0e-27e3-4c5c-b0dc-40c4c7c8bf74_avatar.png')
INSERT [dbo].[NhanViens] ([MaNV], [HoTen], [NgaySinh], [ChucVu], [SoDienThoai], [Email], [MaKho], [ImageUrl]) VALUES (4, N'Nguyễn Văn B', CAST(N'1999-05-04T00:00:00.0000000' AS DateTime2), N'Nhân Viên', N'098509425', N'example2@gmail.com', 2, N'/images/e09c78bd-d7e8-4570-9f0a-a4f0a7d57ec6_59788e0e-27e3-4c5c-b0dc-40c4c7c8bf74_avatar.png')
SET IDENTITY_INSERT [dbo].[NhanViens] OFF
GO
SET IDENTITY_INSERT [dbo].[PhieuKiemKes] ON 

INSERT [dbo].[PhieuKiemKes] ([MaKiemKe], [MaNV], [MaKho], [NgayKiemKe]) VALUES (2, 2, 3, CAST(N'2030-02-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[PhieuKiemKes] ([MaKiemKe], [MaNV], [MaKho], [NgayKiemKe]) VALUES (3, 2, 3, CAST(N'2025-05-29T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[PhieuKiemKes] ([MaKiemKe], [MaNV], [MaKho], [NgayKiemKe]) VALUES (4, 4, 2, CAST(N'2025-06-03T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[PhieuKiemKes] ([MaKiemKe], [MaNV], [MaKho], [NgayKiemKe]) VALUES (5, 1, 3, CAST(N'2025-06-01T00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[PhieuKiemKes] OFF
GO
INSERT [dbo].[TonKhos] ([MaKho], [MaHangHoa], [SoLuong]) VALUES (2, 1, 75)
INSERT [dbo].[TonKhos] ([MaKho], [MaHangHoa], [SoLuong]) VALUES (2, 2, 3)
INSERT [dbo].[TonKhos] ([MaKho], [MaHangHoa], [SoLuong]) VALUES (2, 4, 35)
INSERT [dbo].[TonKhos] ([MaKho], [MaHangHoa], [SoLuong]) VALUES (2, 8, 150)
INSERT [dbo].[TonKhos] ([MaKho], [MaHangHoa], [SoLuong]) VALUES (2, 12, 5)
INSERT [dbo].[TonKhos] ([MaKho], [MaHangHoa], [SoLuong]) VALUES (3, 5, 7)
INSERT [dbo].[TonKhos] ([MaKho], [MaHangHoa], [SoLuong]) VALUES (3, 7, 3)
INSERT [dbo].[TonKhos] ([MaKho], [MaHangHoa], [SoLuong]) VALUES (3, 8, 30)
INSERT [dbo].[TonKhos] ([MaKho], [MaHangHoa], [SoLuong]) VALUES (3, 9, 20)
INSERT [dbo].[TonKhos] ([MaKho], [MaHangHoa], [SoLuong]) VALUES (3, 10, 40)
INSERT [dbo].[TonKhos] ([MaKho], [MaHangHoa], [SoLuong]) VALUES (3, 11, 50)
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
