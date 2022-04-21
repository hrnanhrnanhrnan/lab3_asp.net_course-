using lab3_asp.NET.API.Contexts;
using lab3_asp.NET.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab3_asp.NET.API.Repositories
{
    public class PersonRepository : IRepository<Person, string>
    {
        private PersonDbContext _context;
        public PersonRepository(PersonDbContext context)
        {
            _context = context;
        }
        public async Task Delete(string name)
        {
            var personToDelete = await _context.Persons.FirstOrDefaultAsync(person => person.Name.ToLower().Contains(name.ToLower()));
            if(personToDelete != null)
            {
                _context.Persons.Remove(personToDelete);
            }
        }

        public async Task<IEnumerable<Person>> GetAll()
        {
            return await _context.Persons.ToListAsync();
        }

        public async Task<Person> GetSpecified(string name)
        {
            return await _context.Persons.FirstOrDefaultAsync(person => person.Name.ToLower().Contains(name.ToLower()));
        }

        public async Task<Person> Insert(Person entity)
        {
            await _context.Persons.AddAsync(entity);
            return entity;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
