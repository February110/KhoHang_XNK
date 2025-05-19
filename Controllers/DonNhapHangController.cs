using KhoHang_XNK.Models;
using KhoHang_XNK.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClosedXML.Excel;
using System.IO;
using System.Security.Claims;

namespace KhoHang_XNK.Controllers
{
    public class DonNhapHangController : Controller
    {
        private readonly IDonNhapHangRepository _donNhapHangRepository;
        private readonly INhaCungCapRepository _nhaCungCapRepository;
        private readonly INhanVienRepository _nhanVienRepository;
        private readonly IKhoHangRepository _khoHangRepository;
        private readonly ITonKhoRepository _tonKhoRepository;
        public DonNhapHangController(IDonNhapHangRepository donNhapHangRepository, INhaCungCapRepository nhaCungCapRepository, INhanVienRepository nhanVienRepository, IKhoHangRepository khoHangRepository, ITonKhoRepository tonKhoRepository)
        {
            _tonKhoRepository = tonKhoRepository;
            _donNhapHangRepository = donNhapHangRepository;
            _nhaCungCapRepository = nhaCungCapRepository;
            _nhanVienRepository = nhanVienRepository;
            _khoHangRepository = khoHangRepository;
        }
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
        public async Task<IActionResult> Details(int id)
        {
            var donNhapHang = await _donNhapHangRepository.GetByIdAsync(id);
            if (donNhapHang == null) return NotFound();

            var chiTietList = await _donNhapHangRepository.GetChiTietByDonNhapIdAsync(id);

            ViewBag.ChiTietList = chiTietList; // Gửi danh sách chi tiết đơn nhập sang View

            return View(donNhapHang);
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
                    return RedirectToAction("Index","User"); 
                }
                
            }

            ViewBag.NhaCungCaps = new SelectList(await _nhaCungCapRepository.GetAllNhaCungCapsAsync(), "MaNCC", "TenNCC", donNhapHang.MaNCC);

            ViewBag.Khos = new SelectList(await _khoHangRepository.GetAllKhoHangsAsync(), "MaKho", "TenKho", donNhapHang.MaKho);
            ViewBag.NhanViens = new SelectList(await _nhanVienRepository.GetByKhoAsync(donNhapHang.MaKho), "MaNV", "HoTen", donNhapHang.MaNV);


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

            // Lấy danh sách các Nhà Cung Cấp, Nhân Viên và Kho Hàng và truyền vào ViewBag
            ViewBag.NhaCungCaps = new SelectList(await _nhaCungCapRepository.GetAllNhaCungCapsAsync(), "MaNCC", "TenNCC", donNhapHang.MaNCC);
            
            ViewBag.Khos = new SelectList(await _khoHangRepository.GetAllKhoHangsAsync(), "MaKho", "TenKho", donNhapHang.MaKho);
            ViewBag.NhanViens = new SelectList(await _nhanVienRepository.GetByKhoAsync(donNhapHang.MaKho), "MaNV", "HoTen", donNhapHang.MaNV);

            return View(donNhapHang);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DonNhapHang donNhapHang)
        {
            if (ModelState.IsValid)
            {
                await _donNhapHangRepository.UpdateAsync(donNhapHang);
                return RedirectToAction("Index");
            }

            // Lấy lại danh sách và trả về nếu có lỗi
            ViewBag.NhaCungCaps = new SelectList(await _nhaCungCapRepository.GetAllNhaCungCapsAsync(), "MaNCC", "TenNCC", donNhapHang.MaNCC);

            ViewBag.Khos = new SelectList(await _khoHangRepository.GetAllKhoHangsAsync(), "MaKho", "TenKho", donNhapHang.MaKho);
            ViewBag.NhanViens = new SelectList(await _nhanVienRepository.GetByKhoAsync(donNhapHang.MaKho), "MaNV", "HoTen", donNhapHang.MaNV);

            return View(donNhapHang);
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmPayment(int id)
        {
            // Lấy đơn nhập hàng theo id
            var donNhapHang = await _donNhapHangRepository.GetByIdAsync(id);
            if (donNhapHang == null) return NotFound();

            // Kiểm tra nếu đơn nhập đã thanh toán rồi
            if (donNhapHang.trangthaithanhtoan != 0)
            {
                TempData["Message"] = "Đơn nhập hàng này đã được thanh toán.";
                return RedirectToAction("Details", new { id = donNhapHang.MaDonNhap });
            }

            // Cập nhật trạng thái thanh toán thành đã thanh toán
            donNhapHang.trangthaithanhtoan = 1; // Đã thanh toán
            await _donNhapHangRepository.UpdateAsync(donNhapHang);

            // Cập nhật tồn kho
            var chiTietList = await _donNhapHangRepository.GetChiTietByDonNhapIdAsync(id);
            foreach (var item in chiTietList)
            {
                // Cập nhật tồn kho tương ứng với từng mặt hàng
                var tonKho = await _tonKhoRepository.GetTonKhoByIdsAsync(donNhapHang.MaKho, item.MaHangHoa);
                if (tonKho != null)
                {
                    tonKho.SoLuong += item.SoLuong; // Thêm số lượng vào tồn kho
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
                    await _tonKhoRepository.AddTonKhoAsync(newTonKho); // Thêm mới tồn kho nếu chưa có
                }
            }

            TempData["Message"] = "Thanh toán đã được xác nhận và tồn kho đã được cập nhật.";
            return RedirectToAction("Details", new { id = donNhapHang.MaDonNhap });
        }


    }
}
