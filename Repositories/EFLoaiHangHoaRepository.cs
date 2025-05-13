using KhoHang_XNK.Models;
using Microsoft.EntityFrameworkCore;

namespace KhoHang_XNK.Repositories
{
    public class EFLoaiHangHoaRepository : ILoaiHangHoaRepository
    {
        private readonly ApplicationDbContext _context;
        public EFLoaiHangHoaRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<LoaiHangHoa>> GetAllAsync()
        {
            return await _context.LoaiHangHoas.Include(c => c.HangHoas).ToListAsync();
        }
        public async Task<LoaiHangHoa> GetByIdAsync(int MaLoaiHang)
        {
           return await _context.LoaiHangHoas
            .Include(l => l.HangHoas) 
            .FirstOrDefaultAsync(m => m.MaLoaiHang == MaLoaiHang);
        }
        public async Task AddAsync(LoaiHangHoa loaiHangHoa)
        {
            _context.LoaiHangHoas.Add(loaiHangHoa);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(LoaiHangHoa loaiHangHoa)
        {
            _context.LoaiHangHoas.Update(loaiHangHoa);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var loaiHangHoa = await GetByIdAsync(id);
            _context.LoaiHangHoas.Remove(loaiHangHoa);
            await _context.SaveChangesAsync();
        }
    }
}
