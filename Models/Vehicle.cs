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

        public string Details { get; set; }


        // Diğer araç özellikleri eklenebilir (örneğin, model, marka, üretim yılı vb.).
    }
}
