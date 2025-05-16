using KhoHang_XNK.Models;
using KhoHang_XNK.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KhoHang_XNK.Controllers
{
    public class KhoHangController : Controller
    {
        private readonly IKhoHangRepository _khoHangRepository;
        private readonly INhanVienRepository _nhanVienRepository;
        private readonly IHangHoaRepository _hangHoaRepository;
        private readonly INhaCungCapRepository _nhaCungCapRepository;
        public KhoHangController(IKhoHangRepository khoHangRepository, INhanVienRepository nhanVienRepository, IHangHoaRepository hangHoaRepository, INhaCungCapRepository nhaCungCapRepository)
        {
            _khoHangRepository = khoHangRepository;
            _nhanVienRepository = nhanVienRepository;
            _hangHoaRepository = hangHoaRepository;
            _nhaCungCapRepository = nhaCungCapRepository;
        }
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


            return View(khoHangs);  // Truyền danh sách vào view
        }
        public async Task<IActionResult> Details(int id)
        {
            var khoHang = await _khoHangRepository.GetKhoHangByIdAsync(id);
            if (khoHang == null)
            {
                return NotFound();
            }
            return View(khoHang);  // Truyền đối tượng vào view
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(KhoHang khoHang, IFormFile imageUrl)
        {
            if(!ModelState.IsValid)
            {
                return View(khoHang);
            }
            if (imageUrl != null)
            {
                khoHang.ImageUrl = await SaveImage(imageUrl);
            }
            await _khoHangRepository.AddKhoHangAsync(khoHang);
            return RedirectToAction(nameof(Index));

        }
        private async Task<string> SaveImage(IFormFile image)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

            // Kiểm tra và tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = $"{Guid.NewGuid()}_{image.FileName}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            return "/images/" + fileName; // Trả về đường dẫn ảnh
        }

        public async Task<IActionResult> Edit(int id)
        {
            var khoHang = await _khoHangRepository.GetKhoHangByIdAsync(id);
            if (khoHang == null)
            {
                return NotFound();
            }
            return View(khoHang);  // Truyền đối tượng vào view
        }
        [HttpPost]
        public async Task<IActionResult> Edit(KhoHang khoHang, IFormFile imageUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(khoHang);
            }
            if (imageUrl != null)
            {
                khoHang.ImageUrl = await SaveImage(imageUrl);
            }
            await _khoHangRepository.UpdateKhoHangAsync(khoHang);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var khoHang = await _khoHangRepository.GetKhoHangByIdAsync(id);
            if (khoHang == null)
            {
                return NotFound();
            }
            return View(khoHang);  // Truyền đối tượng vào view
        }
        [HttpPost]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _khoHangRepository.DeleteKhoHangAsync(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> HangHoaAndTonKho(int maKho)
        {
            var hangHoaAndTonKhos = await _khoHangRepository.GetHangHoaAndTonKhoByMaKhoAsync(maKho);
            return View(hangHoaAndTonKhos);  // Truyền danh sách vào view
        }
    }
}
