using KhoHang_XNK.Models;
using KhoHang_XNK.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;

namespace KhoHang_XNK.Controllers
{
    public class DonXuatHangController : Controller
    {
        private readonly IDonXuatHangRepository _donXuatHangRepository;
        private readonly IKhachHangRepository _khachHangRepository;
        private readonly INhanVienRepository _nhanVienRepository;
        private readonly IKhoHangRepository _khoHangRepository;
        private readonly ITonKhoRepository _tonKhoRepository;
        private readonly IExcelExportService _excelExportService;
        public DonXuatHangController(IDonXuatHangRepository donXuatHangRepository, IKhachHangRepository khachHangRepository, INhanVienRepository nhanVienRepository, IKhoHangRepository khoHangRepository, ITonKhoRepository tonKhoRepository, IExcelExportService excelExportService)
        {
            _tonKhoRepository = tonKhoRepository;
            _donXuatHangRepository = donXuatHangRepository;
            _khachHangRepository = khachHangRepository;
            _nhanVienRepository = nhanVienRepository;
            _khoHangRepository = khoHangRepository;
            _excelExportService = excelExportService;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var list = await _donXuatHangRepository.GetAllAsync();
            return View(list);
        }
        public async Task<IActionResult> IndexUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var kho = await _khoHangRepository.GetKhoHangByIdUser(userId);
            var list = await _donXuatHangRepository.GetByKhoAsync(kho.MaKho);
            return View(list);
        }


        public async Task<IActionResult> Details(int id)
        {
            var donXuatHang = await _donXuatHangRepository.GetByIdAsync(id);
            if (donXuatHang == null) return NotFound();

            var chiTietList = await _donXuatHangRepository.GetChiTietByDonXuatIdAsync(id);
            ViewBag.ChiTietList = chiTietList; // Gửi danh sách chi tiết đơn xuất sang View

            return View(donXuatHang);
        }

        [HttpGet]
        public async Task<JsonResult> GetNhanViensByKho(int maKho)
        {
            var nhanViens = await _nhanVienRepository.GetByKhoAsync(maKho);
            return Json(nhanViens.Select(nv => new { maNV = nv.MaNV, hoTen = nv.HoTen }));
        }

