namespace Repository_Pattern.Models
{
    public class Department
    {
        public int id { get; set; }
        public string departmanetName { get; set; }
        public string departmaneDesc { get; set; }

        public virtual ICollection<student> students { get; set; }
    }
}
