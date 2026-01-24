using Microsoft.EntityFrameworkCore;
using Repository_Pattern.Data;
using Repository_Pattern.Models;
using System.Linq.Expressions;

namespace Repository_Pattern.Repo
{
    public class collegeRepository<T> : ICollegeRepository<T> where T : class 
    {
        private readonly AppDbContext _db;
        private readonly DbSet<T> _dbset;
        public collegeRepository(AppDbContext db)
        {
            _db = db;
            _dbset = _db.Set<T>();
        }

        public async Task<T> Create(T dBRecord)
        {

            _dbset.AddAsync(dBRecord);
            await _db.SaveChangesAsync();
            return dBRecord;
        }
        public async Task<bool> Delete(T dbRecord)
        {
            _dbset.Remove(dbRecord);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbset.ToListAsync();
        }

        public async Task<T> GetById(Expression<Func<T,bool>> filter,bool useNoTracking)
        {
            if(useNoTracking)
                return await _dbset.AsNoTracking().Where(filter).FirstOrDefaultAsync();
            else
                return await _dbset.Where(filter).FirstOrDefaultAsync();
        }

        public async Task<T> GetByName(Expression<Func<T, bool>> filter)
        {
            return await _dbset.Where(filter).FirstOrDefaultAsync();
        }

        public async Task<T> Update(T dbRecord)
        {
            _dbset.Update(dbRecord);
            await _db.SaveChangesAsync();
            return dbRecord;
        }
    }
}
