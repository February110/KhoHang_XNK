using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KhoHang_XNK.Models
{
    public class ChiTietDonXuat
    {
        [Key, Column(Order = 0)]  // Khóa chính phần đầu tiên trong composite key
        public int MaDonXuat { get; set; }

        [Key, Column(Order = 1)]  // Khóa chính phần thứ hai trong composite key
        public int MaHangHoa { get; set; }

        [Range(1, int.MaxValue)]  // Đảm bảo số lượng phải lớn hơn hoặc bằng 1
        public int SoLuong { get; set; }

        [Range(0.01, double.MaxValue)]  // Đảm bảo đơn giá phải lớn hơn 0
        public decimal DonGia { get; set; }

        // Điều hướng đến DonXuatHang (mối quan hệ 1-n)
        public DonXuatHang DonXuatHang { get; set; }

        // Điều hướng đến HangHoa (mối quan hệ 1-n)
        public HangHoa HangHoa { get; set; }
    }
}
