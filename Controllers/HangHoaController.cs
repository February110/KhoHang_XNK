using KhoHang_XNK.Models;
using KhoHang_XNK.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Drawing;

namespace KhoHang_XNK.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly IHangHoaRepository _hangHoaRepository;
        private readonly ILoaiHangHoaRepository _loaiHangHoaRepository;
        private readonly ITonKhoRepository _tonKhoRepository;
        private readonly IExcelExportService _excelExportService;
        public HangHoaController(
            IHangHoaRepository hangHoaRepository,
            ILoaiHangHoaRepository loaiHangHoaRepository,
            ITonKhoRepository tonKhoRepository,
            IExcelExportService excelService)
        {
            _excelExportService = excelService;
            _tonKhoRepository = tonKhoRepository;
            _hangHoaRepository = hangHoaRepository;
            _loaiHangHoaRepository = loaiHangHoaRepository;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            //if (User.IsInRole("Admin"))
            //{
                var hangHoas = await _hangHoaRepository.GetAllAsync();
                return View(hangHoas);
            //}
            //return RedirectToAction("AccessDenied", "Account");
            
        }
        public async Task<IActionResult> IndexUser()
        {
            var hangHoas = await _hangHoaRepository.GetAllAsync();
            return View(hangHoas);

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(HangHoa hangHoa, IFormFile imageUrl)
        {
            if (hangHoa == null)
            {
                var loaiHangHoas = await _loaiHangHoaRepository.GetAllAsync();
                ViewBag.MaLoaiHang = new SelectList(loaiHangHoas, "MaLoaiHang", "TenLoaiHang", hangHoa.MaLoaiHang);
                return View(hangHoa);
            }

            if (imageUrl != null && imageUrl.Length > 0)
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
        [HttpPost]
        public async Task<IActionResult> ExportExcel(string searchTerm = "")
        {
            try
            {
                // Log để debug
                System.Diagnostics.Debug.WriteLine($"ExportExcel called with searchTerm: '{searchTerm}'");

                var allItems = (await _hangHoaRepository.GetAllAsync()).ToList();

                // Kiểm tra nếu không có dữ liệu
                if (!allItems.Any())
                {
                    return BadRequest("Không có dữ liệu để xuất");
                }

                var filteredItems = string.IsNullOrWhiteSpace(searchTerm)
                    ? allItems
                    : allItems.Where(h =>
                        (h.TenHangHoa?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                        h.MaHangHoa.ToString().Contains(searchTerm) ||
                        (h.LoaiHangHoa?.TenLoaiHang?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                        (h.MoTa?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false))
                    .ToList();

                var columnMappings = new Dictionary<string, string>
                {
                    { "MaHangHoa", "Mã hàng hóa" },
                    { "TenHangHoa", "Tên hàng hóa" },
                    { "MoTa", "Mô tả" },
                    { "DonViTinh", "Đơn vị tính" },
                    { "HanSuDung", "Hạn sử dụng" },
                    { "LoaiHangHoa.TenLoaiHang", "Loại hàng hóa" }
                };

                // Tạo DTO để tránh vấn đề với navigation properties
                var exportData = filteredItems.Select(h => new
                {
                    MaHangHoa = h.MaHangHoa,
                    TenHangHoa = h.TenHangHoa ?? "",
                    MoTa = h.MoTa ?? "",
                    DonViTinh = h.DonViTinh ?? "",
                    HanSuDung = h.HanSuDung,
                    LoaiHangHoa_TenLoaiHang = h.LoaiHangHoa?.TenLoaiHang ?? ""
                }).ToList();

                // Cập nhật column mappings cho DTO
                var dtoColumnMappings = new Dictionary<string, string>
                {
                    { "MaHangHoa", "Mã hàng hóa" },
                    { "TenHangHoa", "Tên hàng hóa" },
                    { "MoTa", "Mô tả" },
                    { "DonViTinh", "Đơn vị tính" },
                    { "HanSuDung", "Hạn sử dụng" },
                    { "LoaiHangHoa_TenLoaiHang", "Loại hàng hóa" }
                };

                var options = new ExcelExportOptions
                {
                    SheetName = "HangHoa",
                    Title = "DANH SÁCH HÀNG HÓA",
                    ColumnMappings = dtoColumnMappings,
                    CustomFormatters = new Dictionary<string, Func<object, string>>
                    {
                        { "HanSuDung", value => value is DateTime dt ? dt.ToString("dd/MM/yyyy") : value?.ToString() ?? "" }
                    },
                    HeaderBackgroundColor = System.Drawing.Color.LightBlue,
                    AutoFitColumns = true,
                    AddBorders = true
                };

                var stream = _excelExportService.ExportToExcel(exportData, options);

                var fileName = $"DanhSachHangHoa_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
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
