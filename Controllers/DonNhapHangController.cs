using KhoHang_XNK.Models;
using KhoHang_XNK.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClosedXML.Excel;
using System.IO;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;

namespace KhoHang_XNK.Controllers
{
    public class DonNhapHangController : Controller
    {
        private readonly IDonNhapHangRepository _donNhapHangRepository;
        private readonly INhaCungCapRepository _nhaCungCapRepository;
        private readonly INhanVienRepository _nhanVienRepository;
        private readonly IKhoHangRepository _khoHangRepository;
        private readonly ITonKhoRepository _tonKhoRepository;
        private readonly IExcelExportService _excelExportService;
        public DonNhapHangController(IDonNhapHangRepository donNhapHangRepository, INhaCungCapRepository nhaCungCapRepository, INhanVienRepository nhanVienRepository, IKhoHangRepository khoHangRepository, ITonKhoRepository tonKhoRepository,
            IExcelExportService excelExportService)
        {
            _excelExportService = excelExportService;
            _tonKhoRepository = tonKhoRepository;
            _donNhapHangRepository = donNhapHangRepository;
            _nhaCungCapRepository = nhaCungCapRepository;
            _nhanVienRepository = nhanVienRepository;
            _khoHangRepository = khoHangRepository;
        }
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Index()
        {
            var list = await _donNhapHangRepository.GetAllAsync();
            return View(list);
        }
        public async Task<IActionResult> IndexUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var kho = await _khoHangRepository.GetKhoHangByIdUser(userId);
            var list = await _donNhapHangRepository.GetByKhoAsync(kho.MaKho);
            return View(list);
        }

        [HttpGet]
        public async Task<JsonResult> GetNhanViensByKho(int maKho)
        {
            var nhanViens = await _nhanVienRepository.GetByKhoAsync(maKho);
            return Json(nhanViens.Select(nv => new { maNV = nv.MaNV, hoTen = nv.HoTen }));
        }


