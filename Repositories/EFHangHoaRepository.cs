using KhoHang_XNK.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhoHang_XNK.Repositories
{
    public class EFHangHoaRepository : IHangHoaRepository
    {
        private readonly ApplicationDbContext _context;

        // Constructor to initialize the repository with the database context
        public EFHangHoaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all HangHoa items
        public async Task<IEnumerable<HangHoa>> GetAllAsync()
        {
            return await _context.HangHoas
                .Include(h => h.LoaiHangHoa)  // Assuming HangHoa has a navigation property for LoaiHangHoa
                .ToListAsync();
        }

        // Get HangHoa by ID
        public async Task<HangHoa> GetByIdAsync(int hangHoaId)
        {
            return await _context.HangHoas
                .Include(h => h.LoaiHangHoa)  // Include related LoaiHangHoa entity
                .FirstOrDefaultAsync(h => h.MaHangHoa == hangHoaId);  // Use 'MaHangHoa' or 'hangHoaId' based on naming conventions
        }

        // Add a new HangHoa item
        public async Task AddAsync(HangHoa hangHoa)
        {
            _context.HangHoas.Add(hangHoa);
            await _context.SaveChangesAsync();
        }

        // Update an existing HangHoa item
        public async Task UpdateAsync(HangHoa hangHoa)
        {
            _context.HangHoas.Update(hangHoa);
            await _context.SaveChangesAsync();
        }

        // Delete a HangHoa item by ID
        public async Task DeleteAsync(int hangHoaId)
        {
            var hangHoa = await GetByIdAsync(hangHoaId);
            if (hangHoa != null)
            {
                _context.HangHoas.Remove(hangHoa);
                await _context.SaveChangesAsync();
            }
        }

        // Get all ChiTietDonNhap (Receipt details) for a specific HangHoa
        public async Task<IEnumerable<ChiTietDonNhap>> GetDonNhapAsync(int hangHoaId)
        {
            return await _context.ChiTietDonNhaps
                .Where(c => c.MaHangHoa == hangHoaId)  // Filter by HangHoa ID
                .Include(c => c.DonNhapHang)  // Include related DonNhapHang entity
                .ToListAsync();
        }

        // Get all ChiTietDonXuat (Shipment details) for a specific HangHoa
        public async Task<IEnumerable<ChiTietDonXuat>> GetDonXuatAsync(int hangHoaId)
        {
            return await _context.ChiTietDonXuats
                .Where(c => c.MaHangHoa == hangHoaId)  // Filter by HangHoa ID
                .Include(c => c.DonXuatHang)  // Include related DonXuatHang entity
                .ToListAsync();
        }

        // Get all ChiTietPhieuKiemKe (Inventory check details) for a specific HangHoa
        public async Task<IEnumerable<ChiTietPhieuKiemKe>> GetPhieuKiemKeAsync(int hangHoaId)
        {
            return await _context.ChiTietPhieuKiemKes
                .Where(c => c.MaHangHoa == hangHoaId)  // Filter by HangHoa ID
                .Include(c => c.PhieuKiemKe)  // Include related PhieuKiemKe entity
                .ToListAsync();
        }

        // Get the total stock quantity for a specific HangHoa
        public async Task<int?> GetTonKhoAsync(int hangHoaId)
        {
            var tonKho = await _context.TonKhos
                .Where(t => t.MaHangHoa == hangHoaId)  // Filter by HangHoa ID
                .SumAsync(t => t.SoLuong);  // Sum the quantity from all TonKho records

            return tonKho > 0 ? tonKho : (int?)null;  // Return null if no stock or zero
        }

        public async Task<IEnumerable<SelectListItem>> GetHangHoaByKhoAsync(int maKho)
        {
        return await _context.TonKhos
            .Where(t => t.MaKho == maKho && t.SoLuong > 0)
            .Join(_context.HangHoas,
                tonKho => tonKho.MaHangHoa,
                hangHoa => hangHoa.MaHangHoa,
                (tonKho, hangHoa) => new SelectListItem
                {
                    Value = hangHoa.MaHangHoa.ToString(),
                    Text = hangHoa.TenHangHoa
                })
            .ToListAsync();
        }
    }
}
