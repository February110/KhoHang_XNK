using KhoHang_XNK.Models;
using Microsoft.EntityFrameworkCore;

namespace KhoHang_XNK.Repositories
{
    public class EFKhachHangRepository : IKhachHangRepository
    {
        private readonly ApplicationDbContext _context;

        public EFKhachHangRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<KhachHang>> GetAllAsync()
        {
            return await _context.KhachHangs.Include(kh => kh.LoaiKhachHang).ToListAsync();
        }

        public async Task<KhachHang> GetByIdAsync(int id)
        {
            return await _context.KhachHangs
                .Include(kh => kh.LoaiKhachHang)
                .FirstOrDefaultAsync(kh => kh.MaKH == id);
        }

        public async Task AddAsync(KhachHang khachHang)
        {
            _context.KhachHangs.Add(khachHang);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(KhachHang khachHang)
        {
            _context.KhachHangs.Update(khachHang);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var khachHang = await _context.KhachHangs.FindAsync(id);
            if (khachHang != null)
            {
                _context.KhachHangs.Remove(khachHang);
                await _context.SaveChangesAsync();
            }
        }

        // ✅ Thêm phương thức lấy danh sách đơn xuất hàng của khách hàng
        public async Task<IEnumerable<DonXuatHang>> GetDonXuatHangByKhachHangIdAsync(int khachHangId)
        {
            return await _context.DonXuatHangs
                .Where(dx => dx.MaKhachHang == khachHangId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ChiTietDonXuat>> GetChiTietDonXuatByKhachHangIdAsync(int khachHangId)
        {
            return await _context.ChiTietDonXuats
                .Where(ct => _context.DonXuatHangs
                    .Where(dx => dx.MaKhachHang == khachHangId) // Lấy danh sách đơn xuất của khách hàng
                    .Select(dx => dx.MaDonXuat)              // Lấy danh sách mã đơn xuất
                    .Contains(ct.MaDonXuat))                 // Lọc theo mã đơn xuất
                .Include(ct => ct.HangHoa)               // Bao gồm thông tin sản phẩm
                .Include(ct => ct.DonXuatHang)           // Bao gồm thông tin đơn xuất hàng
                .ToListAsync();
        }
    }
}
