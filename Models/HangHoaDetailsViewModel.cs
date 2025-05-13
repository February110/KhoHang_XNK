namespace KhoHang_XNK.Models
{
    public class HangHoaDetailsViewModel
    {
        public HangHoa HangHoa { get; set; }
        public IEnumerable<ChiTietDonNhap> DonNhapHangs { get; set; }
        public IEnumerable<ChiTietDonXuat> DonXuatHangs { get; set; }
        public IEnumerable<ChiTietPhieuKiemKe> PhieuKiemKes { get; set; }
        public int TonKho { get; set; }
    }
}
