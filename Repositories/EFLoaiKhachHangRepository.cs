using KhoHang_XNK.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhoHang_XNK.Repositories
{
    public class EFLoaiKhachHangRepository : ILoaiKhachHangRepository
    {
        private readonly ApplicationDbContext _context;

        public EFLoaiKhachHangRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lấy tất cả loại khách hàng
        public async Task<List<LoaiKhachHang>> GetAllLoaiKhachHangsAsync()
        {
            return await _context.LoaiKhachHangs
                                 .Include(lkh => lkh.KhachHangs) // Nếu muốn bao gồm danh sách khách hàng đi kèm
                                 .ToListAsync();
        }

        // Lấy loại khách hàng theo ID
        public async Task<LoaiKhachHang> GetLoaiKhachHangByIdAsync(int maLoaiKH)
        {
            return await _context.LoaiKhachHangs
                                 .Include(lkh => lkh.KhachHangs) // Bao gồm danh sách khách hàng đi kèm
                                 .FirstOrDefaultAsync(lkh => lkh.MaLoaiKH == maLoaiKH);
        }

        // Thêm mới loại khách hàng
        public async Task AddLoaiKhachHangAsync(LoaiKhachHang loaiKhachHang)
        {
            await _context.LoaiKhachHangs.AddAsync(loaiKhachHang);
            await _context.SaveChangesAsync();
        }

        // Cập nhật loại khách hàng
        public async Task UpdateLoaiKhachHangAsync(LoaiKhachHang loaiKhachHang)
        {
            _context.LoaiKhachHangs.Update(loaiKhachHang);
            await _context.SaveChangesAsync();
        }

        // Xóa loại khách hàng
        public async Task DeleteLoaiKhachHangAsync(int maLoaiKH)
        {
            var loaiKhachHang = await _context.LoaiKhachHangs.FindAsync(maLoaiKH);
            if (loaiKhachHang != null)
            {
                _context.LoaiKhachHangs.Remove(loaiKhachHang);
                await _context.SaveChangesAsync();
            }
        }
    }
}
