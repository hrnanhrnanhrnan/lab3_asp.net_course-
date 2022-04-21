using lab3_asp.NET.API.Contexts;
using lab3_asp.NET.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab3_asp.NET.API.Repositories
{
    public class PersonInterestRepository : IRepository<PersonInterest, string>
    {
        private PersonDbContext _context;
        public PersonInterestRepository(PersonDbContext context)
        {
            _context = context;
        }
        public async Task Delete(string name)
        {
            //var personInterests = await _context.PersonInterests.ToListAsync();
            var persons = await _context.Persons.ToListAsync();
            var interests = await _context.Interests.ToListAsync();

            var person = persons.FirstOrDefault(p => p.Name.ToLower().Contains(name.ToLower()))?.PersonId;
            var interest = interests.FirstOrDefault(i => i.Title.ToLower().Contains(name.ToLower())).InterestId;
            var personInterest = await _context.PersonInterests.FirstOrDefaultAsync(pi => pi.InterestId == interest || pi.PersonId == person);

            //var personInterest = _context.PersonInterests
            //    .Include(p => p.Person).ThenInclude(p => p.Name)
            //    .Include(i => i.Interest).ThenInclude(i => i.Title)
            //    .FirstOrDefault(pi => pi.Person.Name.ToLower().Contains(name.ToLower()) || pi.Interest.Title.ToLower().Contains(name.ToLower()));
            if (personInterest != null)
            {
                _context.PersonInterests.Remove(personInterest);
            }
        }

        public async Task<IEnumerable<PersonInterest>> GetAll()
        {
            return await _context.PersonInterests.ToListAsync();
        }

        public async Task<PersonInterest> GetSpecified(string name)
        {
            var persons = await _context.Persons.ToListAsync();
            var interests = await _context.Interests.ToListAsync();

            var person = persons.FirstOrDefault(p => p.Name.ToLower().Contains(name.ToLower()))?.PersonId;
            var interest = interests.FirstOrDefault(i => i.Title.ToLower().Contains(name.ToLower())).InterestId;
            return await _context.PersonInterests.FirstOrDefaultAsync(pi => pi.InterestId == interest || pi.PersonId == person);
            //return _context.PersonInterests
            //    .Include(p => p.Person).ThenInclude(p => p.Name)
            //    .Include(i => i.Interest).ThenInclude(i => i.Title)
            //    .FirstOrDefault(pi => pi.Person.Name.ToLower().Contains(name.ToLower()) || pi.Interest.Title.ToLower().Contains(name.ToLower()));
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
