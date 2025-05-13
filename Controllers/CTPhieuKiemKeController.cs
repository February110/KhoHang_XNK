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


        public async Task<IActionResult> Create()
        {
            // Fetch available PhieuKiemKe and HangHoa data
            var phieuKiemKeList = await _phieuKiemKeRepository.GetAllAsync();
            var hangHoaList = await _hangHoaRepository.GetAllAsync();

            // Populate the drop-down lists
            ViewBag.MaKiemKe = new SelectList(phieuKiemKeList, "MaKiemKe", "MaKiemKe");  // Assuming MaKiemKe is the identifier
            ViewBag.MaHangHoa = new SelectList(hangHoaList, "MaHangHoa", "TenHangHoa");    // Assuming TenHangHoa is the name of the item

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
