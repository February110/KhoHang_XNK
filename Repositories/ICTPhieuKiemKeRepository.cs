using KhoHang_XNK.Models;

namespace KhoHang_XNK.Repositories
{
    public interface ICTPhieuKiemKeRepository
    {
        Task<IEnumerable<ChiTietPhieuKiemKe>> GetAllByPhieuKiemKeIdAsync(int maKiemKe); // Get details by inventory check ID
        Task<IEnumerable<ChiTietPhieuKiemKe>> GetAllAsync(); // Get all details
        Task<ChiTietPhieuKiemKe> GetByIdAsync(int maKiemKe, int maHangHoa); // Get detail by inventory check ID and product ID
        Task AddOrUpdateAsync(ChiTietPhieuKiemKe chiTietPhieuKiemKe); // Add or update detail
        Task DeleteAsync(int maKiemKe, int maHangHoa); 
    }
}
