using KhoHang_XNK.Models;
using KhoHang_XNK.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KhoHang_XNK.Controllers
{
    public class LoaiHangHoaController : Controller
    {
        private readonly IHangHoaRepository _hangHoaRepository;
        private readonly ILoaiHangHoaRepository _loaiHangHoaRepository;

        public LoaiHangHoaController(IHangHoaRepository hangHoaRepository, ILoaiHangHoaRepository loaiHangHoaRepository)
        {
            _hangHoaRepository = hangHoaRepository;
            _loaiHangHoaRepository = loaiHangHoaRepository;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            //if (User.IsInRole("Admin"))
            //{
                var loaiHangHoas = await _loaiHangHoaRepository.GetAllAsync();
                return View(loaiHangHoas);
            //}
            //return RedirectToAction("AccessDenied");
        }
        public async Task<IActionResult> IndexUser()
        {
            //if (User.IsInRole("Admin"))
            //{
            var loaiHangHoas = await _loaiHangHoaRepository.GetAllAsync();
            return View(loaiHangHoas);
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
            var loaiHangHoa = await _loaiHangHoaRepository.GetByIdAsync(id);

            if (loaiHangHoa == null)
            {
                return NotFound();
            }
            return View(loaiHangHoa);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(LoaiHangHoa loaiHangHoa)
        {
            if (ModelState.IsValid)
            {
                await _loaiHangHoaRepository.AddAsync(loaiHangHoa);
                return RedirectToAction(nameof(Index));
            }

            return View(loaiHangHoa);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var loaiHangHoa = await _loaiHangHoaRepository.GetByIdAsync(id);
            if (loaiHangHoa == null)
            {
                return NotFound();
            }
            return View(loaiHangHoa);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(LoaiHangHoa loaiHangHoa)
        {
            if (loaiHangHoa != null)
            {
                await _loaiHangHoaRepository.UpdateAsync(loaiHangHoa);
                return RedirectToAction(nameof(Index));
            }

            return View(loaiHangHoa);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var loaiHangHoa = await _loaiHangHoaRepository.GetByIdAsync(id);
            if (loaiHangHoa == null)
            {
                return NotFound();
            }
            return View(loaiHangHoa);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _loaiHangHoaRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
