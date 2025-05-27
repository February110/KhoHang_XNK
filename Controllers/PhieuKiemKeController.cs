using KhoHang_XNK.Models;
using KhoHang_XNK.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace KhoHang_XNK.Controllers
{
    public class PhieuKiemKeController : Controller
    {
        private readonly IPhieuKiemKeRepository _phieuKiemKeRepository;
        private readonly INhanVienRepository _nhanVienRepository;
        private readonly IKhoHangRepository _khoHangRepository;
        private readonly IHangHoaRepository _hangHoaRepository;
        private readonly ITonKhoRepository _tonKhoRepository;
        public PhieuKiemKeController(
            ITonKhoRepository tonKhoRepository,
            IHangHoaRepository hangHoaRepository,
          IPhieuKiemKeRepository phieuKiemKeRepository,
          INhanVienRepository nhanVienRepository,
          IKhoHangRepository khoHangRepository)
        {
            _tonKhoRepository = tonKhoRepository;
            _hangHoaRepository = hangHoaRepository;
            _phieuKiemKeRepository = phieuKiemKeRepository;
            _nhanVienRepository = nhanVienRepository;
            _khoHangRepository = khoHangRepository;
        }

        // Hiển thị danh sách phiếu kiểm kê
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var phieuKiemKes = await _phieuKiemKeRepository.GetAllAsync();
            return View(phieuKiemKes);
        }
        public async Task<IActionResult> IndexUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var kho = await _khoHangRepository.GetKhoHangByIdUser(userId);
            var list = await _phieuKiemKeRepository.GetByKhoAsync(kho.MaKho);
            return View(list);
        }
        // GET: Tạo mới phiếu kiểm kê
        public async Task<IActionResult> Create()
        {
            ViewBag.NhanViens = new SelectList(await _nhanVienRepository.GetAllAsync(), "MaNV", "HoTen");
            ViewBag.Khos = new SelectList(await _khoHangRepository.GetAllKhoHangsAsync(), "MaKho", "TenKho");

            return View();
        }

        // POST: Tạo mới phiếu kiểm kê
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PhieuKiemKe phieuKiemKe)
        {
            if (phieuKiemKe != null)
            {
                await _phieuKiemKeRepository.AddAsync(phieuKiemKe);
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "User");
                }
            }
            ViewBag.NhanViens = new SelectList(await _nhanVienRepository.GetAllAsync(), "MaNV", "HoTen");
            ViewBag.Khos = new SelectList(await _khoHangRepository.GetAllKhoHangsAsync(), "MaKho", "TenKho");
            return View(phieuKiemKe);
        }

        // Xem chi tiết phiếu kiểm kê
        public async Task<IActionResult> Details(int id)
        {
            var phieuKiemKe = await _phieuKiemKeRepository.GetByIdAsync(id);
            if (phieuKiemKe == null)
            {
                return NotFound();
            }

            // Lấy danh sách chi tiết phiếu kiểm kê của phiếu này
            var chiTietPhieuKiemKes = await _phieuKiemKeRepository.GetChiTietByPhieuKiemKeIdAsync(id);

            // Gửi dữ liệu sang View
            ViewBag.ChiTietPhieuKiemKes = chiTietPhieuKiemKes;

            return View(phieuKiemKe);
        }

        // Xóa phiếu kiểm kê
        public async Task<IActionResult> Delete(int id)
        {
            var phieuKiemKe = await _phieuKiemKeRepository.GetByIdAsync(id);
            if (phieuKiemKe == null)
            {
                return NotFound();
            }

         
            return View(phieuKiemKe);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _phieuKiemKeRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var phieuKiemKe = await _phieuKiemKeRepository.GetByIdAsync(id);
            if (phieuKiemKe == null)
            {
                return NotFound();
            }
            ViewBag.NhanViens = new SelectList(await _nhanVienRepository.GetAllAsync(), "MaNV", "HoTen");
            ViewBag.Khos = new SelectList(await _khoHangRepository.GetAllKhoHangsAsync(), "MaKho", "TenKho");
            return View(phieuKiemKe);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PhieuKiemKe phieuKiemKe)
        {
            if (id != phieuKiemKe.MaKiemKe)
            {
                return NotFound();
            }
            if (phieuKiemKe != null)
            {
                await _phieuKiemKeRepository.UpdateAsync(phieuKiemKe);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.NhanViens = new SelectList(await _nhanVienRepository.GetAllAsync(), "MaNV", "HoTen");
            ViewBag.Khos = new SelectList(await _khoHangRepository.GetAllKhoHangsAsync(), "MaKho", "TenKho");
            return View(phieuKiemKe);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaKhoByKiemKe(int maKiemKe)
        {
            var maKho = await _phieuKiemKeRepository.GetMaKhoByKiemKeAsync(maKiemKe);
            return Json(new { maKho });
        }

        [HttpGet]
        public async Task<IActionResult> GetHangHoaByKho(int maKho)
        {
            var hangHoas = await _tonKhoRepository.GetHangHoaByKhoAsync(maKho);
            return Json(hangHoas);
        }

        [HttpGet]
        public async Task<IActionResult> GetSoLuongTonKho(int maKho, int maHangHoa)
        {
            var soLuong = await _tonKhoRepository.GetSoLuongTonKhoAsync(maKho, maHangHoa);
            return Json(new { soLuong });
        }

    }
}
