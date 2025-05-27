using KhoHang_XNK.Models;
using KhoHang_XNK.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace KhoHang_XNK.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly IKhachHangRepository _khachHangRepository;
        private readonly ILoaiKhachHangRepository _loaiKhachHangRepository;
        private readonly IDonXuatHangRepository _donxuatHangRepository;
        private readonly IKhoHangRepository _khoHangRepository;
        public KhachHangController(IKhachHangRepository khachHangRepository, ILoaiKhachHangRepository loaiKhachHangRepository, IDonXuatHangRepository donNhapHangRepository, IKhoHangRepository khoHangRepository)
        {
            _donxuatHangRepository = donNhapHangRepository;
            _khachHangRepository = khachHangRepository;
            _loaiKhachHangRepository = loaiKhachHangRepository;
            _khoHangRepository = khoHangRepository;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var khachHangs = await _khachHangRepository.GetAllAsync();
            return View(khachHangs);
        }
        public async Task<IActionResult> IndexUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var kho = await _khoHangRepository.GetKhoHangByIdUser(userId);
            var list = await _donxuatHangRepository.GetKhacHangByKhoAsync(kho.MaKho);
            return View(list);
        }
        public async Task<IActionResult> Create()
        {
            var loaiKhachHangs = await _loaiKhachHangRepository.GetAllLoaiKhachHangsAsync();
            ViewBag.MaLoaiKH = new SelectList(loaiKhachHangs, "MaLoaiKH", "TenLoai");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(KhachHang khachHang)
        {
            if (khachHang != null)
            {
                await _khachHangRepository.AddAsync(khachHang);
                return RedirectToAction("Index");
            }
            var loaiKhachHangs = await _loaiKhachHangRepository.GetAllLoaiKhachHangsAsync();
            ViewBag.MaLoaiKH = new SelectList(loaiKhachHangs, "MaLoaiKH", "TenLoai");
            return View(khachHang);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var khachHang = await _khachHangRepository.GetByIdAsync(id);
            if (khachHang == null)
            {
                return NotFound();
            }
            var loaiKhachHangs = await _loaiKhachHangRepository.GetAllLoaiKhachHangsAsync();
            ViewBag.MaLoaiKH = new SelectList(loaiKhachHangs, "MaLoaiKH", "TenLoai");
            return View(khachHang);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(KhachHang khachHang)
        {
            if (khachHang != null)
            {
                await _khachHangRepository.UpdateAsync(khachHang);
                return RedirectToAction("Index");
            }
            var loaiKhachHangs = await _loaiKhachHangRepository.GetAllLoaiKhachHangsAsync();
            ViewBag.MaLoaiKH = new SelectList(loaiKhachHangs, "MaLoaiKH", "TenLoai");
            return View(khachHang);
        }

        public async Task<IActionResult> Details(int id)
        {
            var khachHang = await _khachHangRepository.GetByIdAsync(id);
            if (khachHang == null) return NotFound();
            return View(khachHang);
        }

        // ✅ Thêm action để xem danh sách đơn xuất hàng của khách hàng
        public async Task<IActionResult> DonXuatHang(int id)
        {
            var donXuatHangs = await _khachHangRepository.GetDonXuatHangByKhachHangIdAsync(id);
            if (donXuatHangs == null || !donXuatHangs.Any()) return NotFound("Không có đơn xuất hàng nào.");
            return View(donXuatHangs);
        }

        public async Task<IActionResult> ChiTietDonXuat(int id)
        {
            var chiTietDonXuatList = await _khachHangRepository.GetChiTietDonXuatByKhachHangIdAsync(id);
            if (chiTietDonXuatList == null || !chiTietDonXuatList.Any())
                return NotFound("Không có chi tiết đơn xuất nào.");

            return View(chiTietDonXuatList);
        }
    }
}
