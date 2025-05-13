using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace KhoHang_XNK.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; }
        public string? AvatarUrl { get; set; }
    }
}
