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
    public class LinksController : ControllerBase
    {
        private readonly IRepository<Person, int> _personRepository;
        private readonly IRepository<Interest, int> _interestRepository;
        private readonly IRepository<Link, int> _linkRepository;
        private readonly IRepository<PersonInterest, int> _personInterestRepository;

        public LinksController(IRepository<Person, int> personRepository,
            IRepository<Interest, int> interestRepository,
            IRepository<Link, int> linkRepository,
            IRepository<PersonInterest, int> personInterestRepository)
        {
            _personRepository = personRepository;
            _interestRepository = interestRepository;
            _linkRepository = linkRepository;
            _personInterestRepository = personInterestRepository;
        }
        //endpoint to fetch all links
        [HttpGet]
        public async Task<IActionResult> GetAllLinks()
        {
            try
            {
                var links = await _linkRepository.GetAll();
                if (links.ToList().Count != 0)
                {
                    return Ok(links);
                }
                else
                {
                    return NotFound("Could not find any links...");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error when trying to retrieve data from the database");
            }
        }

        //endpoint to fetch link from links table by id
        [HttpGet("getbyid:{id}")]
        public async Task<IActionResult> GetLinkById(int id)
        {
            try
            {
                var result = await _linkRepository.GetById(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error to retrieve data from Database");
            }
        }


        //endpoint to fetch all links connected to a specific person
        [HttpGet("{name}")]
        public async Task<IActionResult> GetAllLinksFromSpecifiedPerson(string name)
        {
            try
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
                    return NotFound($"Could not find any links for a person with a name that contains '{name}'...");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error when trying to retrieve data from the database");
            }
        }

        //endpoint to update link for a specific person and a specific interest
        [HttpPost("{nameOfPerson}/{nameOfInterest}")]
        public async Task<ActionResult<Link>> UpdateLinkForSpecifiedPersonAndInterest(string nameOfPerson, string nameOfInterest, Link link)
        {
            try
            {
                if(link == null)
                {
                    return BadRequest();
                }

                var persons = await _personRepository.GetAll();
                var person = persons.PersonFromName(nameOfPerson);

                var interests = await _interestRepository.GetAll();
                var interest = interests.InterestFromName(nameOfInterest);

                var personInterests = await _personInterestRepository.GetAll();

                if (person != null && interest != null)
                {
                    var personInterest = personInterests.FirstOrDefault(pi => pi.PersonId == person.PersonId && pi.InterestId == interest.InterestId);

                    if (personInterest != null)
                    {
                        link.Interest = interest;
                        var insertedLink = await _linkRepository.Insert(link);
                        await _linkRepository.Save();
                        return CreatedAtAction(nameof(GetLinkById), new { id=insertedLink.LinkId }, new { LinkId=insertedLink.LinkId, Url=insertedLink.Url });
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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error when connecting to the database");
            }
        }
    }
}