        public async Task<IActionResult> Create()
        {
 
            ViewBag.KhachHangs = new SelectList(await _khachHangRepository.GetAllAsync(), "MaKH", "TenKH");
            ViewBag.NhanViens = new SelectList(await _nhanVienRepository.GetAllAsync(), "MaNV", "HoTen");
            ViewBag.Khos = new SelectList(await _khoHangRepository.GetAllKhoHangsAsync(), "MaKho", "TenKho");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DonXuatHang donXuatHang)
        {

            if (donXuatHang != null)
            {
                await _donXuatHangRepository.AddAsync(donXuatHang);
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "User");
                }
            }


            ViewBag.KhachHangs = new SelectList(await _khachHangRepository.GetAllAsync(), "MaKH", "TenKH");
            ViewBag.NhanViens = new SelectList(await _nhanVienRepository.GetAllAsync(), "MaNV", "HoTen");
            ViewBag.Khos = new SelectList(await _khoHangRepository.GetAllKhoHangsAsync(), "MaKho", "TenKho");

            return View(donXuatHang);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var donXuatHang = await _donXuatHangRepository.GetByIdAsync(id);
            if (donXuatHang == null) return NotFound();
            return View(donXuatHang);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(DonXuatHang donXuatHang)
        {
            await _donXuatHangRepository.DeleteAsync(donXuatHang.MaDonXuat);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var donXuatHang = await _donXuatHangRepository.GetByIdAsync(id);
            if (donXuatHang == null) return NotFound();

            // Truyền các danh sách vào ViewBag
            ViewBag.KhachHangs = new SelectList(await _khachHangRepository.GetAllAsync(), "MaKH", "TenKH", donXuatHang.MaKhachHang);
            ViewBag.NhanViens = new SelectList(await _nhanVienRepository.GetAllAsync(), "MaNV", "HoTen", donXuatHang.MaNV);
            ViewBag.Khos = new SelectList(await _khoHangRepository.GetAllKhoHangsAsync(), "MaKho", "TenKho", donXuatHang.MaKho);

            return View(donXuatHang);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DonXuatHang donXuatHang)
        {
            if (donXuatHang != null)
            {
                await _donXuatHangRepository.UpdateAsync(donXuatHang);
                return RedirectToAction("Index");
            }

            // Truyền lại danh sách vào ViewBag khi có lỗi
            ViewBag.KhachHangs = new SelectList(await _khachHangRepository.GetAllAsync(), "MaKH", "TenKH", donXuatHang.MaKhachHang);
            ViewBag.NhanViens = new SelectList(await _nhanVienRepository.GetAllAsync(), "MaNV", "HoTen", donXuatHang.MaNV);
            ViewBag.Khos = new SelectList(await _khoHangRepository.GetAllKhoHangsAsync(), "MaKho", "TenKho", donXuatHang.MaKho);

            return View(donXuatHang);
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmPayment(int id)
        {
            // Lấy đơn xuất hàng theo id
            var donXuatHang = await _donXuatHangRepository.GetByIdAsync(id);
            if (donXuatHang == null) return NotFound();

            // Kiểm tra nếu đơn xuất đã thanh toán rồi
            if (donXuatHang.trangthaithanhtoan != 0)
            {
                TempData["Message"] = "Đơn xuất hàng này đã được thanh toán.";
                return RedirectToAction("Details", new { id = donXuatHang.MaDonXuat });
            }

            // Cập nhật trạng thái thanh toán thành đã thanh toán
            donXuatHang.trangthaithanhtoan = 1; // Đã thanh toán
            await _donXuatHangRepository.UpdateAsync(donXuatHang);

            // Cập nhật tồn kho khi xuất hàng
            var chiTietList = await _donXuatHangRepository.GetChiTietByDonXuatIdAsync(id);
            foreach (var item in chiTietList)
            {
                // Cập nhật tồn kho tương ứng với từng mặt hàng khi xuất hàng
                var tonKho = await _tonKhoRepository.GetTonKhoByIdsAsync(donXuatHang.MaKho, item.MaHangHoa);
                if (tonKho != null)
                {
                    tonKho.SoLuong -= item.SoLuong; // Giảm số lượng tồn kho do xuất hàng
                    await _tonKhoRepository.UpdateTonKhoAsync(tonKho);
                }
                else
                {
                    // Nếu không có tồn kho cho mặt hàng này, có thể tạo mới tồn kho hoặc xử lý theo yêu cầu
                    var newTonKho = new TonKho
                    {
                        MaKho = donXuatHang.MaKho,
                        MaHangHoa = item.MaHangHoa,
                        SoLuong = -item.SoLuong // Đặt số lượng âm nếu hàng xuất ra mà không có tồn kho
                    };
                    await _tonKhoRepository.AddTonKhoAsync(newTonKho); // Thêm mới tồn kho nếu chưa có
                }
            }

            TempData["Message"] = "Thanh toán đã được xác nhận và tồn kho đã được cập nhật.";
            return RedirectToAction("Details", new { id = donXuatHang.MaDonXuat });
        }

      
        public async Task<IActionResult> ExportExcel(string searchTerm = "", string fromDate = "", string toDate = "")
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"ExportExcel called with searchTerm: '{searchTerm}', fromDate: '{fromDate}', toDate: '{toDate}'");
                // Khai báo biến allItems ở phạm vi toàn cục
                IEnumerable<DonXuatHang> allItems;

                if (User.IsInRole("Admin"))
                {
                    allItems = await _donXuatHangRepository.GetAllAsync(); // <-- tạo biến mới, KHÁC biến bên ngoài
                }
                else
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var kho = await _khoHangRepository.GetKhoHangByIdUser(userId);
                    allItems = await _donXuatHangRepository.GetByKhoAsync(kho.MaKho); // <-- cũng tạo biến mới
                }
                if (!allItems.Any()) return BadRequest("Không có dữ liệu để xuất");

                // Chuyển đổi fromDate và toDate sang DateTime
                DateTime? fromDateTime = string.IsNullOrWhiteSpace(fromDate) ? null : DateTime.ParseExact(fromDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                DateTime? toDateTime = string.IsNullOrWhiteSpace(toDate) ? null : DateTime.ParseExact(toDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                // Lọc dữ liệu theo searchTerm, fromDate, và toDate
                var filteredItems = allItems
                    .Where(d => string.IsNullOrWhiteSpace(searchTerm) ||
                        (int.TryParse(searchTerm, out int maDonXuat) && d.MaDonXuat == maDonXuat) ||
                        (d.KhachHang?.TenKH?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                        (d.KhoHang?.TenKho?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                        (d.NhanVien?.HoTen?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false))
                    .Where(d => (!fromDateTime.HasValue || d.NgayXuat >= fromDateTime.Value) &&
                               (!toDateTime.HasValue || d.NgayXuat <= toDateTime.Value))
                    .ToList();

                if (!filteredItems.Any()) return BadRequest("Không có dữ liệu phù hợp với bộ lọc để xuất");

                // Định nghĩa ánh xạ cột
                var columnMappings = new Dictionary<string, string>
                {
                    { "MaDonXuat", "Mã đơn xuất" },
                    { "KhachHang_TenKH", "Tên khách hàng" },
                    { "KhoHang_TenKho", "Tên kho hàng" },
                    { "NhanVien_HoTen", "Tên nhân viên" },
                    { "NgayXuat", "Ngày xuất đơn" }
                };

                // Chuẩn bị dữ liệu xuất Excel
                var exportData = filteredItems.Select(d => new
                {
                    MaDonXuat = d.MaDonXuat,
                    KhachHang_TenKH = d.KhachHang?.TenKH ?? "Không có thông tin",
                    KhoHang_TenKho = d.KhoHang?.TenKho ?? "Không có thông tin",
                    NhanVien_HoTen = d.NhanVien?.HoTen ?? "Không có thông tin",
                    NgayXuat = d.NgayXuat
                }).ToList();

                // Cấu hình tùy chọn xuất Excel
                var options = new ExcelExportOptions
                {
                    SheetName = "DonXuat",
                    Title = "DANH SÁCH ĐƠN XUẤT HÀNG",
                    ColumnMappings = columnMappings,
                    CustomFormatters = new Dictionary<string, Func<object, string>>
            {
                { "NgayXuat", value => value is DateTime dt ? dt.ToString("dd/MM/yyyy") : value?.ToString() ?? "" }
            },
                    HeaderBackgroundColor = System.Drawing.Color.LightBlue,
                    AutoFitColumns = true,
                    AddBorders = true
                };

                // Xuất file Excel
                var stream = _excelExportService.ExportToExcel(exportData, options);
                var fileName = $"DanhSachDonXuat_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
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
