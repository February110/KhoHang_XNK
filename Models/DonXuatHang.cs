using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KhoHang_XNK.Models
{
    public class DonXuatHang
    {
        [Key]
        public int MaDonXuat { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(DonXuatHang), "ValidateNgayXuat")]
        public DateTime NgayXuat { get; set; }

        [ForeignKey("KhachHang")]
        public int MaKhachHang { get; set; }
        [ForeignKey("NhanVien")]
        public int MaNV { get; set; }
        [ForeignKey("KhoHang")]
        public int MaKho { get; set; }

        public KhachHang KhachHang { get; set; }
        public NhanVien NhanVien { get; set; }
        public KhoHang KhoHang { get; set; }
        public static ValidationResult ValidateNgayXuat(DateTime ngayXuat, ValidationContext context)
        {
            if (ngayXuat > DateTime.Now)
            {
                return new ValidationResult("Ngày xuất không được lớn hơn ngày hiện tại.");
            }
            return ValidationResult.Success;
        }
        public int trangthaithanhtoan { get; set; } = 0;
    }
}
