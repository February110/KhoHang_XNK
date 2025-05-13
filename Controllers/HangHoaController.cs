using KhoHang_XNK.Models;
using KhoHang_XNK.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KhoHang_XNK.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly IHangHoaRepository _hangHoaRepository;
        private readonly ILoaiHangHoaRepository _loaiHangHoaRepository;
        private readonly ITonKhoRepository _tonKhoRepository;
        public HangHoaController(IHangHoaRepository hangHoaRepository, ILoaiHangHoaRepository loaiHangHoaRepository, ITonKhoRepository tonKhoRepository)
        {
            _tonKhoRepository = tonKhoRepository;
            _hangHoaRepository = hangHoaRepository;
            _loaiHangHoaRepository = loaiHangHoaRepository;
        }

        public async Task<IActionResult> Index()
        {
            //if (User.IsInRole("Admin"))
            //{
                var hangHoas = await _hangHoaRepository.GetAllAsync();
                return View(hangHoas);
            //}
            //return RedirectToAction("AccessDenied", "Account");
            
        }
        public async Task<IActionResult> IndexUser(int id)
        {

            var tonKhos = await _tonKhoRepository.GetTonKhosByMaKhoAsync(id);
            return View(tonKhos);

        }
        public async Task<IActionResult> Create()
        {
            var loaiHangHoas = await _loaiHangHoaRepository.GetAllAsync();
            ViewBag.MaLoaiHang = new SelectList(loaiHangHoas, "MaLoaiHang", "TenLoaiHang");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(HangHoa hangHoa, IFormFile imageUrl)
        {
            if (!ModelState.IsValid)
            {

                var loaiHangHoas = await _loaiHangHoaRepository.GetAllAsync();
                ViewBag.MaLoaiHang = new SelectList(loaiHangHoas, "MaLoaiHang", "TenLoaiHang");
                return View(hangHoa);
            }

            if (imageUrl != null)
            {
                hangHoa.ImageUrl = await SaveImage(imageUrl);
            }

            await _hangHoaRepository.AddAsync(hangHoa);
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
        public async Task<IActionResult> Details(int id)
        {
            // Fetch HangHoa by its ID using the updated repository method
            var hangHoa = await _hangHoaRepository.GetByIdAsync(id);
            if (hangHoa == null)
            {
                // Return 404 if the product is not found
                return NotFound();
            }

            // Fetch related data using updated repository methods
            var donNhapHangs = await _hangHoaRepository.GetDonNhapAsync(id);
            var donXuatHangs = await _hangHoaRepository.GetDonXuatAsync(id);
            var phieuKiemKes = await _hangHoaRepository.GetPhieuKiemKeAsync(id);
            var tonKho = await _hangHoaRepository.GetTonKhoAsync(id);

            // Prepare ViewModel with the fetched data
            var viewModel = new HangHoaDetailsViewModel
            {
                HangHoa = hangHoa,
                DonNhapHangs = donNhapHangs ?? Enumerable.Empty<ChiTietDonNhap>(), // Return empty if no data
                DonXuatHangs = donXuatHangs ?? Enumerable.Empty<ChiTietDonXuat>(), // Return empty if no data
                PhieuKiemKes = phieuKiemKes ?? Enumerable.Empty<ChiTietPhieuKiemKe>(), // Return empty if no data
                TonKho = tonKho ?? 0 // Default to 0 if tonKho is null
            };

            // Return the View with the ViewModel
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var hangHoa = await _hangHoaRepository.GetByIdAsync(id);
            if (hangHoa == null)
            {
                return NotFound();
            }
            var loaiHangHoas = await _loaiHangHoaRepository.GetAllAsync();
            ViewBag.MaLoaiHang = new SelectList(loaiHangHoas, "MaLoaiHang", "TenLoaiHang", hangHoa.MaLoaiHang);
            return View(hangHoa);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(HangHoa hangHoa, IFormFile imageUrl)
        {
            if (!ModelState.IsValid)
            {
                var loaiHangHoas = await _loaiHangHoaRepository.GetAllAsync();
                ViewBag.MaLoaiHang = new SelectList(loaiHangHoas, "MaLoaiHang", "TenLoaiHang", hangHoa.MaLoaiHang);
                return View(hangHoa);
            }
            if (imageUrl != null)
            {
                hangHoa.ImageUrl = await SaveImage(imageUrl);
            }
            await _hangHoaRepository.UpdateAsync(hangHoa);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            var hangHoa = await _hangHoaRepository.GetByIdAsync(id);
            if (hangHoa == null)
            {
                return NotFound();
            }
            return View(hangHoa);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _hangHoaRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }







    }
}
