namespace LearningDotnet.DTO
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Status { get; set; } = "InActive";

        public string Subscription { get; set; } = "UnPaid";
    }
}
