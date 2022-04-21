using lab3_asp.NET.API.Repositories;
using lab3_asp.NET.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using lab3_asp.NET.API.Utilities;

namespace lab3_asp.NET.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IRepository<Person, int> _personRepository;
        private readonly IRepository<Interest, int> _interestRepository;
        private readonly IRepository<Link, int> _linkRepository;
        private readonly IRepository<PersonInterest, int> _personInterestRepository;

        public PersonController(IRepository<Person, int> personRepository,
            IRepository<Interest, int> interestRepository, 
            IRepository<Link, int> linkRepository, 
            IRepository<PersonInterest, int> personInterestRepository)
        {
            _personRepository = personRepository;
            _interestRepository = interestRepository;
            _linkRepository = linkRepository;
            _personInterestRepository = personInterestRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPersons()
        {
            var persons = await _personRepository.GetAll();
            if (persons.ToList().Count != 0)
            {
                return Ok(persons); 
            }
            else
            {
                return NotFound($"Could not find any persons...");
            }

        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetPersonByName(string name)
        {
            var persons = await _personRepository.GetAll();
            var person = persons.PersonFromName(name);
            if (person != null)
            {
                return Ok(person);
            }
            else
            {
                return NotFound($"Could not find any links for a person with a name that consist of '{name}'...");
            }
        }
    }
}
