using System.ComponentModel.DataAnnotations;

namespace Transportathon.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        [Required(ErrorMessage = " Hız Değerlendirme notu zorunludur.")]
        [Range(1, 10, ErrorMessage = "Değerlendirme notu 1 ile 10 arasında olmalıdır.")]
        public int RatingFast { get; set; }

        [Required(ErrorMessage = " Ürün sağlamlığı Değerlendirme notu zorunludur.")]
        [Range(1, 10, ErrorMessage = "Değerlendirme notu 1 ile 10 arasında olmalıdır.")]
        public int ProductDurability { get; set; }

        [Required(ErrorMessage = " Servis hizmeti Değerlendirme notu zorunludur.")]
        [Range(1, 10, ErrorMessage = "Değerlendirme notu 1 ile 10 arasında olmalıdır.")]
        public int ServiceSatisfaction { get; set; }

        [StringLength(500, ErrorMessage = "Yorum en fazla 500 karakter olmalıdır.")]
        public string Comment { get; set; }

        public int TaskId { get; set; }

        public string UserId { get; set; } // Değerlendirmeyi yapan kullanıcının kimliği

        public string DriverId { get; set; } // Değerlendirilen şoförün kimliği

      
    }
}
