using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KhoHang_XNK.Models
{
    public class NhanVien
    {
        [Key]
        public int MaNV { get; set; }

        [Required, StringLength(255)]
        public string HoTen { get; set; }

        public DateTime? NgaySinh { get; set; }

        [StringLength(100)]
        public string? ChucVu { get; set; }

        [StringLength(15)]
        public string? SoDienThoai { get; set; }

        [StringLength(100)]
        [EmailAddress]
        public string? Email { get; set; }

        [ForeignKey("KhoHang")]
        public int? MaKho { get; set; }
        public KhoHang? KhoHang { get; set; }

        public string? ImageUrl { get; set; }
    }
}
