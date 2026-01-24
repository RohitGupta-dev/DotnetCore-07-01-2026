
using Repository_Pattern.Models;

namespace Repository_Pattern.Repo
{
    public interface IEmployee
    {
        Task<List<Employee>> GetAllEmployee();
        Task<Employee> GetById(int id);
        Task<Employee> GetByName(string name);
        Task<Employee> CreateEmployee(Employee emp);
        Task<bool> UpdateEmployee(int id,Employee emp);
        Task<bool> DeleteEmployee(int id);
    }
}
