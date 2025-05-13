using KhoHang_XNK.Models;
using Microsoft.EntityFrameworkCore;

namespace KhoHang_XNK.Repositories
{
    public class EFDonXuatHangRepository : IDonXuatHangRepository
    {
        private readonly ApplicationDbContext _context;
        public EFDonXuatHangRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<DonXuatHang>> GetAllAsync()
        {
            return await _context.DonXuatHangs
                .Include(d => d.KhachHang)
                .Include(d => d.NhanVien)
                .Include(d => d.KhoHang)
                .ToListAsync();
        }

        // Lấy đơn xuất hàng theo ID
        public async Task<DonXuatHang?> GetByIdAsync(int id)
        {
            return await _context.DonXuatHangs
                .Include(d => d.KhachHang)
                .Include(d => d.NhanVien)
                .Include(d => d.KhoHang)
                .FirstOrDefaultAsync(d => d.MaDonXuat == id);
        }

        // Thêm đơn xuất hàng
        public async Task AddAsync(DonXuatHang donXuatHang)
        {
            await _context.DonXuatHangs.AddAsync(donXuatHang);
            await _context.SaveChangesAsync();
        }

        // Cập nhật đơn xuất hàng
        public async Task UpdateAsync(DonXuatHang donXuatHang)
        {
            _context.DonXuatHangs.Update(donXuatHang);
            await _context.SaveChangesAsync();
        }

        // Xóa đơn xuất hàng
        public async Task DeleteAsync(int id)
        {
            var donXuatHang = await _context.DonXuatHangs.FindAsync(id);
            if (donXuatHang != null)
            {
                _context.DonXuatHangs.Remove(donXuatHang);
                await _context.SaveChangesAsync();
            }
        }

        // Lấy danh sách chi tiết đơn xuất hàng dựa vào Mã Đơn Xuất
        public async Task<IEnumerable<ChiTietDonXuat>> GetChiTietByDonXuatIdAsync(int id)
        {
            return await _context.ChiTietDonXuats
                .Where(ct => ct.MaDonXuat == id)
                .Include(ct => ct.HangHoa)
                .ToListAsync();
        }

        public async Task<int> GetKhoByMaDonXuatAsync(int madon)
        {
            var donXuat = await GetByIdAsync(madon);
            return donXuat.KhoHang.MaKho;
        }
        public async Task<IEnumerable<DonXuatHang>> GetByKhoAsync(int maKho)
        {
            return await _context.DonXuatHangs
                .Include(d => d.KhachHang)
                .Include(d => d.NhanVien)
                .Include(d => d.KhoHang)
                .Where(d => d.KhoHang.MaKho == maKho)
                .ToListAsync();
        }

        //public async Task<IEnumerable<DoanhThuThangDto>> GetTongTienXuatTheoThangAsync()
        //{
        //    return await _context.ChiTietDonXuats
        //        .Include(c => c.DonXuatHang) // Phải include để truy cập NgayXuat
        //        .GroupBy(c => new { c.DonXuatHang.NgayXuat.Year, c.DonXuatHang.NgayXuat.Month})
        //        .Select(g => new DoanhThuThangDto
        //        {
        //            Nam = g.Key.Year,
        //            Thang = g.Key.Month,
        //            TongTien = g.Sum(x => x.SoLuong * x.DonGia)
        //        })
        //        .OrderBy(x => x.Nam)
        //        .ThenBy(x => x.Thang)
        //        .ToListAsync();
        //}
        // Lấy tổng tiền XUẤT theo tháng và theo từng kho
        public async Task<IEnumerable<DoanhThuThangDto>> GetTongTienXuatTheoThangAsync(int? khoId = null)
        {
            var query = _context.ChiTietDonXuats
                .Include(c => c.DonXuatHang)
                .AsQueryable();

            // Thêm điều kiện lọc nếu có khoId
            if (khoId.HasValue)
            {
                query = query.Where(x => x.DonXuatHang.MaKho == khoId.Value);
            }

            return await query
                .GroupBy(c => new
                {
                    c.DonXuatHang.MaKho,
                    Year = c.DonXuatHang.NgayXuat.Year,
                    Month = c.DonXuatHang.NgayXuat.Month
                })
                .Select(g => new DoanhThuThangDto
                {
                    KhoId = g.Key.MaKho,
                    Nam = g.Key.Year,
                    Thang = g.Key.Month,
                    TongTien = g.Sum(x => x.SoLuong * x.DonGia)
                })
                .OrderBy(x => x.KhoId)
                .ThenBy(x => x.Nam)
                .ThenBy(x => x.Thang)
                .ToListAsync();
        }



    }
}
