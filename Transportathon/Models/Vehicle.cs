using System.ComponentModel.DataAnnotations;

namespace Transportathon.Models
{
    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }

        [Required(ErrorMessage = "Araç tipi zorunludur.")]
        [StringLength(100, ErrorMessage = "Araç tipi en fazla 100 karakter olmalıdır.")]
        public string VehicleType { get; set; }

        [Required(ErrorMessage = "Kapasite zorunludur.")]
        [Range(1, int.MaxValue, ErrorMessage = "Kapasite geçerli bir değer olmalıdır.")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Plaka numarası zorunludur.")]
        [StringLength(20, ErrorMessage = "Plaka numarası en fazla 20 karakter olmalıdır.")]
        public string LicensePlate { get; set; }

        // Diğer araç özellikleri eklenebilir (örneğin, model, marka, üretim yılı vb.).
    }
}
