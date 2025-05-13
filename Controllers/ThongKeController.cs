using KhoHang_XNK.Models;
using KhoHang_XNK.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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

        //public async Task<IActionResult> Index()
        //{

        //    var doanhThuNhapHang = await _donNhapHangRepository.GetTongTienNhapTheoThangAsync();

        //    // Lấy tổng tiền xuất hàng
        //    var doanhThuXuatHang = await _donXuatHangRepository.GetTongTienXuatTheoThangAsync();

        //    // Tạo model chứa cả 2 thông tin
        //    var viewModel = new ThongKeDoanhThuViewModel
        //    {
        //        DoanhThuNhapHang = doanhThuNhapHang,
        //        DoanhThuXuatHang = doanhThuXuatHang
        //    };
        //    ViewBag.DoanhThuNhapHangJson = JsonConvert.SerializeObject(doanhThuNhapHang);
        //    ViewBag.DoanhThuXuatHangJson = JsonConvert.SerializeObject(doanhThuXuatHang);

        //    return View(viewModel);

        //}
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



    }
}
