using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningDotnet.Data
{
    public class Student
    {
        // it has two ways to do
        //1.this way 
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        //2 add config file and do this all thing you need in genral follow this rule 
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
