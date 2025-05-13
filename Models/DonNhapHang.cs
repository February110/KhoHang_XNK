using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KhoHang_XNK.Models
{
    public class DonNhapHang
    {
        [Key]
        public int MaDonNhap { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(DonNhapHang), "ValidateNgayNhap")]
        public DateTime NgayNhap { get; set; }

        [ForeignKey("NhaCungCap")]
        public int MaNCC { get; set; }

        [ForeignKey("KhoHang")]
        public int MaKho { get; set; }

        [ForeignKey("NhanVien")]
        public int MaNV { get; set; }

        public NhaCungCap NhaCungCap { get; set; }  // Navigation property
        public KhoHang KhoHang { get; set; }  // Navigation property
        public NhanVien NhanVien { get; set; }
        public static ValidationResult ValidateNgayNhap(DateTime ngayNhap, ValidationContext context)
        {
            if (ngayNhap > DateTime.Now)
            {
                return new ValidationResult("Ngày nhập không được lớn hơn ngày hiện tại.");
            }
            return ValidationResult.Success;
        }
        public int trangthaithanhtoan { get; set; } = 0;

    }
}
