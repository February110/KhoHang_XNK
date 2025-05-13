using System.ComponentModel.DataAnnotations;

namespace KhoHang_XNK.Models
{
    public class TonKho
    {
        [Key]
        public int MaKho { get; set; }
        [Key]
        public int MaHangHoa { get; set; }

        [Range(0, int.MaxValue)]
        public int SoLuong { get; set; }

        public KhoHang KhoHang { get; set; }
        public HangHoa HangHoa { get; set; }
    }
}
