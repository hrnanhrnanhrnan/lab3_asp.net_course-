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
    public class LinkController : ControllerBase
    {
        private readonly IRepository<Person, int> _personRepository;
        private readonly IRepository<Interest, int> _interestRepository;
        private readonly IRepository<Link, int> _linkRepository;
        private readonly IRepository<PersonInterest, int> _personInterestRepository;

        public LinkController(IRepository<Person, int> personRepository,
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
        public async Task<IActionResult> GetAllLinks()
        {
            var links = await _linkRepository.GetAll();
            if(links.ToList().Count != 0)
            {
                return Ok(links);
            }
            else
            {
                return NotFound("Could not find any links...");
            }
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetAllLinksFromSpecifiedPerson(string name)
        {
            var persons = await _personRepository.GetAll();
            var personFromName = persons.PersonFromName(name);
            if (personFromName != null)
            {
                var person = await _personRepository.GetById(personFromName.PersonId);
                var personInterests = await _personInterestRepository.GetAll();
                var interests = await _interestRepository.GetAll();
                var links = await _linkRepository.GetAll();
                var query = personInterests.Where(pi => pi.PersonId == person.PersonId)
                    .Join(interests, o => o.InterestId, i => i.InterestId, (o, i) => new { o, i })
                    .Join(links, o => o.i.InterestId, i => i.InterestId, (interest, link) => new { Url = link.Url });
                return Ok(query.Distinct());
            }
            else
            {
                return NotFound($"Could not find any links for a person with a name that consist of '{name}'...");
            }
        }

        [HttpPost("{nameOfPerson}/{nameOfInterest}")]
        public async Task<IActionResult> UpdateLinkForSpecifiedPersonAndInterest(string nameOfPerson, string nameOfInterest, Link link)
        {
            var persons = await _personRepository.GetAll();
            var person = persons.PersonFromName(nameOfPerson);

            var interests = await _interestRepository.GetAll();
            var interest = interests.InterestFromName(nameOfInterest);

            var personInterests = await _personInterestRepository.GetAll();

            if(person != null && interest != null)
            {
                var personInterest = personInterests.FirstOrDefault(pi => pi.PersonId == person.PersonId && pi.InterestId == interest.InterestId);
                
                if (personInterest != null)
                {
                    link.Interest = interest;
                    await _linkRepository.Insert(link);
                    await _linkRepository.Save();
                    return Ok($"Created new link with url: '{link.Url}' for person: '{person.Name}' and for interest '{interest.Title}'!");
                }
                else
                {
                    return NotFound($"Could not find any interests where the title contains '{nameOfInterest}' and the persons name contains '{nameOfPerson}'");
                }
            }
            else
            {
                return NotFound($"No person with name that contains {nameOfPerson}, or interest with title that contains {nameOfInterest}");
            }
        }
    }
}
