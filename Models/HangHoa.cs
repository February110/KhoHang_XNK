using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KhoHang_XNK.Models
{
    public class HangHoa
    {
        [Key]
        public int MaHangHoa { get; set; }

        [Required, StringLength(100)]
        public string TenHangHoa { get; set; }

        public string MoTa { get; set; }
        public string DonViTinh { get; set; }
        public DateTime HanSuDung { get; set; }
        public string? ImageUrl { get; set; }

        [ForeignKey("LoaiHangHoa")]  // 🎯 Chỉ định đây là khóa ngoại
        public int MaLoaiHang { get; set; }

        public LoaiHangHoa? LoaiHangHoa { get; set; }

    }
}
