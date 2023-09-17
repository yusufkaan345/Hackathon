using System.ComponentModel.DataAnnotations;

namespace Transportathon.Models
{
    public class CreateMessage
    {
        [Key]
        public int MessageId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string DriverId { get; set; }
        public string DriverName { get; set; }
        public string Messages { get; set; }

    }
}
