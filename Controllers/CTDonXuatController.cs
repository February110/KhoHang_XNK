using KhoHang_XNK.Models;
using KhoHang_XNK.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KhoHang_XNK.Controllers
{
    public class CTDonXuatController : Controller
    {
        private readonly ICTDonXuatRepository _chiTietDonXuatRepository;
        private readonly IDonXuatHangRepository _donXuatHangRepository;
        private readonly IHangHoaRepository _hangHoaRepository;
        private readonly ITonKhoRepository _tonKhoRepository;

        public CTDonXuatController(ICTDonXuatRepository chiTietDonXuatRepository, IDonXuatHangRepository donXuatHangRepository, IHangHoaRepository hangHoaRepository, ITonKhoRepository tonKhoRepository)
        {
            _chiTietDonXuatRepository = chiTietDonXuatRepository;
            _donXuatHangRepository = donXuatHangRepository;
            _hangHoaRepository = hangHoaRepository;
            _tonKhoRepository = tonKhoRepository;
        }
        public async Task<IActionResult> Index()
        {
            var chiTietDonXuatList = await _chiTietDonXuatRepository.GetAllAsync();
            return View(chiTietDonXuatList);
        }

        public async Task<IActionResult> Details(int maDonXuat, int maHangHoa)
        {
            var chiTietDonXuat = await _chiTietDonXuatRepository.GetByIdAsync(maDonXuat, maHangHoa);
            if (chiTietDonXuat == null)
            {
                return NotFound();
            }
            return View(chiTietDonXuat);
        }
        // Phương thức hiển thị trang Create
        public async Task<IActionResult> Create()
        {
            ViewBag.DonXuatHangs = new SelectList(await _donXuatHangRepository.GetAllAsync(), "MaDonXuat", "MaDonXuat");
            ViewBag.HangHoas = new SelectList(await _hangHoaRepository.GetAllAsync(), "MaHangHoa", "TenHangHoa");

            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(ChiTietDonXuat chiTietDonXuat)
        //{
        //    if (chiTietDonXuat == null)
        //    {
        //        ModelState.AddModelError("", "Dữ liệu không hợp lệ.");
        //        ViewBag.DonXuatHangs = new SelectList(await _donXuatHangRepository.GetAllAsync(), "MaDonXuat", "MaDonXuat");
        //        ViewBag.HangHoas = new SelectList(await _hangHoaRepository.GetAllAsync(), "MaHangHoa", "TenHangHoa");
        //        return View(chiTietDonXuat);
        //    }

        //    // Lấy mã đơn xuất
        //    var madon = chiTietDonXuat.MaDonXuat;

        //    // Tìm mã kho từ đơn xuất
        //    var maKho = await _donXuatHangRepository.GetKhoByMaDonXuatAsync(madon);
        //    if (maKho == null)
        //    {
        //        ModelState.AddModelError("", "Không tìm thấy kho hàng của đơn xuất.");
        //        return View(chiTietDonXuat);
        //    }

        //    // Kiểm tra tồn kho
        //    var existingTonKho = await _tonKhoRepository.GetTonKhoByIdsAsync(maKho, chiTietDonXuat.MaHangHoa);
        //    if (existingTonKho == null || existingTonKho.SoLuong < chiTietDonXuat.SoLuong)
        //    {
        //        ModelState.AddModelError("", "Số lượng tồn kho không đủ để xuất hàng.");
        //        return View(chiTietDonXuat);
        //    }

        //    // Kiểm tra chi tiết đơn xuất
        //    var existingChiTiet = await _chiTietDonXuatRepository.GetChiTietDonXuatByDonXuatAndHangHoa(chiTietDonXuat.MaDonXuat, chiTietDonXuat.MaHangHoa);
        //    if (existingChiTiet != null)
        //    {
        //        existingChiTiet.SoLuong += chiTietDonXuat.SoLuong;
        //        if (chiTietDonXuat.DonGia > 0)
        //        {
        //            existingChiTiet.DonGia = chiTietDonXuat.DonGia;
        //        }

        //        await _chiTietDonXuatRepository.UpdateAsync(existingChiTiet);
        //    }
        //    else
        //    {
        //        await _chiTietDonXuatRepository.AddAsync(chiTietDonXuat);
        //    }

        //    // Cập nhật tồn kho
        //    if (existingTonKho != null)
        //    {
        //        existingTonKho.SoLuong -= chiTietDonXuat.SoLuong;
        //        await _tonKhoRepository.UpdateTonKhoAsync(existingTonKho);
        //    }

        //    return RedirectToAction(nameof(Index));
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChiTietDonXuat chiTietDonXuat)
        {
            if (chiTietDonXuat == null)
            {
                ModelState.AddModelError("", "Dữ liệu không hợp lệ.");
                ViewBag.DonXuatHangs = new SelectList(await _donXuatHangRepository.GetAllAsync(), "MaDonXuat", "MaDonXuat");
                ViewBag.HangHoas = new SelectList(await _hangHoaRepository.GetAllAsync(), "MaHangHoa", "TenHangHoa");
                return View(chiTietDonXuat);
            }

            var madon = chiTietDonXuat.MaDonXuat;
            var maKho = await _donXuatHangRepository.GetKhoByMaDonXuatAsync(madon);
            if (maKho == null)
            {
                ModelState.AddModelError("", "Không tìm thấy kho hàng của đơn xuất.");
                return View(chiTietDonXuat);
            }

            // Kiểm tra tồn kho
            var existingTonKho = await _tonKhoRepository.GetTonKhoByIdsAsync(maKho, chiTietDonXuat.MaHangHoa);
            if (existingTonKho == null || existingTonKho.SoLuong < chiTietDonXuat.SoLuong)
            {
                ModelState.AddModelError("", "Số lượng tồn kho không đủ để xuất hàng.");
                return View(chiTietDonXuat);
            }

            // Kiểm tra chi tiết đơn xuất
            var existingChiTiet = await _chiTietDonXuatRepository.GetChiTietDonXuatByDonXuatAndHangHoa(chiTietDonXuat.MaDonXuat, chiTietDonXuat.MaHangHoa);
            if (existingChiTiet != null)
            {
                existingChiTiet.SoLuong += chiTietDonXuat.SoLuong;
                if (chiTietDonXuat.DonGia > 0)
                {
                    existingChiTiet.DonGia = chiTietDonXuat.DonGia;
                }

                await _chiTietDonXuatRepository.UpdateAsync(existingChiTiet);
            }
            else
            {
                await _chiTietDonXuatRepository.AddAsync(chiTietDonXuat);
                return RedirectToAction(nameof(Index));
            }

            // Kiểm tra trạng thái thanh toán của đơn hàng trước khi trừ kho
            var donXuatHang = await _donXuatHangRepository.GetByIdAsync(madon);
            if (donXuatHang == null || donXuatHang.trangthaithanhtoan == 0)  // Kiểm tra xem đơn hàng có được thanh toán hay không
            {
                ModelState.AddModelError("", "Đơn hàng chưa được thanh toán, không thể trừ kho.");
                return View(chiTietDonXuat);
            }

            // Cập nhật tồn kho
            if (existingTonKho != null)
            {
                existingTonKho.SoLuong -= chiTietDonXuat.SoLuong;
                await _tonKhoRepository.UpdateTonKhoAsync(existingTonKho);
            }

            return RedirectToAction(nameof(Index));
        }



        // Phương thức hiển thị danh sách chi tiết đơn xuất (Index)

        public async Task<IActionResult> Edit(int maDonXuat, int maHangHoa)
        {
            var chiTietDonXuat = await _chiTietDonXuatRepository.GetByIdAsync(maDonXuat, maHangHoa);

            if (chiTietDonXuat == null)
            {
                return NotFound();
            }

            // Đổ dữ liệu vào ViewBag để hiển thị dropdown trong form
            ViewBag.DonXuatHangs = new SelectList(await _donXuatHangRepository.GetAllAsync(), "MaDonXuat", "MaDonXuat", chiTietDonXuat.MaDonXuat);
            ViewBag.HangHoas = new SelectList(await _hangHoaRepository.GetAllAsync(), "MaHangHoa", "TenHangHoa", chiTietDonXuat.MaHangHoa);

            return View(chiTietDonXuat);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int maDonXuat, int maHangHoa, ChiTietDonXuat chiTietDonXuat)
        {
            if (maDonXuat != chiTietDonXuat.MaDonXuat || maHangHoa != chiTietDonXuat.MaHangHoa)
            {
                return BadRequest();
            }

            if (chiTietDonXuat != null)
            {
                try
                {
                    await _chiTietDonXuatRepository.UpdateAsync(chiTietDonXuat);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _chiTietDonXuatRepository.GetByIdAsync(maDonXuat, maHangHoa) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            // Nếu có lỗi, load lại danh mục đơn xuất và hàng hóa
            ViewBag.DonXuatHangs = new SelectList(await _donXuatHangRepository.GetAllAsync(), "MaDonXuat", "MaDonXuat", chiTietDonXuat.MaDonXuat);
            ViewBag.HangHoas = new SelectList(await _hangHoaRepository.GetAllAsync(), "MaHangHoa", "TenHangHoa", chiTietDonXuat.MaHangHoa);

            return View(chiTietDonXuat);
        }

        public async Task<IActionResult> Delete(int maDonXuat, int maHangHoa)
        {
            var chiTietDonXuat = await _chiTietDonXuatRepository.GetByIdAsync(maDonXuat, maHangHoa);
            if (chiTietDonXuat == null)
            {
                return NotFound();
            }
            return View(chiTietDonXuat);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int maDonXuat, int maHangHoa)
        {
            var chiTietDonXuat = await _chiTietDonXuatRepository.GetByIdAsync(maDonXuat, maHangHoa);
            if (chiTietDonXuat == null)
            {
                return NotFound();
            }
            await _chiTietDonXuatRepository.DeleteAsync(chiTietDonXuat);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> GetMaKhoByDonXuat(int maDonXuat)
        {
            var maKho = await _donXuatHangRepository.GetMaKhoByDonXuatAsync(maDonXuat);
            return Json(new { maKho });
        }

        [HttpGet]
        public async Task<IActionResult> GetHangHoaByKho(int maKho)
        {
            var hangHoas = await _hangHoaRepository.GetHangHoaByKhoAsync(maKho);
            return Json(hangHoas);
        }

    }
}
