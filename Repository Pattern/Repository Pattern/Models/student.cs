using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository_Pattern.Models
{
    public class student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool Status { get; set; }

        public int? DepratmentId { get; set; }
        public virtual Department Department { get; set; }

    }
}
