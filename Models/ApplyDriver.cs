using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Transportathon.Models
{
    public class ApplyDriver
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Driver")]
        public string DriverId { get; set; }

        [ForeignKey("CreateJob")]
        public int CreatedJobId { get; set; }

        [Required(ErrorMessage = "Yer bilgisi gereklidir")]
        public bool isAccepted { get; set; }


    }
}
