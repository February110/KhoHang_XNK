using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KhoHang_XNK.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<LoaiHangHoa> LoaiHangHoas { get; set; }
        public DbSet<HangHoa> HangHoas { get; set; }
        public DbSet<DonXuatHang> DonXuatHangs { get; set; }
        public DbSet<ChiTietDonXuat> ChiTietDonXuats { get; set; }
        public DbSet<DonNhapHang> DonNhapHangs { get; set; }
        public DbSet<ChiTietDonNhap> ChiTietDonNhaps { get; set; }
        public DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<KhoHang> KhoHangs { get; set; }
        public DbSet<TonKho> TonKhos { get; set; }
        public DbSet<PhieuKiemKe> PhieuKiemKes { get; set; }
        public DbSet<ChiTietPhieuKiemKe> ChiTietPhieuKiemKes { get; set; }
        public DbSet<LoaiKhachHang> LoaiKhachHangs { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define composite primary key for ChiTietDonNhap
            modelBuilder.Entity<ChiTietDonNhap>()
                .HasKey(c => new { c.MaDonNhap, c.MaHangHoa });

            // Define composite primary key for ChiTietDonXuat
            modelBuilder.Entity<ChiTietDonXuat>()
                .HasKey(c => new { c.MaDonXuat, c.MaHangHoa });

            // Define composite primary key for ChiTietPhieuKiemKe
            modelBuilder.Entity<ChiTietPhieuKiemKe>()
                .HasKey(c => new { c.MaKiemKe, c.MaHangHoa });
            modelBuilder.Entity<TonKho>()
              .HasKey(t => new { t.MaKho, t.MaHangHoa });

            modelBuilder.Entity<KhoHang>()
             .HasOne(kh => kh.User)  // Mối quan hệ 1-1 với bảng AspNetUsers
             .WithMany()  // Nếu không có mối quan hệ ngược lại
             .HasForeignKey(kh => kh.UserId)  // Cột UserId là khóa ngoại
             .OnDelete(DeleteBehavior.Cascade); // Thiết lập hành 


        }

    }
}
