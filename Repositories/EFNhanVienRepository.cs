using KhoHang_XNK.Models;
using Microsoft.EntityFrameworkCore;

namespace KhoHang_XNK.Repositories
{
    public class EFNhanVienRepository : INhanVienRepository
    {
        private readonly ApplicationDbContext _context;
        public EFNhanVienRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NhanVien>> GetAllAsync()
        {
            return await _context.NhanViens
                                 .Include(nv => nv.KhoHang) // Load kho hàng nếu có
                                 .ToListAsync();
        }

        public async Task<NhanVien> GetByIdAsync(int maNV)
        {
            return await _context.NhanViens
                                 .Include(nv => nv.KhoHang) // Load kho hàng nếu có
                                 .FirstOrDefaultAsync(nv => nv.MaNV == maNV);
        }

        public async Task AddAsync(NhanVien nhanVien)
        {
            await _context.NhanViens.AddAsync(nhanVien);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(NhanVien nhanVien)
        {
            _context.NhanViens.Update(nhanVien);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int maNV)
        {
            var nhanVien = await _context.NhanViens.FindAsync(maNV);
            if (nhanVien != null)
            {
                _context.NhanViens.Remove(nhanVien);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<DonNhapHang>> GetDonNhapByNhanVienAsync(int maNV)
        {
            return await _context.DonNhapHangs
                                 .Where(dn => dn.MaNV == maNV)
                                 .Include(dn => dn.NhanVien)
                                 .Include(dn => dn.NhaCungCap)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<DonXuatHang>> GetDonXuatByNhanVienAsync(int maNV)
        {
            return await _context.DonXuatHangs
                                 .Where(dx => dx.MaNV == maNV)
                                 .Include(dx => dx.NhanVien)
                                 .Include(dx => dx.KhachHang)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<PhieuKiemKe>> GetPhieuKiemKeByNhanVienAsync(int maNV)
        {
            return await _context.PhieuKiemKes
                                 .Where(pk => pk.MaNV == maNV)
                                 .Include(pk => pk.NhanVien)
                                 .Include(pk => pk.KhoHang)
                                 .ToListAsync();
        }


        public async Task<IEnumerable<NhanVien>> GetByKhoAsync(int maKho)
        {
            return await _context.NhanViens
                                 .Where(nv => nv.MaKho == maKho)
                                 .Include(nv => nv.KhoHang) // Load thông tin kho
                                 .ToListAsync();
        }
        public async Task<IEnumerable<NhanVien>> GetNhanVienByKhoHang(int maKho)
        {
            return await _context.NhanViens
                .Where(nv => nv.MaKho == maKho)
                .Include(nv => nv.KhoHang) // Load thông tin kho
                .ToListAsync();
        }


    }
}
