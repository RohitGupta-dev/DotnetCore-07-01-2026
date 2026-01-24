
using Repository_Pattern.Models;

namespace Repository_Pattern.Repo
{
    public interface IStudent : ICollegeRepository<student>
    {
        Task<List<student>> GetStudentFees(int feeStatus);
    }
}
