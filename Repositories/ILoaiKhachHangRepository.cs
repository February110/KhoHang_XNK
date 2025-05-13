using KhoHang_XNK.Models;

namespace KhoHang_XNK.Repositories
{
    public interface ILoaiKhachHangRepository
    {
        Task<List<LoaiKhachHang>> GetAllLoaiKhachHangsAsync(); // Lấy tất cả loại khách hàng
        Task<LoaiKhachHang> GetLoaiKhachHangByIdAsync(int maLoaiKH); // Lấy loại khách hàng theo ID
        Task AddLoaiKhachHangAsync(LoaiKhachHang loaiKhachHang); // Thêm mới loại khách hàng
        Task UpdateLoaiKhachHangAsync(LoaiKhachHang loaiKhachHang); // Cập nhật loại khách hàng
        Task DeleteLoaiKhachHangAsync(int maLoaiKH);
    }
}
