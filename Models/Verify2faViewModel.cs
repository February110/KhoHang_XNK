using System.ComponentModel.DataAnnotations;

namespace KhoHang_XNK.Models
{
    public class Verify2faViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(7, ErrorMessage = "Mã xác minh phải có ít nhất {2} và tối đa {1} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Mã xác minh")]
        public string Code { get; set; }

        [Display(Name = "Ghi nhớ thiết bị này")]
        public bool RememberMe { get; set; }

        [Display(Name = "Ghi nhớ trình duyệt")]
        public bool RememberMachine { get; set; }
    }
}
