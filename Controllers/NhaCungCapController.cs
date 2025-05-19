using KhoHang_XNK.Models;
using KhoHang_XNK.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KhoHang_XNK.Controllers
{
    public class NhaCungCapController : Controller
    {
        private readonly INhaCungCapRepository _nhaCungCapRepository;

        public NhaCungCapController(INhaCungCapRepository nhaCungCapRepository)
        {
            _nhaCungCapRepository = nhaCungCapRepository;
        }
        public async Task<IActionResult> Index()
        {

            //if (User.IsInRole("Admin"))
            //{
            var nhaCungCaps = await _nhaCungCapRepository.GetAllNhaCungCapsAsync();
            return View(nhaCungCaps);
            //}
            //return RedirectToAction("AccessDenied");

        }
        public async Task<IActionResult> IndexUser()
        {

            var nhaCungCaps = await _nhaCungCapRepository.GetAllNhaCungCapsAsync();
            return View(nhaCungCaps);

        }
        public async Task<IActionResult> Details(int id)
        {
            var nhacungcap = await _nhaCungCapRepository.GetNhaCungCapByIdAsync(id);
            return View(nhacungcap);
        }
        public IActionResult Create()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        // POST: NhaCungCap/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("MaNCC, TenNCC, DiaChi, SoDienThoai, Email")] NhaCungCap nhaCungCap)
        {
            if (ModelState.IsValid)
            {
                await _nhaCungCapRepository.AddNhaCungCapAsync(nhaCungCap);
                return RedirectToAction(nameof(Index));
            }
            return View(nhaCungCap);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var nhaCungCap = await _nhaCungCapRepository.GetNhaCungCapByIdAsync(id);
            if (nhaCungCap == null)
            {
                return NotFound();
            }
            return View(nhaCungCap);
        }

        // POST: NhaCungCap/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("MaNCC, TenNCC, DiaChi, SoDienThoai, Email")] NhaCungCap nhaCungCap)
        {
            if (id != nhaCungCap.MaNCC)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _nhaCungCapRepository.UpdateNhaCungCapAsync(nhaCungCap);
                }
                catch
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(nhaCungCap);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var nhaCungCap = await _nhaCungCapRepository.GetNhaCungCapByIdAsync(id);
            if (nhaCungCap == null)
            {
                return NotFound();
            }
            return View(nhaCungCap);
        }

        // POST: NhaCungCap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _nhaCungCapRepository.DeleteNhaCungCapAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
