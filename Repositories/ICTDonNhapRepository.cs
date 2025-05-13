using KhoHang_XNK.Models;
using Microsoft.EntityFrameworkCore;

namespace KhoHang_XNK.Repositories
{
    public interface ICTDonNhapRepository
    {
        Task<IEnumerable<ChiTietDonNhap>> GetAllAsync();
        Task<ChiTietDonNhap> GetByIdAsync(int maDonNhap, int maHangHoa);
        Task AddAsync(ChiTietDonNhap chiTietDonNhap);
        Task UpdateAsync(ChiTietDonNhap chiTietDonNhap);
        Task DeleteAsync(int maDonNhap, int maHangHoa);

        Task<ChiTietDonNhap> GetChiTietDonNhapByDonNhapAndHangHoa(int maDonNhap, int maHangHoa);
    }
}
