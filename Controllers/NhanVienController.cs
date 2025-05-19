using KhoHang_XNK.Models;
using KhoHang_XNK.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Security.Claims;

namespace KhoHang_XNK.Controllers
{
    public class NhanVienController : Controller
    {
        private readonly INhanVienRepository _nhanVienRepository;
        private readonly IKhoHangRepository _khoHangRepository;
        public NhanVienController(INhanVienRepository nhanVienRepository, IKhoHangRepository khoHangRepository)
        {
            _nhanVienRepository = nhanVienRepository;
            _khoHangRepository = khoHangRepository;
        }

        public async Task<IActionResult> Index()
        {
            var nhanViens = await _nhanVienRepository.GetAllAsync();
            return View(nhanViens);
        }
        public async Task<IActionResult> IndexUser()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            var kho = await _khoHangRepository.GetKhoHangByIdUser(userId);

            var nhanVienList = await _nhanVienRepository.GetNhanVienByKhoHang(kho.MaKho);
            return View(nhanVienList);
        }
        public async Task<IActionResult> Create()
        {
            var khoHangs = await _khoHangRepository.GetAllKhoHangsAsync();
            ViewBag.MaKho = new SelectList(khoHangs, "MaKho", "TenKho");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(NhanVien nhanVien, IFormFile imageUrl) 
        {   
            if (nhanVien == null)
            {
                var khoHangs = await _nhanVienRepository.GetAllAsync();
                ViewBag.MaKho = new SelectList(khoHangs, "MaKho", "TenKho");
                return View(nhanVien);
            }
            if (imageUrl != null)
            {
                nhanVien.ImageUrl = await SaveImage(imageUrl);
            }
            await _nhanVienRepository.AddAsync(nhanVien);
            if(User.IsInRole("Admin"))
             {
                return RedirectToAction(nameof(Index));
            }
                else
            {
                return RedirectToAction("IndexUser" ,"NhanVien");
            }

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
            var nhanVien = await _nhanVienRepository.GetByIdAsync(id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            var khoHangs = await _khoHangRepository.GetAllKhoHangsAsync();
            ViewBag.MaKho = new SelectList(khoHangs, "MaKho", "TenKho", nhanVien.MaKho);
            return View(nhanVien);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(NhanVien nhanVien, IFormFile imageUrl)
        {
            if (nhanVien == null)
            {
                var khoHangs = await _khoHangRepository.GetAllKhoHangsAsync();
                ViewBag.MaKho = new SelectList(khoHangs, "MaKho", "TenKho", nhanVien.MaKho);
                return View(nhanVien);
            }

            if (imageUrl != null)
            {
                nhanVien.ImageUrl = await SaveImage(imageUrl);
            }

            await _nhanVienRepository.UpdateAsync(nhanVien);
            return RedirectToAction(nameof(Index));
        }

        //public async Task<IActionResult> Edit(int id)
        //{
        //    var nhanVien = await _nhanVienRepository.GetByIdAsync(id);
        //    var khoHangs = await _nhanVienRepository.GetAllAsync();
        //    ViewBag.MaKho = new SelectList(khoHangs, "MaKho", "TenKho");
        //    if (nhanVien == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(nhanVien);
        //}
        //[HttpPost]
        //public async Task<IActionResult> Edit(NhanVien nhanVien, IFormFile imageUrl)
        //{
        //    if (nhanVien == null)
        //    {
        //        var khoHangs = await _nhanVienRepository.GetAllAsync();
        //        ViewBag.MaKho = new SelectList(khoHangs, "MaKho", "TenKho");
        //        return View(nhanVien);
        //    }
        //    if (imageUrl != null)
        //    {
        //        nhanVien.ImageUrl = await SaveImage(imageUrl);
        //    }
        //    await _nhanVienRepository.UpdateAsync(nhanVien);
        //    return RedirectToAction(nameof(Index));
        //}
        public async Task<IActionResult> Delete(int id)
        {
            var nhanVien = await _nhanVienRepository.GetByIdAsync(id);
            if (nhanVien == null)
            {
                return NotFound();
            }
            return View(nhanVien);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _nhanVienRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var nhanVien = await _nhanVienRepository.GetByIdAsync(id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            var donNhapHangs = await _nhanVienRepository.GetDonNhapByNhanVienAsync(id);
            var donXuatHangs = await _nhanVienRepository.GetDonXuatByNhanVienAsync(id);
            var phieuKiemKes = await _nhanVienRepository.GetPhieuKiemKeByNhanVienAsync(id);

            var viewModel = new NhanVienDetailsViewModel
            {
                NhanVien = nhanVien,
                DonNhapHangs = donNhapHangs,
                DonXuatHangs = donXuatHangs,
                PhieuKiemKes = phieuKiemKes
            };

            return View(viewModel);
        }

      

    }
}
