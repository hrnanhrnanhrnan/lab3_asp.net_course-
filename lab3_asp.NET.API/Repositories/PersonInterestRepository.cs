using lab3_asp.NET.API.Contexts;
using lab3_asp.NET.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab3_asp.NET.API.Repositories
{
    // repository for PersonInterest that implements the IRepository interface
    public class PersonInterestRepository : IRepository<PersonInterest, int>
    {
        private PersonDbContext _context;
        public PersonInterestRepository(PersonDbContext context)
        {
            _context = context;
        }
        public async Task Delete(int id)
        {
            var personInterest = await _context.PersonInterests.FirstOrDefaultAsync(pi => pi.PersonInterestId == id);

            if (personInterest != null)
            {
                _context.PersonInterests.Remove(personInterest);
            }
        }

        public async Task<IEnumerable<PersonInterest>> GetAll()
        {
            return await _context.PersonInterests.ToListAsync();
        }

        public async Task<PersonInterest> GetById(int id)
        {
            return await _context.PersonInterests.FirstOrDefaultAsync(pi => pi.PersonInterestId== id);
        }

        public async Task<PersonInterest> Insert(PersonInterest entity)
        {
            await _context.PersonInterests.AddAsync(entity);
            return entity;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
