using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KhoHang_XNK.Models
{
    public class ChiTietPhieuKiemKe
    {
        [Key, Column(Order = 0)] // Khóa chính phần đầu tiên trong composite key
        public int MaKiemKe { get; set; }

        [Key, Column(Order = 1)] // Khóa chính phần thứ hai trong composite key
        public int MaHangHoa { get; set; }

        public int SoLuongThucTe { get; set; }
        public int? SoLuongChenhLech { get; set; }

        // Điều hướng đến PhieuKiemKe (mối quan hệ 1-n)
        public PhieuKiemKe PhieuKiemKe { get; set; }

        // Điều hướng đến HangHoa (mối quan hệ 1-n)
        public HangHoa HangHoa { get; set; }
    }
}
