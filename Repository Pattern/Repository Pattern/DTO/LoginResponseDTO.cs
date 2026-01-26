using System.ComponentModel.DataAnnotations;

namespace Repository_Pattern.DTO
{
    public class LoginResponseDTO
    {
        public string UserName{ get; set; }
        public string Token{ get; set; }
    }
}
