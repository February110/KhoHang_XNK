using KhoHang_XNK.Models;
using Microsoft.EntityFrameworkCore;

namespace KhoHang_XNK.Repositories
{
    public class EFCTDonNhapRepository : ICTDonNhapRepository
    {
        private readonly ApplicationDbContext _context;
        public EFCTDonNhapRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        // Lấy tất cả chi tiết đơn nhập
        public async Task<IEnumerable<ChiTietDonNhap>> GetAllAsync()
        {
            return await _context.ChiTietDonNhaps
                .Include(ct => ct.DonNhapHang)
                .Include(ct => ct.HangHoa)
                .ToListAsync();
        }

        // Lấy chi tiết đơn nhập theo MaDonNhap và MaHangHoa
        public async Task<ChiTietDonNhap> GetByIdAsync(int maDonNhap, int maHangHoa)
        {
            return await _context.ChiTietDonNhaps
                .Include(ct => ct.DonNhapHang)
                .Include(ct => ct.HangHoa)
                .FirstOrDefaultAsync(ct => ct.MaDonNhap == maDonNhap && ct.MaHangHoa == maHangHoa);
        }

        // Thêm chi tiết đơn nhập mới
        public async Task AddAsync(ChiTietDonNhap chiTietDonNhap)
        {
            await _context.ChiTietDonNhaps.AddAsync(chiTietDonNhap);
            await _context.SaveChangesAsync();
        }

        // Cập nhật chi tiết đơn nhập
        public async Task UpdateAsync(ChiTietDonNhap chiTietDonNhap)
        {
            _context.ChiTietDonNhaps.Update(chiTietDonNhap);
            await _context.SaveChangesAsync();
        }

        // Xóa chi tiết đơn nhập
        public async Task DeleteAsync(int maDonNhap, int maHangHoa)
        {
            var chiTietDonNhap = await GetByIdAsync(maDonNhap, maHangHoa);
            if (chiTietDonNhap != null)
            {
                _context.ChiTietDonNhaps.Remove(chiTietDonNhap);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<ChiTietDonNhap> GetChiTietDonNhapByDonNhapAndHangHoa(int maDonNhap, int maHangHoa)
        {
            return await _context.ChiTietDonNhaps
                .FirstOrDefaultAsync(c => c.MaDonNhap == maDonNhap && c.MaHangHoa == maHangHoa);
        }
    }
}
