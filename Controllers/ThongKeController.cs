using KhoHang_XNK.Models;
using KhoHang_XNK.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;

namespace KhoHang_XNK.Controllers
{
    public class ThongKeController : Controller
    {
        private readonly ITonKhoRepository _tonKhoRepository;
        private readonly IHangHoaRepository _hangHoaRepository;
        private readonly IKhoHangRepository _khoHangRepository;
        private readonly IDonXuatHangRepository _donXuatHangRepository;
        private readonly IDonNhapHangRepository _donNhapHangRepository;
        private readonly ICTDonNhapRepository _chiTietDonNhapRepository;
        private readonly ICTDonXuatRepository _chiTietDonXuatRepository;

        public ThongKeController(
            ITonKhoRepository tonKhoRepository,
            IHangHoaRepository hangHoaRepository,
            IKhoHangRepository khoHangRepository,
            IDonXuatHangRepository phieuXuatRepository,
            IDonNhapHangRepository donNhapHangRepository,
            ICTDonNhapRepository chiTietDonNhapRepository,
            ICTDonXuatRepository chiTietDonXuatRepository)
        {
            _tonKhoRepository = tonKhoRepository;
            _hangHoaRepository = hangHoaRepository;
            _khoHangRepository = khoHangRepository;
            _donXuatHangRepository = phieuXuatRepository;
            _donNhapHangRepository = donNhapHangRepository;
            _chiTietDonNhapRepository = chiTietDonNhapRepository;
            _chiTietDonXuatRepository = chiTietDonXuatRepository;
        }

     
        public async Task<IActionResult> Index(int? khoId)
        {
            var viewModel = new ThongKeDoanhThuViewModel
            {
                SelectedKhoId = khoId,
                DanhSachKho = (await _khoHangRepository.GetAllKhoHangsAsync()).ToList(),
                DoanhThuNhapHang = await _donNhapHangRepository.GetTongTienNhapTheoThangAsync(khoId),
                DoanhThuXuatHang = await _donXuatHangRepository.GetTongTienXuatTheoThangAsync(khoId)
            };

            return View(viewModel);
        }
        public async Task<IActionResult> IndexUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var kho = await _khoHangRepository.GetKhoHangByIdUser(userId);
            var khoId = kho.MaKho;
            var viewModel = new ThongKeDoanhThuViewModel
            {
                SelectedKhoId = khoId,
                DanhSachKho = (await _khoHangRepository.GetAllKhoHangsAsync()).ToList(),
                DoanhThuNhapHang = await _donNhapHangRepository.GetTongTienNhapTheoThangAsync(khoId),
                DoanhThuXuatHang = await _donXuatHangRepository.GetTongTienXuatTheoThangAsync(khoId)
            };

            ViewBag.TenKho = kho.TenKho; // Gửi tên kho sang ViewBag để hiển thị trong view
            ViewData["Title"] = $"Thống kê doanh thu - {kho.TenKho}";

            return View(viewModel);
        }


    }
}
