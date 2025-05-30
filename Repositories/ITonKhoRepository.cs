﻿using KhoHang_XNK.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KhoHang_XNK.Repositories
{
    public interface ITonKhoRepository
    {
        Task<IEnumerable<TonKho>> GetAllTonKhosAsync();
        Task<TonKho> GetTonKhoByIdsAsync(int maKho, int maHangHoa);
        Task AddTonKhoAsync(TonKho tonKho);
        Task UpdateTonKhoAsync(TonKho tonKho);
        Task DeleteTonKhoAsync(int maKho, int maHangHoa);
        Task<IEnumerable<TonKho>>? GetTonKhosByMaKhoAsync(int maKho);
        Task<IEnumerable<TonKho>> GetTonKhosByMaHangHoaAsync(int maHangHoa);
        Task<IEnumerable<object>> GetHangHoaAndTonKhoByMaKhoAsync(int maKho);

        Task<IEnumerable<HangHoa>>? GetHangHoasByMaKhoAsync(int maKho);
        Task<TonKho> GetByMaHangHoa(int maHangHoa);

        Task<TonKho?> GetTonKhoByMaKhoAndMaHangHoaAsync(int maKho, int maHangHoa);
        Task<IEnumerable<SelectListItem>> GetHangHoaByKhoAsync(int maKho);
        Task<int?> GetSoLuongTonKhoAsync(int maKho, int maHangHoa);
    }
}
