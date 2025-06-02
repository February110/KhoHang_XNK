using DocumentFormat.OpenXml.InkML;
using KhoHang_XNK.Models;
using KhoHang_XNK.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace KhoHang_XNK.Controllers
{
    public class TonKhoController : Controller
    {
        private readonly ITonKhoRepository _tonKhoRepository;
        private readonly IHangHoaRepository _hangHoaRepository;
        private readonly IKhoHangRepository _khoHangRepository;
        private readonly IExcelExportService _excelExportService;
        public TonKhoController(ITonKhoRepository tonKhoRepository, IHangHoaRepository hangHoaRepository, IKhoHangRepository khoHangRepository, IExcelExportService excelExportService)
        {
            _excelExportService = excelExportService;
            _tonKhoRepository = tonKhoRepository;
            _hangHoaRepository = hangHoaRepository;
            _khoHangRepository = khoHangRepository;
        }
        [Authorize(Roles = "Admin")]
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
            else
            {
                await _tonKhoRepository.UpdateTonKhoAsync(tonKho);
                return RedirectToAction(nameof(Index));
            }
        }
     


        // Helper method
        private async Task<bool> TonKhoExistsAsync(int maKho, int maHangHoa)
        {
            var tonKho = await _tonKhoRepository.GetTonKhoByIdsAsync(maKho, maHangHoa);
            return tonKho != null;
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

        public async Task<IActionResult> IndexUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var khoList = await _khoHangRepository.GetAllKhoHangsForUserAsync(userId);

            if (khoList == null || !khoList.Any())
            {
                return NotFound("Không tìm thấy kho hàng nào của bạn.");
            }

            // Gộp tất cả tồn kho từ các kho lại
            var tonKhos = khoList.SelectMany(k => k.TonKhos).ToList();

            return View(tonKhos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExportExcel(string searchTerm = "")
        {
            try
            {
                // Log để debug
                System.Diagnostics.Debug.WriteLine($"ExportExcel called with searchTerm: '{searchTerm}'");

                // Lấy tất cả dữ liệu tồn kho
                var allItems = (await _tonKhoRepository.GetAllTonKhosAsync()).ToList();

                // Kiểm tra nếu không có dữ liệu
                if (!allItems.Any())
                {
                    return BadRequest("Không có dữ liệu tồn kho để xuất");
                }

                // Lọc dữ liệu theo searchTerm
                var filteredItems = string.IsNullOrWhiteSpace(searchTerm)
                    ? allItems
                    : allItems.Where(tk =>
                        (tk.KhoHang?.TenKho?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                        (tk.HangHoa?.TenHangHoa?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                        (tk.SoLuong.ToString()?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false))
                    .ToList();

                // Định nghĩa ánh xạ cột
                var columnMappings = new Dictionary<string, string>
            {
                { "KhoHang.TenKho", "Mã kho hàng" },
                { "HangHoa.TenHangHoa", "Mã hàng hóa" },
                { "SoLuong", "Số lượng" }
            };

                // Tạo DTO để tránh vấn đề với navigation properties
                var exportData = filteredItems.Select(tk => new
                {
                    KhoHang_TenKho = tk.KhoHang?.TenKho ?? "",
                    HangHoa_TenHangHoa = tk.HangHoa?.TenHangHoa ?? "",
                    SoLuong = tk.SoLuong
                }).ToList();

                // Cập nhật column mappings cho DTO
                var dtoColumnMappings = new Dictionary<string, string>
            {
                { "KhoHang_TenKho", "Mã kho hàng" },
                { "HangHoa_TenHangHoa", "Mã hàng hóa" },
                { "SoLuong", "Số lượng" }
            };

                var options = new ExcelExportOptions
                {
                    SheetName = "TonKho",
                    Title = "DANH SÁCH TỒN KHO",
                    ColumnMappings = dtoColumnMappings,
                    HeaderBackgroundColor = System.Drawing.Color.LightBlue,
                    AutoFitColumns = true,
                    AddBorders = true
                };

                var stream = _excelExportService.ExportToExcel(exportData, options);

                var fileName = $"DanhSachTonKho_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                return File(stream, contentType, fileName);
            }
            catch (Exception ex)
            {
                // Log chi tiết lỗi
                System.Diagnostics.Debug.WriteLine($"Export Excel Error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack Trace: {ex.StackTrace}");

                return BadRequest($"Lỗi xuất Excel: {ex.Message}");
            }
        }


    }
}
