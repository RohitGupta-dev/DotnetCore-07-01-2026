using System.ComponentModel.DataAnnotations;

namespace Repository_Pattern.DTO
{
    public class LoginDTO
    {
        [Required]
        public string UserName{ get; set; }
        [Required]
        public string Password{ get; set; }
    }
}
