namespace KhoHang_XNK.Models
{
    public class DoanhThuThangDto
    {
        public int? KhoId { get; set; }  // Thêm dòng này nếu chưa có
        public int Nam { get; set; }
        public int Thang { get; set; }
        public decimal TongTien { get; set; }
    }
    public class ThongKeDoanhThuViewModel
    {
        public int? SelectedKhoId { get; set; }
        public List<KhoHang> DanhSachKho { get; set; }

        // Chỉ dùng 2 biến dưới cho biểu đồ (dữ liệu đã lọc sẵn theo SelectedKhoId hoặc không lọc nếu null)
        public IEnumerable<DoanhThuThangDto> DoanhThuNhapHang { get; set; }
        public IEnumerable<DoanhThuThangDto> DoanhThuXuatHang { get; set; }

        //public IEnumerable<DoanhThuThangDto> DoanhThuNhapTheoKho { get; set; } // Bổ sung lại dòng này
        //public IEnumerable<DoanhThuThangDto> DoanhThuXuatTheoKho { get; set; }
    }


}
