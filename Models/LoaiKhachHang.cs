using System.ComponentModel.DataAnnotations;

namespace KhoHang_XNK.Models
{
    public class LoaiKhachHang
    {
        [Key]
        public int MaLoaiKH { get; set; }

        [Required]
        [StringLength(255)]
        public string TenLoai { get; set; }

        public List<KhachHang> KhachHangs { get; set; }
    }
}
