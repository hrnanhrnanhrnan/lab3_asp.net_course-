using lab3_asp.NET.API.Contexts;
using lab3_asp.NET.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab3_asp.NET.API.Repositories
{
    // repository for Links that implements the IRepository interface
    public class LinkRepository : IRepository<Link, int>
    {
        private PersonDbContext _context;
        public LinkRepository(PersonDbContext context)
        {
            _context = context;
        }
        public async Task<Link> Delete(int id)
        {
            var link = await _context.Links.FirstOrDefaultAsync(link => link.LinkId == id);
            if(link != null)
            {
                _context.Links.Remove(link);
                return link;
            }
            return null;
        }

        public async Task<IEnumerable<Link>> GetAll()
        {
            return await _context.Links.ToListAsync();
        }

        public async Task<Link> GetById(int id)
        {
            return await _context.Links.FirstOrDefaultAsync(link => link.LinkId == id);
        }

        public async Task<Link> Insert(Link entity)
        {
            var result = await _context.Links.AddAsync(entity);
            return result.Entity;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
