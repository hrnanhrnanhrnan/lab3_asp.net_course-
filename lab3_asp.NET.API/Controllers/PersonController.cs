using lab3_asp.NET.API.Repositories;
using lab3_asp.NET.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace lab3_asp.NET.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IRepository<Person, string> _personRepository;
        private readonly IRepository<Interest, string> _interestRepository;
        private readonly IRepository<Link, string> _linkRepository;
        private readonly IRepository<PersonInterest, string> _personInterestRepository;

        public PersonController(IRepository<Person, string> personRepository,
            IRepository<Interest, string> interestRepository, 
            IRepository<Link, string> linkRepository, 
            IRepository<PersonInterest, string> personInterestRepository)
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
        public async Task<IActionResult> GetAllLinksFromSpecifiedPerson(string name)
        {

            var person = await _personRepository.GetSpecified(name);
            if(person != null)
            {
                
                var personInterests = await _personInterestRepository.GetAll();
                var interests = await _interestRepository.GetAll();
                var links = await _linkRepository.GetAll();
                var query = personInterests.ToList().Where(pi => pi.PersonId == person.PersonId)
                    .Join(interests, o => o.InterestId, i => i.InterestId, (o, i) => new { o, i })
                    .Join(links, o => o.i.InterestId, i => i.InterestId, (interest, link) => new { Url = link.Url });
                return Ok(query);
            }
            else
            {
                return NotFound($"Could not find any links for a person with a name that consist of '{name}'...");
            }
            

            //var personInterests = _personRepository.GetAll().Where(person => person.Name.ToLower().Contains(name.ToLower()))
            //    .Join(_personInterestRepository.GetAll(), o => o.PersonId, i => i.PersonId,
            //    (o, i) => new { o, i })
            //    .Join(_interestRepository.GetAll(), o => o.i.InterestId, i => i.InterestId, 
            //    (person, interest) => new Interest { Title = interest.Title, Description = interest.Description, Links = interest.Links })
            //    .ToList();
            //if(personInterests.Count != 0)
            //{
            //    return Ok(personInterests);
            //}
            //else
            //{
            //    return NotFound($"Could not find any interest from a person with a name that consist of '{name}'...");
            //}
        }
    }
}
