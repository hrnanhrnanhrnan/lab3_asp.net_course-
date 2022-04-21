﻿using lab3_asp.NET.API.Contexts;
using lab3_asp.NET.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab3_asp.NET.API.Repositories
{
    public class InterestRepository : IRepository<Interest, string>
    {
        private PersonDbContext _context;
        public InterestRepository(PersonDbContext context)
        {
            _context = context;
        }
        public async Task Delete(string name)
        {
            var interest = await _context.Interests.FirstOrDefaultAsync(interest => interest.Title.ToLower().Contains(name.ToLower()));
            if(interest != null)
            {
                _context.Interests.Remove(interest);
            }
        }

        public async Task<IEnumerable<Interest>> GetAll()
        {
            return await _context.Interests.ToListAsync();
        }

        public async Task<Interest> GetSpecified(string name)
        {
            return await _context.Interests.FirstOrDefaultAsync(interest => interest.Title.ToLower().Contains(name.ToLower()));
        }

        public async Task<Interest> Insert(Interest entity)
        {
            await _context.Interests.AddAsync(entity);
            return entity;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}