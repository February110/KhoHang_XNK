namespace KhoHang_XNK.Models
{
    public class NhanVienDetailsViewModel
    {
        public NhanVien NhanVien { get; set; }
        public IEnumerable<DonNhapHang> DonNhapHangs { get; set; }
        public IEnumerable<DonXuatHang> DonXuatHangs { get; set; }
        public IEnumerable<PhieuKiemKe> PhieuKiemKes { get; set; }
    }
}
