using LearningDotnet.Validator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace LearningDotnet.DTO
{
    public class StudentDTO
    {
        [ValidateNever]
        public int Id { get; set; }
        [Required(ErrorMessage ="Enter Valid User Name")]
        [StringLength(40)]
        public string StudentName { get; set; }
        [EmailAddress(ErrorMessage ="Please Enter Valid Email")]
        //[Remote(action: "verifyEmail",controller:"StudentDTO")]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        public string Status { get; set; } = "InActive";

        public string Subscription { get; set; } = "UnPaid";
        [Range(18,100)]
        public int age { get; set; }
        public int Password { get; set; }
        [Compare(nameof(Password))]
        public int ConfirmPassWord { get; set; }
        [DateCheckValidater]
        public DateTime createon { get; set; }

    }
}
