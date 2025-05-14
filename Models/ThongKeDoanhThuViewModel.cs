namespace KhoHang_XNK.Models
{
    public class DoanhThuThangDto
    {
        public int? KhoId { get; set; } 
        public int Nam { get; set; }
        public int Thang { get; set; }
        public decimal TongTien { get; set; }
    }
    public class LaiSuatThangDto
    {
        public int Thang { get; set; }
        public int Nam { get; set; }
        public decimal LaiSuat { get; set; }  // Đơn vị: %
    }
    public class ThongKeDoanhThuViewModel
    {
        public int? SelectedKhoId { get; set; }
        public List<KhoHang> DanhSachKho { get; set; }

        public IEnumerable<DoanhThuThangDto> DoanhThuNhapHang { get; set; }
        public IEnumerable<DoanhThuThangDto> DoanhThuXuatHang { get; set; }

        public IEnumerable<LaiSuatThangDto> LaiSuatHangThang { get; set; }

    }


}
