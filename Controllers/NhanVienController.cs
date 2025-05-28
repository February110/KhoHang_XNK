using KhoHang_XNK.Models;
using KhoHang_XNK.Repositories;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IExcelExportService _excelExportService;
        public NhanVienController(INhanVienRepository nhanVienRepository, IKhoHangRepository khoHangRepository, IExcelExportService excelExportService)
        {
            _nhanVienRepository = nhanVienRepository;
            _khoHangRepository = khoHangRepository;
            _excelExportService = excelExportService;
        }
        [Authorize(Roles = "Admin")]
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
        
        public async Task<IActionResult> ExportExcel(string searchTerm = "")
        {
            try
            {
                // Log để debug
                System.Diagnostics.Debug.WriteLine($"ExportExcel called with searchTerm: '{searchTerm}'");

                var allItems = (await _nhanVienRepository.GetAllAsync()).ToList();

                // Kiểm tra nếu không có dữ liệu
                if (!allItems.Any())
                {
                    return BadRequest("Không có dữ liệu để xuất");
                }

                // Lọc dữ liệu dựa trên searchTerm
                var filteredItems = string.IsNullOrWhiteSpace(searchTerm)
                  ? allItems
                  : allItems.Where(n =>
                      n.MaNV.ToString().Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                      (n.HoTen?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                      (n.ChucVu?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                      (n.SoDienThoai?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                      (n.Email?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                      (n.KhoHang.TenKho?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false))
                  .ToList();


                // Định nghĩa ánh xạ cột (tên cột trong Excel)
                var columnMappings = new Dictionary<string, string>
                {
                    { "MaNhanVien", "Mã nhân viên" },
                    { "HoVaTen", "Họ và tên" },
                    { "NgaySinh", "Ngày sinh" },
                    { "ChucVu", "Chức vụ" },
                    { "SoDienThoai", "Số điện thoại" },
                    { "Email", "Email" },
                    { "KhuVucLamViec", "Khu vực làm việc" }
                };

                // Tạo DTO để xuất dữ liệu
                var exportData = filteredItems.Select(n => new
                {
                    MaNhanVien = n.MaNV.ToString() ?? "",
                    HoVaTen = n.HoTen ?? "",
                    NgaySinh = n.NgaySinh,
                    ChucVu = n.ChucVu ?? "",
                    SoDienThoai = n.SoDienThoai ?? "",
                    Email = n.Email ?? "",
                    KhuVucLamViec = n.KhoHang.TenKho ?? ""
                }).ToList();

                // Cấu hình tùy chọn xuất Excel
                var options = new ExcelExportOptions
                {
                    SheetName = "NhanVien",
                    Title = "DANH SÁCH NHÂN VIÊN",
                    ColumnMappings = columnMappings,
                    CustomFormatters = new Dictionary<string, Func<object, string>>
            {
                { "NgaySinh", value => value is DateTime dt ? dt.ToString("dd/MM/yyyy") : value?.ToString() ?? "" }
            },
                    HeaderBackgroundColor = System.Drawing.Color.LightBlue,
                    AutoFitColumns = true,
                    AddBorders = true
                };

                // Xuất dữ liệu ra stream
                var stream = _excelExportService.ExportToExcel(exportData, options);

                // Tạo tên file
                var fileName = $"DanhSachNhanVien_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
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
