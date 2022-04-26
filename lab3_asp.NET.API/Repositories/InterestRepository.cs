using lab3_asp.NET.API.Contexts;
using lab3_asp.NET.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab3_asp.NET.API.Repositories
{
    // repository for Interests that implements the IRepository interface
    public class InterestRepository : IRepository<Interest, int>
    {
        private PersonDbContext _context;
        public InterestRepository(PersonDbContext context)
        {
            _context = context;
        }
        public async Task<Interest> Delete(int id)
        {
            var interest = await _context.Interests.FirstOrDefaultAsync(interest => interest.InterestId == id);
            if(interest != null)
            {
                _context.Interests.Remove(interest);
                return interest;
            }
            return null;
        }

        public async Task<IEnumerable<Interest>> GetAll()
        {
            return await _context.Interests.ToListAsync();
        }

        public async Task<Interest> GetById(int id)
        {
            return await _context.Interests.FirstOrDefaultAsync(interest => interest.InterestId == id);
        }

        public async Task<Interest> Insert(Interest entity)
        {
            var result = await _context.Interests.AddAsync(entity);
            return result.Entity;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
