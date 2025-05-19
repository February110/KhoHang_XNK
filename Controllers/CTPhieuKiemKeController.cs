using KhoHang_XNK.Models;
using KhoHang_XNK.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KhoHang_XNK.Controllers
{
    public class CTPhieuKiemKeController : Controller
    {
        private readonly ICTPhieuKiemKeRepository _ctPhieuKiemKeRepository;
        private readonly IPhieuKiemKeRepository _phieuKiemKeRepository;
        private readonly IHangHoaRepository _hangHoaRepository;
        private readonly IKhoHangRepository _khoHangRepository;
        private readonly ITonKhoRepository _tonKhoRepository;

        public CTPhieuKiemKeController(ICTPhieuKiemKeRepository ctPhieuKiemKeRepository,
                                       IPhieuKiemKeRepository phieuKiemKeRepository,
                                       IHangHoaRepository hangHoaRepository,
                                       IKhoHangRepository khoHangRepository,
                                        ITonKhoRepository tonKhoRepository)
        {
            _ctPhieuKiemKeRepository = ctPhieuKiemKeRepository;
            _phieuKiemKeRepository = phieuKiemKeRepository;
            _hangHoaRepository = hangHoaRepository;
            _khoHangRepository = khoHangRepository;
            _tonKhoRepository = tonKhoRepository;
        }

        public async Task<IActionResult> Index()
        {
            var chiTietPhieuKiemKes = await _ctPhieuKiemKeRepository.GetAllAsync();
            return View(chiTietPhieuKiemKes);
        }

        public async Task<IActionResult> Details(int maKiemKe, int maHangHoa)
        {
            // Fetch the ChiTietPhieuKiemKe using the composite key
            var chiTietPhieuKiemKe = await _ctPhieuKiemKeRepository.GetByIdAsync(maKiemKe, maHangHoa);

            if (chiTietPhieuKiemKe == null)
            {
                return NotFound();
            }

            // Return the view with the fetched details
            return View(chiTietPhieuKiemKe);
        }


        [HttpGet]
        public async Task<IActionResult> GetSoLuongTonKho(int maKho, int maHangHoa)
        {
            var tonKho = await _tonKhoRepository.GetByMaHangHoa(maHangHoa);
            return Json(new { soLuongTon = tonKho?.SoLuong ?? 0 });
        }

        public async Task<IActionResult> Create()
        {
            // Fetch available PhieuKiemKe and HangHoa data
            var phieuKiemKeList = await _phieuKiemKeRepository.GetAllAsync();
            var hangHoaList = await _hangHoaRepository.GetAllAsync();

            // Populate the drop-down lists
            ViewBag.MaKiemKe = new SelectList(phieuKiemKeList, "MaKiemKe", "MaKiemKe");  
            ViewBag.MaHangHoa = new SelectList(hangHoaList, "MaHangHoa", "TenHangHoa");  

            return View();
        }

        // POST: CTPhieuKiemKe/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChiTietPhieuKiemKe chiTietPhieuKiemKe)
        {
            if (chiTietPhieuKiemKe != null)
            {
                // Save the new ChiTietPhieuKiemKe record

       
                await _ctPhieuKiemKeRepository.AddOrUpdateAsync(chiTietPhieuKiemKe);

                // Redirect to the list or another page after saving
                return RedirectToAction("Index");
            }

            // In case of validation failure, reload the drop-downs and return the form
            var phieuKiemKeList = await _phieuKiemKeRepository.GetAllAsync();
            var hangHoaList = await _hangHoaRepository.GetAllAsync();

            // Re-populate the drop-down lists
            ViewBag.MaKiemKe = new SelectList(phieuKiemKeList, "MaKiemKe", "MaKiemKe");
            ViewBag.MaHangHoa = new SelectList(hangHoaList, "MaHangHoa", "TenHangHoa");

            return View(chiTietPhieuKiemKe);
        }
        [HttpGet]
        public async Task<IActionResult> GetTonKho(int maHangHoa, int maKiemKe)
        {
            try
            {
                Console.WriteLine($"=== Yêu cầu lấy tồn kho: MaHangHoa={maHangHoa}, MaKiemKe={maKiemKe} ===");

                var phieuKiemKe = await _phieuKiemKeRepository.GetPhieuKiemKeWithKhoAsync(maKiemKe);

                if (phieuKiemKe == null)
                {
                    Console.WriteLine("❌ Không tìm thấy phiếu kiểm kê!");
                    return Json(new { success = false, message = "Phiếu kiểm kê không tồn tại" });
                }

                Console.WriteLine($"✅ Tìm thấy phiếu kiểm kê: MaKho={phieuKiemKe.MaKho}");

                var tonKho = await _tonKhoRepository.GetTonKhoByMaKhoAndMaHangHoaAsync(maHangHoa, phieuKiemKe.MaKho);
                //var tonKho = _context.TonKho
                //    .FirstOrDefault(t => t.MaHangHoa == maHangHoa && t.MaKho == phieuKiemKe.MaKho);

                if (tonKho == null)
                {
                    Console.WriteLine($"❌ Không có tồn kho cho MaHangHoa={maHangHoa} trong MaKho={phieuKiemKe.MaKho}");
                    return Json(new { success = false, message = "Hàng hóa không tồn tại trong kho này" });
                }

                Console.WriteLine($"✅ Tìm thấy tồn kho: SoLuong={tonKho.SoLuong}");
                return Json(new { success = true, soLuong = tonKho.SoLuong });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🔥 Lỗi: {ex.Message}");
                return Json(new { success = false, message = "Lỗi server" });
            }
        }
        //[HttpGet]

        //public async Task<IActionResult> GetTonKho(int maHangHoa, int maKiemKe)
        //{
        //    try
        //    {
        //        // Debug: Log tham số đầu vào
        //        Console.WriteLine($"Yêu cầu lấy tồn kho - Mã hàng hóa: {maHangHoa}, Mã kiểm kê: {maKiemKe}");

        //        // Kiểm tra hợp lệ
        //        if (maHangHoa <= 0 || maKiemKe <= 0)
        //        {
        //            return Json(new { success = false, message = "Mã hàng hóa hoặc mã kiểm kê không hợp lệ" });
        //        }

        //        var phieuKiemKe = await _phieuKiemKeRepository.GetPhieuKiemKeWithKhoAsync(maKiemKe);

        //        if (phieuKiemKe == null)
        //        {
        //            return Json(new { success = false, message = "Không tìm thấy phiếu kiểm kê" });
        //        }

        //        // Lấy tồn kho
        //        var tonKho = await _tonKhoRepository.GetTonKhoByMaKhoAndMaHangHoaAsync(maHangHoa, phieuKiemKe.MaKho);
        //        // Trả kết quả
        //        return Json(new
        //        {
        //            success = true,
        //            soLuong = tonKho?.SoLuong ?? 0,
        //            maKho = phieuKiemKe.MaKho,
        //            message = tonKho == null ? "Hàng hóa chưa có tồn kho trong kho này" : "OK"
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log lỗi
        //        Console.WriteLine($"Lỗi khi lấy tồn kho: {ex.Message}");
        //        return Json(new { success = false, message = "Lỗi server: " + ex.Message });
        //    }
        //    // Lấy phiếu kiểm kê, trong đó có MaKho


        //    // Lấy tồn kho theo MaKho và MaHangHoa



        //}



        public async Task<IActionResult> Edit(int maKiemKe, int maHangHoa)
        {
            // Fetch the ChiTietPhieuKiemKe record by MaKiemKe and MaHangHoa
            var chiTietPhieuKiemKe = await _ctPhieuKiemKeRepository.GetByIdAsync(maKiemKe, maHangHoa);

            if (chiTietPhieuKiemKe == null)
            {
                return NotFound();
            }

            // Fetch available PhieuKiemKe and HangHoa data for the drop-down lists
            var phieuKiemKeList = await _phieuKiemKeRepository.GetAllAsync();
            var hangHoaList = await _hangHoaRepository.GetAllAsync();

            // Populate the drop-down lists with existing data
            ViewBag.MaKiemKe = new SelectList(phieuKiemKeList, "MaKiemKe", "MaKiemKe", chiTietPhieuKiemKe.MaKiemKe);
            ViewBag.MaHangHoa = new SelectList(hangHoaList, "MaHangHoa", "TenHangHoa", chiTietPhieuKiemKe.MaHangHoa);

            return View(chiTietPhieuKiemKe);
        }

        // POST: CTPhieuKiemKe/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int maKiemKe, int maHangHoa, ChiTietPhieuKiemKe chiTietPhieuKiemKe)
        {
            if (maKiemKe != chiTietPhieuKiemKe.MaKiemKe || maHangHoa != chiTietPhieuKiemKe.MaHangHoa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Update the ChiTietPhieuKiemKe record
                await _ctPhieuKiemKeRepository.AddOrUpdateAsync(chiTietPhieuKiemKe);

                // Redirect to the index page or another relevant page
                return RedirectToAction("Index");
            }

            // Re-load the drop-down lists if the model is invalid
            var phieuKiemKeList = await _phieuKiemKeRepository.GetAllAsync();
            var hangHoaList = await _hangHoaRepository.GetAllAsync();

            ViewBag.MaKiemKe = new SelectList(phieuKiemKeList, "MaKiemKe", "MaKiemKe", chiTietPhieuKiemKe.MaKiemKe);
            ViewBag.MaHangHoa = new SelectList(hangHoaList, "MaHangHoa", "TenHangHoa", chiTietPhieuKiemKe.MaHangHoa);

            return View(chiTietPhieuKiemKe);
        }


        public async Task<IActionResult> Delete(int maKiemKe, int maHangHoa)
        {
            var chiTietPhieuKiemKe = await _ctPhieuKiemKeRepository.GetByIdAsync(maKiemKe, maHangHoa);
            if (chiTietPhieuKiemKe == null)
            {
                return NotFound();
            }

            return View(chiTietPhieuKiemKe);
        }

        // POST: CTPhieuKiemKe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int maKiemKe, int maHangHoa)
        {
            await _ctPhieuKiemKeRepository.DeleteAsync(maKiemKe, maHangHoa);
            return RedirectToAction(nameof(Index));
        }
    }
}
