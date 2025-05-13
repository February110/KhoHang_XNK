using KhoHang_XNK.Models;

namespace KhoHang_XNK.Repositories
{
    public interface IDonNhapHangRepository
    {
        Task<IEnumerable<DonNhapHang>> GetAllAsync();  // Lấy danh sách đơn nhập
        Task<DonNhapHang?> GetByIdAsync(int id);       // Lấy đơn nhập theo ID
        Task AddAsync(DonNhapHang donNhapHang);        // Thêm mới đơn nhập
        Task UpdateAsync(DonNhapHang donNhapHang);     // Cập nhật đơn nhập
        Task DeleteAsync(int id);                      // Xóa đơn nhập
        Task<IEnumerable<ChiTietDonNhap>> GetChiTietByDonNhapIdAsync(int id); // Lấy ch
        Task <int> GetKhoByMaDonNhapAsync(int madon);
        Task<IEnumerable<DonNhapHang>>? GetByKhoAsync(int maKho);

        Task<IEnumerable<DoanhThuThangDto>> GetTongTienNhapTheoThangAsync(int? khoId = null);
    }
}
