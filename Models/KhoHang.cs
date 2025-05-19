using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KhoHang_XNK.Models
{
    public class KhoHang
    {
        [Key]
        public int MaKho { get; set; }

        [Required, StringLength(255)]
        public string TenKho { get; set; }

        public string? DiaChi { get; set; }

        public int SucChua { get; set; }

        public string? ImageUrl { get; set; }
        // Thêm thuộc tính UserId
        public string? UserId { get; set; }  // Khóa ngoại tới AspNetUsers

        // Thuộc tính điều hướng (navigation property) tới bảng AspNetUsers
        public ApplicationUser User { get; set; }
        public List<NhanVien>? NhanViens { get; set; } 
        public List<TonKho>? TonKhos { get; set; }
    }
}
