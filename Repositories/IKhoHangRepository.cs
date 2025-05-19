using KhoHang_XNK.Models;

namespace KhoHang_XNK.Repositories
{
    public interface IKhoHangRepository
    {

        Task<IEnumerable<KhoHang>> GetAllKhoHangsAsync();
        Task<KhoHang> GetKhoHangByIdAsync(int id);
        Task AddKhoHangAsync(KhoHang khoHang);
        Task UpdateKhoHangAsync(KhoHang khoHang);
        Task DeleteKhoHangAsync(int id);

        // Phương thức mới để lấy kho hàng và hàng hóa cùng số lượng tồn kho
        Task<IEnumerable<object>> GetHangHoaAndTonKhoByMaKhoAsync(int maKho);

        Task<IEnumerable<KhoHang>> GetAllKhoHangsForUserAsync(string userId);
        Task<KhoHang> GetKhoHangByIdForUserAsync(int id);  // Dành cho user, chỉ lấy kho của người dùng hiện tại
        Task AddKhoHangForUserAsync(KhoHang khoHang);  // Dành cho user, thêm kho cho người dùng hiện tại
        Task UpdateKhoHangForUserAsync(KhoHang khoHang);  // Dành cho user, cập nhật kho của người dùng hiện tại
        Task DeleteKhoHangForUserAsync(int id);  // Dành ch

        Task<KhoHang> GetKhoHangByIdUser(string id);

    }
}
