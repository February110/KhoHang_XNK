using KhoHang_XNK.Models;
using KhoHang_XNK.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KhoHang_XNK.Controllers
{
    public class TonKhoController : Controller
    {
        private readonly ITonKhoRepository _tonKhoRepository;
        private readonly IHangHoaRepository _hangHoaRepository;
        private readonly IKhoHangRepository _khoHangRepository;
        public TonKhoController(ITonKhoRepository tonKhoRepository, IHangHoaRepository hangHoaRepository, IKhoHangRepository khoHangRepository)
        {
            _tonKhoRepository = tonKhoRepository;
            _hangHoaRepository = hangHoaRepository;
            _khoHangRepository = khoHangRepository;
        }
        public async Task<IActionResult> Index()
        {
            var tonKhos = await _tonKhoRepository.GetAllTonKhosAsync();
            return View(tonKhos);
        }

        // GET: TonKho/Create
        // GET: TonKho/Create
        public async Task<IActionResult> Create()
        {
            // Fetch all KhoHang items for dropdown
            var khoHangs = await _khoHangRepository.GetAllKhoHangsAsync();
            ViewBag.MaKho = new SelectList(khoHangs, "MaKho", "TenKho");

            // Fetch all HangHoa items for dropdown
            var hangHoas = await _hangHoaRepository.GetAllAsync();
            ViewBag.MaHangHoa = new SelectList(hangHoas, "MaHangHoa", "TenHangHoa");

            return View();
        }

        // POST: TonKho/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("MaKho, MaHangHoa, SoLuong")] TonKho tonKho)
        {
            if (tonKho != null)
            {
                // Kiểm tra xem bản ghi có tồn tại không
                var existingTonKho = await _tonKhoRepository.GetTonKhoByIdsAsync(tonKho.MaKho, tonKho.MaHangHoa);

                if (existingTonKho != null)
                {
                    // Nếu bản ghi tồn tại, cộng thêm số lượng vào số lượng cũ
                    existingTonKho.SoLuong += tonKho.SoLuong;

                    // Cập nhật lại bản ghi trong cơ sở dữ liệu
                    await _tonKhoRepository.UpdateTonKhoAsync(existingTonKho);
                }
                else
                {
                    // Nếu bản ghi không tồn tại, thêm mới
                    await _tonKhoRepository.AddTonKhoAsync(tonKho);
                }

                // Redirect đến trang Index sau khi thực hiện
                return RedirectToAction(nameof(Index));
            }

            // Nếu model không hợp lệ, trả lại view với lỗi
            var khoHangs = await _khoHangRepository.GetAllKhoHangsAsync();
            ViewBag.MaKho = new SelectList(khoHangs, "MaKho", "TenKho");

            var hangHoas = await _hangHoaRepository.GetAllAsync();
            ViewBag.MaHangHoa = new SelectList(hangHoas, "MaHangHoa", "TenHangHoa");

            return View(tonKho);
        }





        public async Task<IActionResult> Edit(int maKho, int maHangHoa)
        {
            var tonKho = await _tonKhoRepository.GetTonKhoByIdsAsync(maKho, maHangHoa);
            if (tonKho == null)
            {
                return NotFound();
            }
            return View(tonKho);
        }

        // POST: TonKho/Edit/maKho/maHangHoa
        [HttpPost]
        public async Task<IActionResult> Edit(int maKho, int maHangHoa, [Bind("MaKho, MaHangHoa, SoLuong")] TonKho tonKho)
        {
            if (maKho != tonKho.MaKho || maHangHoa != tonKho.MaHangHoa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _tonKhoRepository.UpdateTonKhoAsync(tonKho);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TonKhoExists(maKho, maHangHoa))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tonKho);
        }

        // GET: TonKho/Details/maKho/maHangHoa
        public async Task<IActionResult> Details(int maKho, int maHangHoa)
        {
            var tonKho = await _tonKhoRepository.GetTonKhoByIdsAsync(maKho, maHangHoa);
            if (tonKho == null)
            {
                return NotFound();
            }
            return View(tonKho);
        }

        // GET: TonKho/Delete/maKho/maHangHoa
        public async Task<IActionResult> Delete(int maKho, int maHangHoa)
        {
            var tonKho = await _tonKhoRepository.GetTonKhoByIdsAsync(maKho, maHangHoa);
            if (tonKho == null)
            {
                return NotFound();
            }
            return View(tonKho);
        }

        // POST: TonKho/Delete/maKho/maHangHoa
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int maKho, int maHangHoa)
        {
            var tonKho = await _tonKhoRepository.GetTonKhoByIdsAsync(maKho, maHangHoa);
            if (tonKho != null)
            {
                await _tonKhoRepository.DeleteTonKhoAsync(maKho, maHangHoa);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TonKhoExists(int maKho, int maHangHoa)
        {
            return _tonKhoRepository.GetTonKhoByIdsAsync(maKho, maHangHoa) != null;
        }

        public async Task<IActionResult> IndexUser(int id)
        {
           
            var tonKhos = await _tonKhoRepository.GetTonKhosByMaKhoAsync(id);
            return View(tonKhos);
        }
    }
}
