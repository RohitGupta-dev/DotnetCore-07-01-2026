namespace LearningDotnet.Models
{
    public static class CollegeRepository
    {
        public static List<Student> Students = new List<Student>()
        {

            new Student()
            {
                Id=1,
                StudentName="Rohit",
                Email="test@gmail.com",
                Address="#22 Rajpura",
            },new Student()
            {
                Id=2,
                StudentName="Ankit",
                Email="test1@gmail.com",
                Address="#22 Mohali",
            },new Student()
            {
                Id=3,
                StudentName="Mohit",
                Email="test3@gmail.com",
                Address="#22 Chandigadh",
            }
        };
    }
}
