using KhoHang_XNK.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace KhoHang_XNK.Repositories
{
    public class EFKhoHangRepository : IKhoHangRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EFKhoHangRepository(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        #region User-Specific Operations
        public async Task<IEnumerable<KhoHang>> GetAllKhoHangsForUserAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("User is not logged in.");

            return await _context.KhoHangs
                .Where(k => k.UserId == userId)
                .Include(k => k.NhanViens)
                .Include(k => k.TonKhos)
                    .ThenInclude(tk => tk.HangHoa)
                .ToListAsync();
        }

        public async Task<KhoHang> GetKhoHangByIdForUserAsync(int id)
        {
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("User is not logged in.");

            return await _context.KhoHangs
                .Where(k => k.MaKho == id && k.UserId == userId)
                .Include(k => k.NhanViens)
                .Include(k => k.TonKhos)
                    .ThenInclude(tk => tk.HangHoa)
                .FirstOrDefaultAsync();
        }

        public async Task AddKhoHangForUserAsync(KhoHang khoHang)
        {
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("User is not logged in.");

            khoHang.UserId = userId;
            _context.KhoHangs.Add(khoHang);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateKhoHangForUserAsync(KhoHang khoHang)
        {
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("User is not logged in.");

            var existingKho = await _context.KhoHangs
                .FirstOrDefaultAsync(k => k.MaKho == khoHang.MaKho);

            if (existingKho == null)
                throw new KeyNotFoundException("Warehouse not found");

            if (existingKho.UserId != userId)
                throw new UnauthorizedAccessException("You can only update your own warehouses");

            _context.Entry(existingKho).CurrentValues.SetValues(khoHang);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteKhoHangForUserAsync(int id)
        {
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("User is not logged in.");

            var khoHang = await _context.KhoHangs
                .FirstOrDefaultAsync(k => k.MaKho == id && k.UserId == userId);

            if (khoHang == null)
                throw new UnauthorizedAccessException("Warehouse not found or doesn't belong to you");

            _context.KhoHangs.Remove(khoHang);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Admin Operations
        public async Task<IEnumerable<KhoHang>> GetAllKhoHangsAsync()
        {
            return await _context.KhoHangs
                .Include(k => k.User)
                .Include(k => k.NhanViens)
                .Include(k => k.TonKhos)
                    .ThenInclude(tk => tk.HangHoa)
                .ToListAsync();
        }

        public async Task<KhoHang> GetKhoHangByIdAsync(int id)
        {
            return await _context.KhoHangs
                .Include(k => k.User)
                .Include(k => k.NhanViens)
                .Include(k => k.TonKhos)
                    .ThenInclude(tk => tk.HangHoa)
                .FirstOrDefaultAsync(k => k.MaKho == id);
        }

        public async Task AddKhoHangAsync(KhoHang khoHang)
        {
            _context.KhoHangs.Add(khoHang);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateKhoHangAsync(KhoHang khoHang)
        {
            _context.KhoHangs.Update(khoHang);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteKhoHangAsync(int id)
        {
            var khoHang = await _context.KhoHangs.FindAsync(id);
            if (khoHang != null)
            {
                _context.KhoHangs.Remove(khoHang);
                await _context.SaveChangesAsync();
            }
        }
        #endregion

        #region Inventory Operations
        public async Task<IEnumerable<object>> GetHangHoaAndTonKhoByMaKhoAsync(int maKho)
        {
            return await _context.TonKhos
                .Where(tk => tk.MaKho == maKho)
                .Include(tk => tk.HangHoa)
                .Select(tk => new
                {
                    MaHangHoa = tk.HangHoa.MaHangHoa,
                    TenHangHoa = tk.HangHoa.TenHangHoa,
                    SoLuongTon = tk.SoLuong,
                    DonViTinh = tk.HangHoa.DonViTinh,
                    ImageUrl = tk.HangHoa.ImageUrl,
                    MaKho = tk.MaKho
                })
                .ToListAsync();
        }
        #endregion
    }
}