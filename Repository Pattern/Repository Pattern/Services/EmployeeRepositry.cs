using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Repository_Pattern.Data;
using Repository_Pattern.Models;
using Repository_Pattern.Repo;

namespace Repository_Pattern.Services
{
    public class EmployeeRepositry : IEmployee
    {
        private readonly AppDbContext _db;

        public EmployeeRepositry(AppDbContext db)
        {
            _db = db;   
        }
        public async Task<Employee> CreateEmployee(Employee emp)
        {
            var employee = await _db.employee.OrderBy(x=>x.Id).LastOrDefaultAsync();

            _db.AddAsync(emp);
            await _db.SaveChangesAsync();
            return emp;
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var emp= await _db.employee.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (emp == null)
                return false;

            _db.Remove(emp);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<List<Employee>> GetAllEmployee()
        {
           return await _db.employee.ToListAsync();
        }

        public async Task<Employee> GetById(int id)
        {
            return await _db.employee.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Employee> GetByName(string name)
        {
            return await _db.employee.Where(x => x.Name == name).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateEmployee(int id, Employee emp)
        {
            var employee = await _db.employee.AsNoTracking().Where(x=>x.Id==id).FirstOrDefaultAsync();
            if (employee == null)
                return false;

            _db.Update(emp);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
