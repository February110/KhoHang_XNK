using KhoHang_XNK.Models;
using Microsoft.EntityFrameworkCore;

namespace KhoHang_XNK.Repositories
{
    public class EFNhaCungCapRepository : INhaCungCapRepository
    {
        private readonly ApplicationDbContext _context;
        public EFNhaCungCapRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<NhaCungCap>> GetAllNhaCungCapsAsync()
        {
            return await _context.NhaCungCaps.ToListAsync();
        }

        // Lấy nhà cung cấp theo ID
        public async Task<NhaCungCap> GetNhaCungCapByIdAsync(int maNCC)
        {
            return await _context.NhaCungCaps
                                 .Where(ncc => ncc.MaNCC == maNCC)
                                 .FirstOrDefaultAsync();
        }

        // Thêm mới nhà cung cấp
        public async Task AddNhaCungCapAsync(NhaCungCap nhaCungCap)
        {
            await _context.NhaCungCaps.AddAsync(nhaCungCap);
            await _context.SaveChangesAsync();
        }

        // Cập nhật nhà cung cấp
        public async Task UpdateNhaCungCapAsync(NhaCungCap nhaCungCap)
        {
            _context.NhaCungCaps.Update(nhaCungCap);
            await _context.SaveChangesAsync();
        }

        // Xóa nhà cung cấp
        public async Task DeleteNhaCungCapAsync(int maNCC)
        {
            var nhaCungCap = await _context.NhaCungCaps.FindAsync(maNCC);
            if (nhaCungCap != null)
            {
                _context.NhaCungCaps.Remove(nhaCungCap);
                await _context.SaveChangesAsync();
            }
        }
    }
}
