using System.ComponentModel.DataAnnotations;

namespace Transportathon.Models
{
    public class Driver
    {
        [Key]
        public int DriverId { get; set; }

        [Required(ErrorMessage = "Şoför adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Şoför adı en fazla 100 karakter olmalıdır.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "İletişim bilgileri zorunludur.")]
        [StringLength(100, ErrorMessage = "İletişim bilgileri en fazla 100 karakter olmalıdır.")]
        public string ContactInfo { get; set; }

        // Diğer şoför özellikleri eklenebilir (örneğin, sürücü belgesi numarası, ehliyet sınıfı vb.).
    }
}
