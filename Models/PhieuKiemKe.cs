using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KhoHang_XNK.Models
{
    public class PhieuKiemKe
    {
        [Key]
        public int MaKiemKe { get; set; }

        [ForeignKey("NhanVien")]
        public int MaNV { get; set; }

        [ForeignKey("KhoHang")]
        public int MaKho { get; set; }

        [Required]
        public DateTime NgayKiemKe { get; set; }

        public NhanVien NhanVien { get; set; }
        public KhoHang KhoHang { get; set; }
    }
}
