using System.ComponentModel.DataAnnotations;

namespace Transportathon.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        [Required(ErrorMessage = "Değerlendirme notu zorunludur.")]
        [Range(1, 5, ErrorMessage = "Değerlendirme notu 1 ile 5 arasında olmalıdır.")]
        public int Rating { get; set; }

        [StringLength(500, ErrorMessage = "Yorum en fazla 500 karakter olmalıdır.")]
        public string Comment { get; set; }

        public int UserId { get; set; } // Değerlendirmeyi yapan kullanıcının kimliği

        public int DriverId { get; set; } // Değerlendirilen şoförün kimliği
        public  Driver Driver { get; set; } // Değerlendirilen şoförü ilişkilendirmek için

      
    }
}
