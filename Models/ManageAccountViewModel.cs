using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace KhoHang_XNK.Models
{
    public class ManageAccountViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên hiển thị")]
        [Display(Name = "Tên hiển thị")]
        public string FullName { get; set; }

        [Display(Name = "Ảnh đại diện")]
        public IFormFile AvatarFile { get; set; }
        public string AvatarUrl { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu hiện tại")]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu mới")]
        [StringLength(100, ErrorMessage = "Mật khẩu phải dài ít nhất {2} ký tự.", MinimumLength = 6)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu mới")]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu xác nhận không khớp.")]
        public string ConfirmPassword { get; set; }
    }
}
