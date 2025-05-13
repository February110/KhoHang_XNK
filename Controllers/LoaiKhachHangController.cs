using KhoHang_XNK.Models;
using KhoHang_XNK.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KhoHang_XNK.Controllers
{
    public class LoaiKhachHangController : Controller
    {
        private readonly ILoaiKhachHangRepository loaiKhachHangRepository;
        public LoaiKhachHangController(ILoaiKhachHangRepository loaiKhachHangRepository)
        {
            this.loaiKhachHangRepository = loaiKhachHangRepository;
        }
        public async Task<IActionResult> Index()
        {
            //if (User.IsInRole("Admin"))
            //{
                var loaiKhachHangs = await loaiKhachHangRepository.GetAllLoaiKhachHangsAsync();
                return View(loaiKhachHangs);
            //}
            //return RedirectToAction("AccessDenied");
        }
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
        public async Task<IActionResult> Details(int id)
        {
            var loaiKhachHang = await loaiKhachHangRepository.GetLoaiKhachHangByIdAsync(id);
            if (loaiKhachHang == null)
            {
                return NotFound();
            }
            return View(loaiKhachHang);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(LoaiKhachHang loaiKhachHang)
        {
            if (loaiKhachHang != null)
            {
                await loaiKhachHangRepository.AddLoaiKhachHangAsync(loaiKhachHang);
                return RedirectToAction(nameof(Index));
            }
            return View(loaiKhachHang);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var loaiKhachHang = await loaiKhachHangRepository.GetLoaiKhachHangByIdAsync(id);
            if (loaiKhachHang == null)
            {
                return NotFound();
            }
            return View(loaiKhachHang);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(LoaiKhachHang loaiKhachHang)
        {
            if (ModelState.IsValid)
            {
                await loaiKhachHangRepository.UpdateLoaiKhachHangAsync(loaiKhachHang);
                return RedirectToAction(nameof(Index));
            }
            return View(loaiKhachHang);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var loaiKhachHang = await loaiKhachHangRepository.GetLoaiKhachHangByIdAsync(id);
            if (loaiKhachHang == null)
            {
                return NotFound();
            }
            return View(loaiKhachHang);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await loaiKhachHangRepository.DeleteLoaiKhachHangAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
