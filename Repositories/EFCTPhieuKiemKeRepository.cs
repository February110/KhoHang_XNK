using KhoHang_XNK.Models;
using Microsoft.EntityFrameworkCore;

namespace KhoHang_XNK.Repositories
{
    public class EFCTPhieuKiemKeRepository : ICTPhieuKiemKeRepository
    {
        private readonly ApplicationDbContext _context;
        public EFCTPhieuKiemKeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all details by the inventory check ID
        public async Task<IEnumerable<ChiTietPhieuKiemKe>> GetAllByPhieuKiemKeIdAsync(int maKiemKe)
        {
            return await _context.ChiTietPhieuKiemKes
                                 .Where(c => c.MaKiemKe == maKiemKe)
                                 .Include(c => c.HangHoa)
                                 .Include(c => c.PhieuKiemKe)
                                 .ToListAsync();
        }

        // Get all details
        public async Task<IEnumerable<ChiTietPhieuKiemKe>> GetAllAsync()
        {
            return await _context.ChiTietPhieuKiemKes
                                 .Include(c => c.HangHoa)
                                 .Include(c => c.PhieuKiemKe)
                                 .ToListAsync();
        }

        // Get a specific detail by inventory check ID and product ID
        public async Task<ChiTietPhieuKiemKe> GetByIdAsync(int maKiemKe, int maHangHoa)
        {
            return await _context.ChiTietPhieuKiemKes
                                 .Where(c => c.MaKiemKe == maKiemKe && c.MaHangHoa == maHangHoa)
                                 .Include(c => c.HangHoa)
                                 .Include(c => c.PhieuKiemKe)
                                 .FirstOrDefaultAsync();
        }

        // Add or update detail
        public async Task AddOrUpdateAsync(ChiTietPhieuKiemKe chiTietPhieuKiemKe)
        {
            var existingDetail = await _context.ChiTietPhieuKiemKes
                                               .FirstOrDefaultAsync(c => c.MaKiemKe == chiTietPhieuKiemKe.MaKiemKe && c.MaHangHoa == chiTietPhieuKiemKe.MaHangHoa);

            if (existingDetail != null)
            {
                // Update existing detail
                existingDetail.SoLuongThucTe = chiTietPhieuKiemKe.SoLuongThucTe;
                existingDetail.SoLuongChenhLech = chiTietPhieuKiemKe.SoLuongChenhLech;
                _context.ChiTietPhieuKiemKes.Update(existingDetail);
            }
            else
            {
                // Add new detail
                await _context.ChiTietPhieuKiemKes.AddAsync(chiTietPhieuKiemKe);
            }

            await _context.SaveChangesAsync();
        }

        // Delete a specific detail by inventory check ID and product ID
        public async Task DeleteAsync(int maKiemKe, int maHangHoa)
        {
            var detail = await _context.ChiTietPhieuKiemKes
                                       .FirstOrDefaultAsync(c => c.MaKiemKe == maKiemKe && c.MaHangHoa == maHangHoa);

            if (detail != null)
            {
                _context.ChiTietPhieuKiemKes.Remove(detail);
                await _context.SaveChangesAsync();
            }
        }

    }
}
