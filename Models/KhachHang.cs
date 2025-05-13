using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KhoHang_XNK.Models
{
    public class KhachHang
    {
        [Key]
        public int MaKH { get; set; }

        [Required]
        [StringLength(255)]
        public string TenKH { get; set; }

        [StringLength(255)]
        public string? DiaChi { get; set; }

        [StringLength(15)]
        public string? SoDienThoai { get; set; }

        [StringLength(100)]
        public string? Email { get; set; }

        [ForeignKey("LoaiKhachHang")]
        public int MaLoaiKH { get; set; }

        public LoaiKhachHang LoaiKhachHang { get; set; }  
    }
}
