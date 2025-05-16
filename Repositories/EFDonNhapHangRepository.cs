using DocumentFormat.OpenXml.Office2010.Excel;
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

        public async Task<IEnumerable<DoanhThuThangDto>> GetTongTienNhapTheoThangAsync(int? khoId = null)
        {
            var query = _context.ChiTietDonNhaps
                .Include(c => c.DonNhapHang)
                .AsQueryable();

            // Thêm điều kiện lọc nếu có khoId
            if (khoId.HasValue)
            {
                query = query.Where(x => x.DonNhapHang.MaKho == khoId.Value);

                // Nhóm theo kho khi chọn một kho cụ thể
                return await query
                    .GroupBy(c => new
                    {
                        c.DonNhapHang.MaKho,
                        Year = c.DonNhapHang.NgayNhap.Year,
                        Month = c.DonNhapHang.NgayNhap.Month
                    })
                    .Select(g => new DoanhThuThangDto
                    {
                        KhoId = g.Key.MaKho,
                        Nam = g.Key.Year,
                        Thang = g.Key.Month,
                        TongTien = g.Sum(x => x.SoLuong * x.DonGia)
                    })
                    .OrderBy(x => x.Nam)
                    .ThenBy(x => x.Thang)
                    .ToListAsync();
            }
            else
            {
                // Khi chọn "Tất cả kho", chỉ nhóm theo tháng và năm (không nhóm theo kho)
                return await query
                    .GroupBy(c => new
                    {
                        Year = c.DonNhapHang.NgayNhap.Year,
                        Month = c.DonNhapHang.NgayNhap.Month
                    })
                    .Select(g => new DoanhThuThangDto
                    {
                        KhoId = 0, // Giá trị mặc định hoặc null
                        Nam = g.Key.Year,
                        Thang = g.Key.Month,
                        TongTien = g.Sum(x => x.SoLuong * x.DonGia)
                    })
                    .OrderBy(x => x.Nam)
                    .ThenBy(x => x.Thang)
                    .ToListAsync();
            }
        }
    }
}
