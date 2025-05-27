using KhoHang_XNK.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KhoHang_XNK.Repositories
{
    public interface IHangHoaRepository
    {
        Task<IEnumerable<HangHoa>> GetAllAsync();
        Task<HangHoa> GetByIdAsync(int hangHoaId);  // Changed to 'hangHoaId' for clarity
        Task AddAsync(HangHoa hangHoa);
        Task UpdateAsync(HangHoa hangHoa);
        Task DeleteAsync(int hangHoaId);  // Changed to 'hangHoaId'

        Task<IEnumerable<ChiTietDonNhap>> GetDonNhapAsync(int hangHoaId);  // Renamed for consistency
        Task<IEnumerable<ChiTietDonXuat>> GetDonXuatAsync(int hangHoaId);  // Renamed for consistency
        Task<IEnumerable<ChiTietPhieuKiemKe>> GetPhieuKiemKeAsync(int hangHoaId);  // Renamed for consistency
        Task<int?> GetTonKhoAsync(int hangHoaId);  // Changed return type to 'int?' for optional values

        Task<IEnumerable<SelectListItem>> GetHangHoaByKhoAsync(int maKho);
    }
}
