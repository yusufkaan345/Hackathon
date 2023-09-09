using System.ComponentModel.DataAnnotations;

namespace Transportathon.Models
{
    public class Request
    {
        [Key]
        public int RequestId { get; set; }

        [Required(ErrorMessage = "Talep türü zorunludur.")]
        public string RequestType { get; set; }

        [Required(ErrorMessage = "Eşya ağırlığı zorunludur.")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Eşya ağırlığı geçerli bir değer olmalıdır.")]
        public decimal Weight { get; set; }

        [Required(ErrorMessage = "Taşıma tarihi zorunludur.")]
        [DataType(DataType.Date)]
        public DateTime TransportDate { get; set; }

        public int UserId { get; set; } // Bu talebin hangi kullanıcı tarafından oluşturulduğunu göstermek için kullanıcı kimliği

        public  User User { get; set; } // Talebin hangi kullanıcıya ait olduğunu ilişkilendirmek için
        
    }
}
