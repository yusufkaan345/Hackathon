using System.ComponentModel.DataAnnotations;

namespace Transportathon.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Ad Soyad zorunludur.")]
        public string NameSurname { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
        public string UserName { get; set; }

        public string Role { get; set; } // Kullanıcının rolünü tutmak için

       
    }
}
