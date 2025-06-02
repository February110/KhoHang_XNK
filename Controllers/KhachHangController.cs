using KhoHang_XNK.Models;
using KhoHang_XNK.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;
using System.Security.Claims;

namespace KhoHang_XNK.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly IKhachHangRepository _khachHangRepository;
        private readonly ILoaiKhachHangRepository _loaiKhachHangRepository;
        private readonly IDonXuatHangRepository _donxuatHangRepository;
        private readonly IKhoHangRepository _khoHangRepository;
        private readonly IExcelExportService _excelExportService;
        public KhachHangController(IKhachHangRepository khachHangRepository, ILoaiKhachHangRepository loaiKhachHangRepository, IDonXuatHangRepository donNhapHangRepository, IKhoHangRepository khoHangRepository,
            IExcelExportService excelExportService
            )
        {
            _excelExportService = excelExportService;
            _donxuatHangRepository = donNhapHangRepository;
            _khachHangRepository = khachHangRepository;
            _loaiKhachHangRepository = loaiKhachHangRepository;
            _khoHangRepository = khoHangRepository;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var khachHangs = await _khachHangRepository.GetAllAsync();
            return View(khachHangs);
        }
        public async Task<IActionResult> IndexUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var kho = await _khoHangRepository.GetKhoHangByIdUser(userId);
            var list = await _donxuatHangRepository.GetKhacHangByKhoAsync(kho.MaKho);
            return View(list);
        }
        public async Task<IActionResult> Create()
        {
            var loaiKhachHangs = await _loaiKhachHangRepository.GetAllLoaiKhachHangsAsync();
            ViewBag.MaLoaiKH = new SelectList(loaiKhachHangs, "MaLoaiKH", "TenLoai");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(KhachHang khachHang)
        {
            if (khachHang != null)
            {
                await _khachHangRepository.AddAsync(khachHang);
                return RedirectToAction("Index");
            }
            var loaiKhachHangs = await _loaiKhachHangRepository.GetAllLoaiKhachHangsAsync();
            ViewBag.MaLoaiKH = new SelectList(loaiKhachHangs, "MaLoaiKH", "TenLoai");
            return View(khachHang);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var khachHang = await _khachHangRepository.GetByIdAsync(id);
            if (khachHang == null)
            {
                return NotFound();
            }
            var loaiKhachHangs = await _loaiKhachHangRepository.GetAllLoaiKhachHangsAsync();
            ViewBag.MaLoaiKH = new SelectList(loaiKhachHangs, "MaLoaiKH", "TenLoai");
            return View(khachHang);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(KhachHang khachHang)
        {
            if (khachHang != null)
            {
                await _khachHangRepository.UpdateAsync(khachHang);
                return RedirectToAction("Index");
            }
            var loaiKhachHangs = await _loaiKhachHangRepository.GetAllLoaiKhachHangsAsync();
            ViewBag.MaLoaiKH = new SelectList(loaiKhachHangs, "MaLoaiKH", "TenLoai");
            return View(khachHang);
        }

        public async Task<IActionResult> Details(int id)
        {
            var khachHang = await _khachHangRepository.GetByIdAsync(id);
            if (khachHang == null) return NotFound();
            return View(khachHang);
        }

        // ✅ Thêm action để xem danh sách đơn xuất hàng của khách hàng
        public async Task<IActionResult> DonXuatHang(int id)
        {
            var donXuatHangs = await _khachHangRepository.GetDonXuatHangByKhachHangIdAsync(id);
            if (donXuatHangs == null || !donXuatHangs.Any()) return NotFound("Không có đơn xuất hàng nào.");
            return View(donXuatHangs);
        }

        public async Task<IActionResult> ChiTietDonXuat(int id)
        {
            var chiTietDonXuatList = await _khachHangRepository.GetChiTietDonXuatByKhachHangIdAsync(id);
            if (chiTietDonXuatList == null || !chiTietDonXuatList.Any())
                return NotFound("Không có chi tiết đơn xuất nào.");

            return View(chiTietDonXuatList);
        }

        [HttpPost]
        public async Task<IActionResult> ExportExcel(string searchTerm = "")
        {
            try
            {
                // Log để debug
                System.Diagnostics.Debug.WriteLine($"ExportExcel called with searchTerm: '{searchTerm}'");

                // Lấy tất cả dữ liệu khách hàng
                List<KhachHang> allItems;

                // Lấy dữ liệu dựa trên quyền của người dùng
                if (User.IsInRole("Admin"))
                {
                    allItems = (await _khachHangRepository.GetAllAsync()).ToList();
                }
                else
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var kho = await _khoHangRepository.GetKhoHangByIdUser(userId);
                    allItems = (await _donxuatHangRepository.GetKhacHangByKhoAsync(kho.MaKho)).ToList();
                }

                // Kiểm tra nếu không có dữ liệu
                if (!allItems.Any())
                {
                    return BadRequest("Không có dữ liệu khách hàng để xuất");
                }

                // Lọc dữ liệu theo searchTerm
                var filteredItems = string.IsNullOrWhiteSpace(searchTerm)
                ? allItems
                : allItems.Where(d =>
                    (d.MaKH.ToString().Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) || // Không cần ?. vì không null
                    (d.TenKH?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (d.DiaChi?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (d.SoDienThoai?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (d.Email?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (d.LoaiKhachHang?.TenLoai?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false))
                .ToList();

                // Định nghĩa ánh xạ cột
                var columnMappings = new Dictionary<string, string>
                {
                    { "MaKH", "Mã khách hàng" },
                    { "TenKH", "Tên khách hàng" },
                    { "DiaChi", "Địa chỉ" },
                    { "SoDienThoai", "Số điện thoại" },
                    { "Email", "Email" },
                    { "LoaiKhachHang.TenLoai", "Loại khách hàng" }
                };

                // Tạo DTO để tránh vấn đề với navigation properties
                var exportData = filteredItems.Select(kh => new
                {
                    MaKH = kh.MaKH,
                    TenKH = kh.TenKH ?? "",
                    DiaChi = kh.DiaChi ?? "",
                    SoDienThoai = kh.SoDienThoai ?? "",
                    Email = kh.Email ?? "",
                    LoaiKhachHang_TenLoai = kh.LoaiKhachHang?.TenLoai ?? ""
                }).ToList();

                // Cập nhật column mappings cho DTO
                var dtoColumnMappings = new Dictionary<string, string>
                {
                    { "MaKH", "Mã khách hàng" },
                    { "TenKH", "Tên khách hàng" },
                    { "DiaChi", "Địa chỉ" },
                    { "SoDienThoai", "Số điện thoại" },
                    { "Email", "Email" },
                    { "LoaiKhachHang_TenLoai", "Loại khách hàng" }
                };

                var options = new ExcelExportOptions
                {
                    SheetName = "KhachHang",
                    Title = "DANH SÁCH KHÁCH HÀNG",
                    ColumnMappings = dtoColumnMappings,
                    // Không có cột ngày nào cần format, nên bỏ CustomFormatters nếu không cần
                    HeaderBackgroundColor = System.Drawing.Color.LightBlue,
                    AutoFitColumns = true,
                    AddBorders = true
                };

                var stream = _excelExportService.ExportToExcel(exportData, options);

                var fileName = $"DanhSachKhachHang_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
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
