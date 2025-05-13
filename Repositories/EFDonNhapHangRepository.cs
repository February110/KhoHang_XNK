using KhoHang_XNK.Models;
using Microsoft.EntityFrameworkCore;

namespace KhoHang_XNK.Repositories
{
    public class EFDonNhapHangRepository : IDonNhapHangRepository
    {
        private readonly ApplicationDbContext _context;
        public EFDonNhapHangRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DonNhapHang>> GetAllAsync()
        {
            return await _context.DonNhapHangs
                .Include(d => d.NhaCungCap)
                .Include(d => d.NhanVien)
                .Include(d => d.KhoHang)
                .ToListAsync();
        }

        public async Task<DonNhapHang?> GetByIdAsync(int id)
        {
            return await _context.DonNhapHangs
                .Include(d => d.NhaCungCap)
                .Include(d => d.NhanVien)
                .Include(d => d.KhoHang)
                .FirstOrDefaultAsync(d => d.MaDonNhap == id);
        }

        public async Task AddAsync(DonNhapHang donNhapHang)
        {
            await _context.DonNhapHangs.AddAsync(donNhapHang);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DonNhapHang donNhapHang)
        {
            _context.DonNhapHangs.Update(donNhapHang);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var donNhapHang = await GetByIdAsync(id);
            if (donNhapHang != null)
            {
                _context.DonNhapHangs.Remove(donNhapHang);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ChiTietDonNhap>> GetChiTietByDonNhapIdAsync(int id)
        {
            return await _context.ChiTietDonNhaps
                .Where(ct => ct.MaDonNhap == id)
                .Include(ct => ct.HangHoa) // Load thông tin hàng hóa
                .ToListAsync();
        }

        public async Task<IEnumerable<NhanVien>> GetAllNhanVien(int maKho)
        {
            return await _context.NhanViens.Where(ct => ct.MaKho == maKho).ToListAsync();
        }

        public async Task<int> GetKhoByMaDonNhapAsync(int madon)
        {
            var donNhap = await GetByIdAsync(madon);
            return donNhap.KhoHang.MaKho;
        }
        public async Task<IEnumerable<DonNhapHang>> GetByKhoAsync(int maKho)
        {
            return await _context.DonNhapHangs
                .Where(d => d.KhoHang.MaKho == maKho)
                .Include(d => d.NhaCungCap)
                .Include(d => d.NhanVien)
                .Include(d => d.KhoHang)
                .ToListAsync();
        }

        public async Task<IEnumerable<DoanhThuThangDto>> GetTongTienNhapTheoThangAsync()
        {
            return await _context.ChiTietDonNhaps
                .Include(c => c.DonNhapHang)
                .GroupBy(c => new { c.DonNhapHang.NgayNhap.Year, c.DonNhapHang.NgayNhap.Month })
                .Select(g => new DoanhThuThangDto
                {
                    Nam = g.Key.Year,
                    Thang = g.Key.Month,
                    TongTien = g.Sum(x => x.SoLuong * x.DonGia)
                })
                .OrderBy(x => x.Nam).ThenBy(x => x.Thang)
                .ToListAsync();
        }

    }
}
