using KhoHang_XNK.Models;

namespace KhoHang_XNK.Repositories
{
    public interface ICTDonXuatRepository
    {

        Task<ChiTietDonXuat> GetChiTietDonXuatByDonXuatAndHangHoa(int maDonXuat, int maHangHoa);
        Task AddAsync(ChiTietDonXuat chiTietDonXuat);
        Task UpdateAsync(ChiTietDonXuat chiTietDonXuat);
        Task DeleteAsync(ChiTietDonXuat chiTietDonXuat);
        Task<IEnumerable<ChiTietDonXuat>> GetAllAsync();
        Task<ChiTietDonXuat> GetByIdAsync(int maDonXuat, int maHangHoa);

        Task<IEnumerable<ChiTietDonXuat>> GetByHangHoaAsync();
    }
}
