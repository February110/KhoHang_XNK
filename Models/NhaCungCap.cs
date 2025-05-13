using System.ComponentModel.DataAnnotations;

namespace KhoHang_XNK.Models
{
    public class NhaCungCap
    {
        [Key]
        public int MaNCC { get; set; }

        [Required]
        [StringLength(255)]
        public string TenNCC { get; set; }

        [StringLength(255)]
        public string? DiaChi { get; set; }

        [StringLength(15)]
        public string? SoDienThoai { get; set; }

        [StringLength(100)]
        public string? Email { get; set; }
    }
}
