using KhoHang_XNK.Models;

namespace KhoHang_XNK.Repositories
{
    public interface INhaCungCapRepository
    {
        Task<List<NhaCungCap>> GetAllNhaCungCapsAsync(); // Lấy tất cả nhà cung cấp
        Task<NhaCungCap> GetNhaCungCapByIdAsync(int maNCC); // Lấy nhà cung cấp theo ID
        Task AddNhaCungCapAsync(NhaCungCap nhaCungCap); // Thêm mới nhà cung cấp
        Task UpdateNhaCungCapAsync(NhaCungCap nhaCungCap); // Cập nhật nhà cung cấp
        Task DeleteNhaCungCapAsync(int maNCC); // Xóa nhà cung cấp
    }
}
