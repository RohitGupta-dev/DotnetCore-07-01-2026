using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Repository_Pattern.Data;
using Repository_Pattern.Models;
using Repository_Pattern.Repo;

namespace Repository_Pattern.Services
{
    public class StudentRepositry : collegeRepository<student>,IStudent
    {   
        private readonly AppDbContext _db;

        public StudentRepositry(AppDbContext db):base(db)
        {
            _db = db;   
        }

        public Task<List<student>> GetStudentFees(int feeStatus)
        {
            return null;
        }
    }
}
