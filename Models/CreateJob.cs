using System.ComponentModel.DataAnnotations;

namespace Transportathon.Models
{
    public class CreateJob
    {
        [Key]
        public int CreatedJobId { get; set; }

        [Required(ErrorMessage = "Eşya adı zorunludur.")]
        public string ItemName { get; set; }


        [Required(ErrorMessage = "Eşya ağırlığı zorunludur.")]
        [Range(1, int.MaxValue, ErrorMessage = "Eşya ağırlığı geçerli bir değer olmalıdır.")]
        public int Weight { get; set; }


        [Required(ErrorMessage = "Eşya bilgisi zorunludur.")]
        public string Definition { get; set; }
        [Required(ErrorMessage = "Ücret zorunludur.")]
        [Range(1, int.MaxValue, ErrorMessage = "Eşya ağırlığı geçerli bir değer olmalıdır.")]
        public int Price { get; set; }


        [Required(ErrorMessage = "Araç bilgisi zorunludur.")]
        public string VehicleType { get; set; }

        [Required(ErrorMessage = "Yer bilgisi gereklidir")]
        public string FromLocation { get; set; }

        [Required(ErrorMessage = "Yer bilgisi gereklidir")]
        public string ToLocation { get; set; }

        [Required(ErrorMessage = "Taşıma tarihi zorunludur.")]
        [DataType(DataType.Date)]
        public DateTime TransportDate { get; set; }


        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }
        public string UserId { get; set; } // Bu talebin hangi kullanıcı tarafından oluşturulduğunu göstermek için kullanıcı kimliği

        public  string UserName { get; set; } // Talebin hangi kullanıcıya ait olduğunu ilişkilendirmek için
        public string DriverId { get; set; }
        public bool Status { get; set; }
        public bool Accepted { get; set; }


    }
}
