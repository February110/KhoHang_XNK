using KhoHang_XNK.Models;
using KhoHang_XNK.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KhoHang_XNK.Controllers
{
    public class CTDonNhapController : Controller
    {
        private readonly ICTDonNhapRepository _chiTietDonNhapRepository;
        private readonly IDonNhapHangRepository _donNhapHangRepository;
        private readonly IHangHoaRepository _hangHoaRepository;
        private readonly ITonKhoRepository _tonKhoRepository;
        public CTDonNhapController(ICTDonNhapRepository chiTietDonNhapRepository, IDonNhapHangRepository donNhapHangRepository, IHangHoaRepository hangHoaRepository, ITonKhoRepository tonKhoRepository)
        {
            _chiTietDonNhapRepository = chiTietDonNhapRepository;
            _donNhapHangRepository = donNhapHangRepository;
            _hangHoaRepository = hangHoaRepository;
            _tonKhoRepository = tonKhoRepository;
        }
        public async Task<IActionResult> Index()
        {
            var chiTietDonNhaps = await _chiTietDonNhapRepository.GetAllAsync();
            return View(chiTietDonNhaps);
        }
        public async Task<IActionResult> Details(int maDonNhap, int maHangHoa)
        {
            var chiTietDonNhap = await _chiTietDonNhapRepository.GetByIdAsync(maDonNhap, maHangHoa);
            if (chiTietDonNhap == null)
            {
                return NotFound();
            }
            return View(chiTietDonNhap);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.DonNhapHangs = new SelectList(await _donNhapHangRepository.GetAllAsync(), "MaDonNhap", "MaDonNhap");
            ViewBag.HangHoas = new SelectList(await _hangHoaRepository.GetAllAsync(), "MaHangHoa", "TenHangHoa");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(ChiTietDonNhap chiTietDonNhap)
        //{
        //    if (chiTietDonNhap != null)
        //    {
        //        var madon = chiTietDonNhap.MaDonNhap;
        //        // Kiểm tra xem bản ghi chi tiết đơn nhập đã tồn tại trong bảng chi tiết đơn nhập
        //        var existingChiTiet = await _chiTietDonNhapRepository
        //            .GetChiTietDonNhapByDonNhapAndHangHoa(chiTietDonNhap.MaDonNhap, chiTietDonNhap.MaHangHoa);

        //        if (existingChiTiet != null)
        //        {
        //            // Nếu chi tiết đơn nhập đã tồn tại, cộng thêm số lượng vào bản ghi hiện tại
        //            existingChiTiet.SoLuong += chiTietDonNhap.SoLuong;
        //            existingChiTiet.DonGia = chiTietDonNhap.DonGia; // Cập nhật đơn giá nếu cần

        //            // Cập nhật bản ghi trong cơ sở dữ liệu
        //            await _chiTietDonNhapRepository.UpdateAsync(existingChiTiet);
        //        }
        //        else
        //        {
        //            // Nếu chi tiết đơn nhập chưa tồn tại, thêm mới chi tiết đơn nhập vào cơ sở dữ liệu
        //            await _chiTietDonNhapRepository.AddAsync(chiTietDonNhap);
        //        }

        //        var maKho = await _donNhapHangRepository.GetKhoByMaDonNhapAsync(madon);
        //        // Kiểm tra tồn kho có tồn tại cho MaKho và MaHangHoa
        //        var existingTonKho = await _tonKhoRepository.GetTonKhoByIdsAsync(maKho, chiTietDonNhap.MaHangHoa);

        //        if (existingTonKho != null)
        //        {
        //            // Nếu tồn kho đã tồn tại, cộng thêm số lượng vào
        //            existingTonKho.SoLuong += chiTietDonNhap.SoLuong;

        //            // Cập nhật bản ghi tồn kho trong cơ sở dữ liệu
        //            await _tonKhoRepository.UpdateTonKhoAsync(existingTonKho);
        //        }
        //        else
        //        {
        //            // Nếu chưa có tồn kho, thêm mới bản ghi tồn kho với số lượng đã nhập
        //            var newTonKho = new TonKho
        //            {
        //                MaKho = chiTietDonNhap.DonNhapHang.MaKho,
        //                MaHangHoa = chiTietDonNhap.MaHangHoa,
        //                SoLuong = chiTietDonNhap.SoLuong
        //            };

        //            // Thêm mới bản ghi tồn kho vào cơ sở dữ liệu
        //            await _tonKhoRepository.AddTonKhoAsync(newTonKho);
        //        }

        //        // Chuyển hướng về trang danh sách chi tiết đơn nhập
        //        return RedirectToAction(nameof(Index));
        //    }

        //    // Nếu có lỗi, gửi lại danh mục đơn nhập và hàng hóa vào ViewBag để hiển thị trong form
        //    ViewBag.DonNhapHangs = new SelectList(await _donNhapHangRepository.GetAllAsync(), "MaDonNhap", "MaDonNhap");
        //    ViewBag.HangHoas = new SelectList(await _hangHoaRepository.GetAllAsync(), "MaHangHoa", "TenHangHoa");

        //    return View(chiTietDonNhap);
        //}
        public async Task<IActionResult> Create(ChiTietDonNhap chiTietDonNhap)
        {
            if (chiTietDonNhap != null && chiTietDonNhap.MaDonNhap != 0 && chiTietDonNhap.MaHangHoa != 0 && chiTietDonNhap.SoLuong > 0)
            {
                var madon = chiTietDonNhap.MaDonNhap;

                var existingChiTiet = await _chiTietDonNhapRepository
                    .GetChiTietDonNhapByDonNhapAndHangHoa(chiTietDonNhap.MaDonNhap, chiTietDonNhap.MaHangHoa);

                if (existingChiTiet != null)
                {

                    existingChiTiet.SoLuong += chiTietDonNhap.SoLuong;
                    existingChiTiet.DonGia = chiTietDonNhap.DonGia; 

                    
                    await _chiTietDonNhapRepository.UpdateAsync(existingChiTiet);
                }
                else
                {
                  
                    await _chiTietDonNhapRepository.AddAsync(chiTietDonNhap);
                    return RedirectToAction(nameof(Index));
                }

                var donNhapHang = await _donNhapHangRepository.GetByIdAsync(madon);
                if (donNhapHang == null)
                {
                    ModelState.AddModelError(string.Empty, "Không tìm thấy đơn nhập.");
                    return View(chiTietDonNhap);
                }

              
                if (donNhapHang.trangthaithanhtoan != 0)
                {
                    var maKho = await _donNhapHangRepository.GetKhoByMaDonNhapAsync(madon);
                  
                    var existingTonKho = await _tonKhoRepository.GetTonKhoByIdsAsync(maKho, chiTietDonNhap.MaHangHoa);

                    if (existingTonKho != null)
                    {
                        
                        existingTonKho.SoLuong += chiTietDonNhap.SoLuong;


                        await _tonKhoRepository.UpdateTonKhoAsync(existingTonKho);
                    }
                    else
                    {
                        
                        var newTonKho = new TonKho
                        {
                            MaKho = maKho,
                            MaHangHoa = chiTietDonNhap.MaHangHoa,
                            SoLuong = chiTietDonNhap.SoLuong
                        };

                      
                        await _tonKhoRepository.AddTonKhoAsync(newTonKho);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Chưa thanh toán, không thể cập nhật tồn kho.");
                    return View(chiTietDonNhap);
                }

              
                return RedirectToAction(nameof(Index));
            }
            ViewBag.DonNhapHangs = new SelectList(await _donNhapHangRepository.GetAllAsync(), "MaDonNhap", "MaDonNhap");
            ViewBag.HangHoas = new SelectList(await _hangHoaRepository.GetAllAsync(), "MaHangHoa", "TenHangHoa");

            return View(chiTietDonNhap);
        }



        public async Task<IActionResult> Edit(int maDonNhap, int maHangHoa)
        {
            var chiTietDonNhap = await _chiTietDonNhapRepository.GetByIdAsync(maDonNhap, maHangHoa);
            if (chiTietDonNhap == null) return NotFound();
            ViewBag.DonNhapHangs = new SelectList(await _donNhapHangRepository.GetAllAsync(), "MaDonNhap", "MaDonNhap");
            ViewBag.HangHoas = new SelectList(await _hangHoaRepository.GetAllAsync(), "MaHangHoa", "TenHangHoa");

            return View(chiTietDonNhap);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int maDonNhap, int maHangHoa, ChiTietDonNhap chiTietDonNhap)
        {

            if (chiTietDonNhap != null)
            {
               
                await _chiTietDonNhapRepository.UpdateAsync(chiTietDonNhap);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.DonNhapHangs = new SelectList(await _donNhapHangRepository.GetAllAsync(), "MaDonNhap", "MaDonNhap");
            ViewBag.HangHoas = new SelectList(await _hangHoaRepository.GetAllAsync(), "MaHangHoa", "TenHangHoa");

            return View(chiTietDonNhap);
        }

        public async Task<IActionResult> Delete(int maDonNhap, int maHangHoa)
        {
            var chiTietDonNhap = await _chiTietDonNhapRepository.GetByIdAsync(maDonNhap, maHangHoa);
            if (chiTietDonNhap == null)
            {
                return NotFound();
            }
            return View(chiTietDonNhap);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int maDonNhap, int maHangHoa)
        {
            await _chiTietDonNhapRepository.DeleteAsync(maDonNhap, maHangHoa);
            return RedirectToAction(nameof(Index));
        }
    }
}
