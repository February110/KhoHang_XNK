using KhoHang_XNK.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhoHang_XNK.Repositories
{
    public class EFTonKhoRepository : ITonKhoRepository
    {
        private readonly ApplicationDbContext _context;

        public EFTonKhoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TonKho>> GetAllTonKhosAsync()
        {
            return await _context.TonKhos
                .Include(tk => tk.KhoHang)
                .Include(tk => tk.HangHoa)
                .ToListAsync();
        }

        public async Task<TonKho> GetTonKhoByIdsAsync(int maKho, int maHangHoa)
        {
            return await _context.TonKhos
                .Include(tk => tk.KhoHang)
                .Include(tk => tk.HangHoa)
                .FirstOrDefaultAsync(tk => tk.MaKho == maKho && tk.MaHangHoa == maHangHoa);
        }

        public async Task AddTonKhoAsync(TonKho tonKho)
        {
            _context.TonKhos.Add(tonKho);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTonKhoAsync(TonKho tonKho)
        {
            _context.TonKhos.Update(tonKho);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTonKhoAsync(int maKho, int maHangHoa)
        {
            var tonKho = await GetTonKhoByIdsAsync(maKho, maHangHoa);
            if (tonKho != null)
            {
                _context.TonKhos.Remove(tonKho);
                await _context.SaveChangesAsync();
            }
        }

        // Get all TonKho records by MaKho, including HangHoa details
        public async Task<IEnumerable<TonKho>> GetTonKhosByMaKhoAsync(int maKho)
        {
            return await _context.TonKhos
                .Include(tk => tk.HangHoa) 
                .Include(tk => tk.KhoHang)  // Include KhoHang details
                .Where(tk => tk.MaKho == maKho) // Filter by MaKho
                .ToListAsync();
        }

        // Get all TonKho records by MaHangHoa, including KhoHang details
        public async Task<IEnumerable<TonKho>> GetTonKhosByMaHangHoaAsync(int maHangHoa)
        {
            return await _context.TonKhos
                .Include(tk => tk.KhoHang)  // Include KhoHang details
                .Where(tk => tk.MaHangHoa == maHangHoa) // Filter by MaHangHoa
                .ToListAsync();
        }

        // Return stock details along with HangHoa information for a specific MaKho
        public async Task<IEnumerable<object>> GetHangHoaAndTonKhoByMaKhoAsync(int maKho)
        {
            return await _context.TonKhos
                .Where(tk => tk.MaKho == maKho) // Filter by MaKho
                .Include(tk => tk.HangHoa)  // Include HangHoa details
                .Select(tk => new
                {
                    TenHangHoa = tk.HangHoa.TenHangHoa,
                    SoLuongTon = tk.SoLuong,
                    ImageUrl = tk.HangHoa.ImageUrl,
                    MaKho = tk.MaKho // MaKho from TonKho
                })
                .ToListAsync();
        }
        public async Task<IEnumerable<HangHoa>> GetHangHoasByMaKhoAsync(int maKho)
        {
            return await _context.TonKhos
                .Where(tk => tk.MaKho == maKho)
                .Include(tk => tk.HangHoa)
                .Select(tk => tk.HangHoa)
                .Distinct()
                .ToListAsync();
        }
    }

}
