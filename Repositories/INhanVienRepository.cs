using KhoHang_XNK.Models;
using System.Collections;

namespace KhoHang_XNK.Repositories
{
    public interface INhanVienRepository
    {
        Task<IEnumerable<NhanVien>> GetAllAsync();
        Task<NhanVien> GetByIdAsync(int maNV);
        Task AddAsync(NhanVien nhanVien);
        Task UpdateAsync(NhanVien nhanVien);
        Task DeleteAsync(int maNV);
        Task<IEnumerable<NhanVien>> GetByKhoAsync(int maKho);
        Task<IEnumerable<DonNhapHang>> GetDonNhapByNhanVienAsync(int maNV);
        Task<IEnumerable<DonXuatHang>> GetDonXuatByNhanVienAsync(int maNV);
        Task<IEnumerable<PhieuKiemKe>> GetPhieuKiemKeByNhanVienAsync(int maNV);


    }
}
