using KhoHang_XNK.Models;

namespace KhoHang_XNK.Repositories
{
    public interface ILoaiHangHoaRepository
    {
        Task<IEnumerable<LoaiHangHoa>> GetAllAsync();
        Task<LoaiHangHoa> GetByIdAsync(int MaLoaiHang);
        Task AddAsync(LoaiHangHoa loaiHangHoa);
        Task UpdateAsync(LoaiHangHoa loaiHangHoa);
        Task DeleteAsync(int MaLoaiHang);
    }
}
