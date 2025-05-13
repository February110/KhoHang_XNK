using KhoHang_XNK.Models;

namespace KhoHang_XNK.Repositories
{
    public interface IKhachHangRepository
    {
        Task<IEnumerable<KhachHang>> GetAllAsync();
        Task<KhachHang> GetByIdAsync(int id);
        Task AddAsync(KhachHang khachHang);
        Task UpdateAsync(KhachHang khachHang);
        Task DeleteAsync(int id);
        Task<IEnumerable<DonXuatHang>> GetDonXuatHangByKhachHangIdAsync(int khachHangId);

        Task<IEnumerable<ChiTietDonXuat>> GetChiTietDonXuatByKhachHangIdAsync(int khachHangId);


    }
}
