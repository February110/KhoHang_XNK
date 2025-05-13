using System.ComponentModel.DataAnnotations;

namespace KhoHang_XNK.Models
{
    public class LoaiHangHoa
    {
        [Key]
        public int MaLoaiHang { get; set; }

        [Required]
        [StringLength(100)]
        public string TenLoaiHang { get; set; }

        public List<HangHoa>? HangHoas { get; set; }


    }
}
