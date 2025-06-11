using KhoHang_XNK.Models;
using Microsoft.EntityFrameworkCore;

namespace KhoHang_XNK.Repositories
{
    public class EFCTDonXuatRepository : ICTDonXuatRepository
    {
        private readonly ApplicationDbContext _context;

        public EFCTDonXuatRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Phương thức để kiểm tra nếu ChiTietDonXuat đã tồn tại theo MaDonXuat và MaHangHoa
        public async Task<ChiTietDonXuat> GetChiTietDonXuatByDonXuatAndHangHoa(int maDonXuat, int maHangHoa)
        {
            return await _context.ChiTietDonXuats
                .FirstOrDefaultAsync(c => c.MaDonXuat == maDonXuat && c.MaHangHoa == maHangHoa);
        }

        // Phương thức thêm chi tiết đơn xuất vào cơ sở dữ liệu
        public async Task AddAsync(ChiTietDonXuat chiTietDonXuat)
        {
            await _context.ChiTietDonXuats.AddAsync(chiTietDonXuat);
            await _context.SaveChangesAsync();
        }

        // Phương thức cập nhật chi tiết đơn xuất trong cơ sở dữ liệu
        public async Task UpdateAsync(ChiTietDonXuat chiTietDonXuat)
        {
            _context.ChiTietDonXuats.Update(chiTietDonXuat);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int maDonXuat, int maHangHoa)
        {
            var chiTietDonXuat = await GetByIdAsync(maDonXuat, maHangHoa);
            if (chiTietDonXuat != null)
            {
                _context.ChiTietDonXuats.Remove(chiTietDonXuat);
                await _context.SaveChangesAsync();
            }
            
        }

        // Phương thức lấy tất cả chi tiết đơn xuất
        public async Task<IEnumerable<ChiTietDonXuat>> GetAllAsync()
        {
            return await _context.ChiTietDonXuats
                 .Include(ct => ct.DonXuatHang)
                .Include(ct => ct.HangHoa)
                .ToListAsync();
        }
        public async Task<IEnumerable<ChiTietDonXuat>> GetByHangHoaAsync()
        {
            return await _context.ChiTietDonXuats
                .Include(ct => ct.HangHoa)
                .ToListAsync();
        }
        public async Task<IEnumerable<ChiTietDonXuat>> GetByHangHoaAndKhoAsync(int maKho)
        {
            return await _context.ChiTietDonXuats
                .Include(ct => ct.HangHoa)
                .Include(ct => ct.DonXuatHang)
                .Where(ct => ct.DonXuatHang.KhoHang.MaKho == maKho)
                .ToListAsync();
        }
        public async Task<ChiTietDonXuat> GetByIdAsync(int maDonXuat, int maHangHoa)
        {
            return await _context.ChiTietDonXuats
                .Include(ct => ct.DonXuatHang)
                .Include(ct => ct.HangHoa)
                .FirstOrDefaultAsync(c => c.MaDonXuat == maDonXuat && c.MaHangHoa == maHangHoa);
        }


        public async Task<int> GetKhoByMaDonXuatAsync(int madon)
        {
            var donXuat = await _context.DonXuatHangs.FindAsync(madon);
            if (donXuat != null)
            {
                return donXuat.MaKho;
            }
            return 0;

        }
    }
}
