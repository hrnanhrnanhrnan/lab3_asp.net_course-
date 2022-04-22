using lab3_asp.NET.API.Contexts;
using lab3_asp.NET.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab3_asp.NET.API.Repositories
{
    // repository for Persons that implements the IRepository interface
    public class PersonRepository : IRepository<Person, int>
    {
        private PersonDbContext _context;
        public PersonRepository(PersonDbContext context)
        {
            _context = context;
        }
        public async Task Delete(int id)
        {
            var personToDelete = await _context.Persons.FirstOrDefaultAsync(person => person.PersonId == id);
            if(personToDelete != null)
            {
                _context.Persons.Remove(personToDelete);
            }
        }

        public async Task<IEnumerable<Person>> GetAll()
        {
            return await _context.Persons.ToListAsync();
        }

        public async Task<Person> GetById(int id)
        {
            return await _context.Persons.FirstOrDefaultAsync(person => person.PersonId == id);
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