        public async Task<IActionResult> Create()
        {
            ViewBag.NhaCungCaps = new SelectList(await _nhaCungCapRepository.GetAllNhaCungCapsAsync(), "MaNCC", "TenNCC");
            ViewBag.NhanViens = new SelectList(await _nhanVienRepository.GetAllAsync(), "MaNV", "HoTen");
            ViewBag.Khos = new SelectList(await _khoHangRepository.GetAllKhoHangsAsync(), "MaKho", "TenKho");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DonNhapHang donNhapHang)
        {
            if (donNhapHang != null)
            {
                await _donNhapHangRepository.AddAsync(donNhapHang);
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "User");
                }

            }

            ViewBag.NhaCungCaps = new SelectList(await _nhaCungCapRepository.GetAllNhaCungCapsAsync(), "MaNCC", "TenNCC", donNhapHang.MaNCC);

            ViewBag.Khos = new SelectList(await _khoHangRepository.GetAllKhoHangsAsync(), "MaKho", "TenKho", donNhapHang.MaKho);
            ViewBag.NhanViens = new SelectList(await _nhanVienRepository.GetByKhoAsync(donNhapHang.MaKho), "MaNV", "HoTen", donNhapHang.MaNV);


            return View(donNhapHang);
        }

        public async Task<IActionResult> Details(int id)
        {
            var donNhapHang = await _donNhapHangRepository.GetByIdAsync(id);
            if (donNhapHang == null) return NotFound();

            var chiTietList = await _donNhapHangRepository.GetChiTietByDonNhapIdAsync(id);

            ViewBag.ChiTietList = chiTietList;

            return View(donNhapHang);
        }
       
        public async Task<IActionResult> Delete(int id)
        {
            var donNhapHang = await _donNhapHangRepository.GetByIdAsync(id);
            if (donNhapHang == null) return NotFound();
            return View(donNhapHang);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(DonNhapHang donNhapHang)
        {
            await _donNhapHangRepository.DeleteAsync(donNhapHang.MaDonNhap);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            var donNhapHang = await _donNhapHangRepository.GetByIdAsync(id);
            if (donNhapHang == null) return NotFound();
            ViewBag.NhaCungCaps = new SelectList(await _nhaCungCapRepository.GetAllNhaCungCapsAsync(), "MaNCC", "TenNCC", donNhapHang.MaNCC);
            
            ViewBag.Khos = new SelectList(await _khoHangRepository.GetAllKhoHangsAsync(), "MaKho", "TenKho", donNhapHang.MaKho);
            ViewBag.NhanViens = new SelectList(await _nhanVienRepository.GetByKhoAsync(donNhapHang.MaKho), "MaNV", "HoTen", donNhapHang.MaNV);

            return View(donNhapHang);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DonNhapHang donNhapHang)
        {
            if (donNhapHang != null)
            {
                await _donNhapHangRepository.UpdateAsync(donNhapHang);
                return RedirectToAction("Index");
            }
            ViewBag.NhaCungCaps = new SelectList(await _nhaCungCapRepository.GetAllNhaCungCapsAsync(), "MaNCC", "TenNCC", donNhapHang.MaNCC);

            ViewBag.Khos = new SelectList(await _khoHangRepository.GetAllKhoHangsAsync(), "MaKho", "TenKho", donNhapHang.MaKho);
            ViewBag.NhanViens = new SelectList(await _nhanVienRepository.GetByKhoAsync(donNhapHang.MaKho), "MaNV", "HoTen", donNhapHang.MaNV);

            return View(donNhapHang);
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmPayment(int id)
        {
            var donNhapHang = await _donNhapHangRepository.GetByIdAsync(id);
            if (donNhapHang == null) return NotFound();
            if (donNhapHang.trangthaithanhtoan != 0)
            {
                TempData["Message"] = "Đơn nhập hàng này đã được thanh toán.";
                return RedirectToAction("Details", new { id = donNhapHang.MaDonNhap });
            }
            donNhapHang.trangthaithanhtoan = 1; 
            await _donNhapHangRepository.UpdateAsync(donNhapHang);
            var chiTietList = await _donNhapHangRepository.GetChiTietByDonNhapIdAsync(id);
            foreach (var item in chiTietList)
            {
                var tonKho = await _tonKhoRepository.GetTonKhoByIdsAsync(donNhapHang.MaKho, item.MaHangHoa);
                if (tonKho != null)
                {
                    tonKho.SoLuong += item.SoLuong;
                    await _tonKhoRepository.UpdateTonKhoAsync(tonKho);
                }
                else
                {
                    var newTonKho = new TonKho
                    {
                        MaKho = donNhapHang.MaKho,
                        MaHangHoa = item.MaHangHoa,
                        SoLuong = item.SoLuong
                    };
                    await _tonKhoRepository.AddTonKhoAsync(newTonKho); 
                }
            }

            TempData["Message"] = "Thanh toán đã được xác nhận và tồn kho đã được cập nhật.";
            return RedirectToAction("Details", new { id = donNhapHang.MaDonNhap });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExportExcel(string searchTerm = "", string fromDate = "", string toDate = "")
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"ExportExcel called with searchTerm: '{searchTerm}', fromDate: '{fromDate}', toDate: '{toDate}'");
                IEnumerable<DonNhapHang> allItems;

                if (User.IsInRole("Admin"))
                {
                    allItems = await _donNhapHangRepository.GetAllAsync(); // <-- tạo biến mới, KHÁC biến bên ngoài
                }
                else
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var kho = await _khoHangRepository.GetKhoHangByIdUser(userId);
                    allItems= await _donNhapHangRepository.GetByKhoAsync(kho.MaKho);
                }

                if (!allItems.Any()) return BadRequest("Không có dữ liệu để xuất");

                // Chuyển đổi fromDate và toDate sang DateTime
                DateTime? fromDateTime = string.IsNullOrWhiteSpace(fromDate) ? null : DateTime.ParseExact(fromDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                DateTime? toDateTime = string.IsNullOrWhiteSpace(toDate) ? null : DateTime.ParseExact(toDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                // Lọc dữ liệu theo searchTerm, fromDate, và toDate
                var filteredItems = allItems
                    .Where(d => string.IsNullOrWhiteSpace(searchTerm) ||
                        (int.TryParse(searchTerm, out int maDonNhap) && d.MaDonNhap == maDonNhap) ||
                        (d.NhaCungCap?.TenNCC?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                        (d.KhoHang?.TenKho?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                        (d.NhanVien?.HoTen?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false))
                    .Where(d => (!fromDateTime.HasValue || d.NgayNhap >= fromDateTime.Value) &&
                               (!toDateTime.HasValue || d.NgayNhap <= toDateTime.Value))
                    .ToList();

                if (!filteredItems.Any()) return BadRequest("Không có dữ liệu phù hợp với bộ lọc để xuất");

                // Định nghĩa ánh xạ cột
                var columnMappings = new Dictionary<string, string>
        {
            { "MaDonNhap", "Mã đơn nhập" },
            { "NhaCungCap_TenNCC", "Tên nhà cung cấp" },
            { "KhoHang_TenKho", "Tên kho hàng" },
            { "NhanVien_HoTen", "Tên nhân viên" },
            { "NgayNhap", "Ngày nhập đơn" }
        };

                // Chuẩn bị dữ liệu xuất Excel
                var exportData = filteredItems.Select(d => new
                {
                    MaDonNhap = d.MaDonNhap,
                    NhaCungCap_TenNCC = d.NhaCungCap?.TenNCC ?? "Không có thông tin",
                    KhoHang_TenKho = d.KhoHang?.TenKho ?? "Không có thông tin",
                    NhanVien_HoTen = d.NhanVien?.HoTen ?? "Không có thông tin",
                    NgayNhap = d.NgayNhap
                }).ToList();

                // Cấu hình tùy chọn xuất Excel
                var options = new ExcelExportOptions
                {
                    SheetName = "DonNhap",
                    Title = "DANH SÁCH ĐƠN NHẬP HÀNG",
                    ColumnMappings = columnMappings,
                    CustomFormatters = new Dictionary<string, Func<object, string>>
            {
                { "NgayNhap", value => value is DateTime dt ? dt.ToString("dd/MM/yyyy") : value?.ToString() ?? "" }
            },
                    HeaderBackgroundColor = System.Drawing.Color.LightBlue,
                    AutoFitColumns = true,
                    AddBorders = true
                };

                // Xuất file Excel
                var stream = _excelExportService.ExportToExcel(exportData, options);
                var fileName = $"DanhSachDonNhap_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                return File(stream, contentType, fileName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Export Excel Error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
                return BadRequest($"Lỗi xuất Excel: {ex.Message}");
            }
        }
    }
}
