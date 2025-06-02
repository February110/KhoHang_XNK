using KhoHang_XNK.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace KhoHang_XNK.Controllers
{
    public class TongQuanController : Controller
    {
        private readonly IKhoHangRepository _khoHangRepository;
        private readonly INhanVienRepository _nhanVienRepository;
        private readonly IHangHoaRepository _hangHoaRepository;
        private readonly INhaCungCapRepository _nhaCungCapRepository;
        private readonly ILoaiHangHoaRepository _loaiHangHoaRepository;
        private readonly IKhachHangRepository _khachHangRepository;
        private readonly IDonXuatHangRepository _donXuatHangRepository;
        private readonly ICTDonXuatRepository _ctDonXuatRepository;
        public TongQuanController(ICTDonXuatRepository ctDonXuatRepository,
            IKhoHangRepository khoHangRepository, INhanVienRepository nhanVienRepository, IHangHoaRepository hangHoaRepository, INhaCungCapRepository nhaCungCapRepository, ILoaiHangHoaRepository loaiHangHoaRepository, IKhachHangRepository khachHangRepository, IDonXuatHangRepository donXuatHangRepository)
        {
            _ctDonXuatRepository = ctDonXuatRepository;
            _donXuatHangRepository = donXuatHangRepository;
            _khoHangRepository = khoHangRepository;
            _nhanVienRepository = nhanVienRepository;
            _hangHoaRepository = hangHoaRepository;
            _nhaCungCapRepository = nhaCungCapRepository;
            _loaiHangHoaRepository = loaiHangHoaRepository;
            _khachHangRepository = khachHangRepository;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var khoHangs = await _khoHangRepository.GetAllKhoHangsAsync();
            var hangHoas = await _hangHoaRepository.GetAllAsync();
            var nhaCungCaps = await _nhaCungCapRepository.GetAllNhaCungCapsAsync();
            var nhanViens = await _nhanVienRepository.GetAllAsync();

            ViewBag.SoLuongKhoHang = khoHangs.Count();
            ViewBag.SoLuongHangHoa = hangHoas.Count();
            ViewBag.SoLuongNhaCungCap = nhaCungCaps.Count();
            ViewBag.SoLuongNhanVien = nhanViens.Count();

            var donXuatHangs = await _donXuatHangRepository.GetAllKhachHangAsync();

            var top10KhachHang = donXuatHangs
                .GroupBy(d => d.KhachHang.TenKH)
                .Select(g => new
                {
                    KhachHang = g.Key,
                    SoLuongDon = g.Count()
                })
                .OrderByDescending(g => g.SoLuongDon)
                .Take(10)
                .ToList();

            var chartData = top10KhachHang.Select(x => new {
                label = x.KhachHang,
                tongTienXuat = x.SoLuongDon
            });

            //var hangHoaBanHang = await _ctDonXuatRepository.GetByHangHoaAsync();
            //var top10HangHoa = hangHoaBanHang
            //    .GroupBy(h => h.HangHoa.TenHangHoa)
            //    .Select(g => new
            //    {
            //        HangHoa = g.Key,
            //        SoLuongBan = g.Sum(h => h.SoLuong)
            //    })
            //    .OrderByDescending(g => g.SoLuongBan)
            //    .Take(10)
            //    .ToList();
            // Top 10 hàng hóa
            var hangHoaBanHang = await _ctDonXuatRepository.GetByHangHoaAsync();
            var top10HangHoa = hangHoaBanHang
                .GroupBy(h => h.HangHoa.TenHangHoa)
                .Select(g => new
                {
                    HangHoa = g.Key,
                    SoLuongBan = g.Sum(h => h.SoLuong)
                })
                .OrderByDescending(g => g.SoLuongBan)
                .Take(10)
                .ToList();

            var chartDataHangHoa = top10HangHoa.Select(x => new {
                label = x.HangHoa,
                tongTienXuat = x.SoLuongBan
            });

            ViewBag.ChartDataHangHoa = JsonConvert.SerializeObject(chartDataHangHoa);
            ViewBag.ChartData = JsonConvert.SerializeObject(chartData);
            return View();
        }
        public async Task<IActionResult> IndexUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var khoHangs = await _khoHangRepository.GetAllKhoHangsForUserAsync(userId);

            var kho = await _khoHangRepository.GetKhoHangByIdUser(userId);
            var nhanViens = await _nhanVienRepository.GetNhanVienByKhoHang(kho.MaKho);
            var hangHoas = await _hangHoaRepository.GetAllAsync();
            var nhaCungCaps = await _nhaCungCapRepository.GetAllNhaCungCapsAsync();

            ViewBag.SoLuongKhoHang = khoHangs.Count();
            ViewBag.SoLuongHangHoa = hangHoas.Count();
            ViewBag.SoLuongNhaCungCap = nhaCungCaps.Count();
            ViewBag.SoLuongNhanVien = nhanViens.Count();


            return View(khoHangs);
        }
    }
}
