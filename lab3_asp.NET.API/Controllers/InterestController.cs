using lab3_asp.NET.API.Repositories;
using lab3_asp.NET.API.Utilities;
using lab3_asp.NET.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab3_asp.NET.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterestController : ControllerBase
    {
        private readonly IRepository<Person, int> _personRepository;
        private readonly IRepository<Interest, int> _interestRepository;
        private readonly IRepository<Link, int> _linkRepository;
        private readonly IRepository<PersonInterest, int> _personInterestRepository;

        public InterestController(IRepository<Person, int> personRepository,
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
        public async Task<IActionResult> GetAllInterests()
        {
            var interests = await _interestRepository.GetAll();

            if (interests.ToList().Count != 0)
            {
                return Ok(interests);
            }
            else
            {
                return NotFound("Could not find any interests...");
            }
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetAllInterestFromSpecifiedPerson(string name)
        {
            var persons = await _personRepository.GetAll();
            var personFromName = persons.PersonFromName(name);
            if(personFromName != null)
            {
                var person = await _personRepository.GetById(personFromName.PersonId);
                var personInterests = await _personInterestRepository.GetAll();
                var interests = await _interestRepository.GetAll();
                var query = personInterests.Where(pi => pi.PersonId == person.PersonId)
                    .Join(interests, o => o.InterestId, i => i.InterestId, (pi, interest) => new { Title = interest.Title, Description = interest.Description });
                return Ok(query.Distinct());
            }
            else
            {
                return NotFound($"Could not find any interests linked to a person with a name that contains '{name}'...");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePersonInterest(PersonInterest personInterest)
        {
            await _personInterestRepository.Insert(personInterest);
            await _personInterestRepository.Save();
            return Ok(personInterest);
        }

    }
}
