using KhoHang_XNK.Models;

namespace KhoHang_XNK.Repositories
{
    public interface IDonXuatHangRepository
    {
        Task<IEnumerable<DonXuatHang>> GetAllAsync();  // Lấy danh sách đơn xuất
        Task<DonXuatHang?> GetByIdAsync(int id);       // Lấy đơn xuất theo ID
        Task AddAsync(DonXuatHang donXuatHang);        // Thêm mới đơn xuất
        Task UpdateAsync(DonXuatHang donXuatHang);     // Cập nhật đơn xuất
        Task DeleteAsync(int id);                      // Xóa đơn xuất
        Task<IEnumerable<ChiTietDonXuat>> GetChiTietByDonXuatIdAsync(int id);
        Task<int> GetKhoByMaDonXuatAsync(int madon);
        Task<IEnumerable<DonXuatHang>>? GetByKhoAsync(int maKho);

        Task<IEnumerable<DoanhThuThangDto>>  GetTongTienXuatTheoThangAsync();
    }
}
