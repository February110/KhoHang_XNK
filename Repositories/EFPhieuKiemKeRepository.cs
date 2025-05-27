using KhoHang_XNK.Models;
using Microsoft.EntityFrameworkCore;

namespace KhoHang_XNK.Repositories
{
    public class EFPhieuKiemKeRepository : IPhieuKiemKeRepository
    {
        private readonly ApplicationDbContext _context;
        public EFPhieuKiemKeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        // Lấy danh sách phiếu kiểm kê
        public async Task<IEnumerable<PhieuKiemKe>> GetAllAsync()
        {
            return await _context.PhieuKiemKes
                .Include(p => p.NhanVien)
                .Include(p => p.KhoHang)
                .ToListAsync();
        }

        // Lấy phiếu kiểm kê theo mã
        public async Task<PhieuKiemKe> GetByIdAsync(int maKiemKe)
        {
            return await _context.PhieuKiemKes
                .Include(p => p.NhanVien)
                .Include(p => p.KhoHang)
                .FirstOrDefaultAsync(p => p.MaKiemKe == maKiemKe);
        }

        // Lấy chi tiết phiếu kiểm kê theo mã phiếu kiểm kê
        public async Task<IEnumerable<ChiTietPhieuKiemKe>> GetChiTietByPhieuKiemKeIdAsync(int maKiemKe)
        {
            return await _context.ChiTietPhieuKiemKes
                .Where(c => c.MaKiemKe == maKiemKe)
                .Include(c => c.HangHoa)
                .ToListAsync();
        }

        // Thêm phiếu kiểm kê mới
        public async Task AddAsync(PhieuKiemKe phieuKiemKe)
        {
            _context.PhieuKiemKes.Add(phieuKiemKe);
            await _context.SaveChangesAsync();
        }

        // Cập nhật phiếu kiểm kê
        public async Task UpdateAsync(PhieuKiemKe phieuKiemKe)
        {
            _context.PhieuKiemKes.Update(phieuKiemKe);
            await _context.SaveChangesAsync();
        }

        // Xóa phiếu kiểm kê
        public async Task DeleteAsync(int maKiemKe)
        {
            var phieuKiemKe = await _context.PhieuKiemKes.FindAsync(maKiemKe);
            if (phieuKiemKe != null)
            {
                _context.PhieuKiemKes.Remove(phieuKiemKe);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<PhieuKiemKe>> GetByKhoAsync(int maKho)
        {
            return await _context.PhieuKiemKes
                .Where(p => p.MaKho == maKho)
                .Include(p => p.NhanVien)
                .Include(p => p.KhoHang)
                .ToListAsync();
        }

        public async Task<PhieuKiemKe?> GetPhieuKiemKeWithKhoAsync(int maKiemKe)
        {
            return await _context.PhieuKiemKes
                .Include(p => p.KhoHang)
                .FirstOrDefaultAsync(p => p.MaKiemKe == maKiemKe);
        }

        public async Task<int?> GetMaKhoByKiemKeAsync(int maKiemKe)
        {
            return await _context.PhieuKiemKes
                .Where(p => p.MaKiemKe == maKiemKe)
                .Select(p => p.MaKho)
                .FirstOrDefaultAsync();
        }
    }
}
