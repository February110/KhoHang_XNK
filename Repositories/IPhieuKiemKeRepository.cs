using KhoHang_XNK.Models;

namespace KhoHang_XNK.Repositories
{
    public interface IPhieuKiemKeRepository
    {
        Task<IEnumerable<PhieuKiemKe>> GetAllAsync(); // Lấy danh sách phiếu kiểm kê
        Task<PhieuKiemKe> GetByIdAsync(int maKiemKe); // Lấy phiếu kiểm kê theo mã
        Task<IEnumerable<ChiTietPhieuKiemKe>> GetChiTietByPhieuKiemKeIdAsync(int maKiemKe); // Xem chi tiết phiếu kiểm kê
        Task AddAsync(PhieuKiemKe phieuKiemKe); // Thêm phiếu kiểm kê mới
        Task UpdateAsync(PhieuKiemKe phieuKiemKe); // Cập nhật phiếu kiểm kê
        Task DeleteAsync(int maKiemKe); // Xóa phiếu kiểm kê
        Task<IEnumerable<PhieuKiemKe>>? GetByKhoAsync(int maKho);

        Task<PhieuKiemKe?> GetPhieuKiemKeWithKhoAsync(int maKiemKe);

    }
}
