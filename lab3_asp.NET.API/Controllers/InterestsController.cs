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
    public class InterestsController : ControllerBase
    {
        private readonly IRepository<Person, int> _personRepository;
        private readonly IRepository<Interest, int> _interestRepository;
        private readonly IRepository<Link, int> _linkRepository;
        private readonly IRepository<PersonInterest, int> _personInterestRepository;

        public InterestsController(IRepository<Person, int> personRepository,
            IRepository<Interest, int> interestRepository,
            IRepository<Link, int> linkRepository,
            IRepository<PersonInterest, int> personInterestRepository)
        {
            _personRepository = personRepository;
            _interestRepository = interestRepository;
            _linkRepository = linkRepository;
            _personInterestRepository = personInterestRepository;
        }

        //endpoint to fetch all interests
        [HttpGet]
        public async Task<IActionResult> GetAllInterests()
        {
            try
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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error when trying to retrieve data from the database");
            }
        }

        //endpoint to fetch interests from interest table by id
        [HttpGet("interestbyid:{id}")]
        public async Task<IActionResult> GetInterestById(int id)
        {
            try
            {
                var result = await _interestRepository.GetById(id);
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

        //endpoint to fetch personinterest from personinterest table by id
        [HttpGet("personinterestbyid:{id}")]
        public async Task<IActionResult> GetPersonInterestById(int id)
        {
            try
            {
                var result = await _personInterestRepository.GetById(id);
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

        //endpoint to fetch all interests that is connected to a person filtered on name
        [HttpGet("{name}")]
        public async Task<IActionResult> GetAllInterestFromSpecifiedPerson(string name)
        {
            try
            {
                var persons = await _personRepository.GetAll();
                var personFromName = persons.PersonFromName(name);
                if (personFromName != null)
                {
                    var person = await _personRepository.GetById(personFromName.PersonId);
                    var interests = await _interestRepository.GetAll();
                    var query = interests.Join(await _personInterestRepository.GetAll(), o => o.InterestId, i => i.InterestId, (o, i) => new { o, i })
                        .Join(persons, o => o.i.PersonId, i => i.PersonId, (interest, person) => new { Person = person, Interest = interest.o, Links = interest.o.Links })
                        .Where(a => a.Person.PersonId == person.PersonId);
                    return Ok(query.Distinct());
                }
                else
                {
                    return NotFound($"Could not find any interests linked to a person with a name that contains '{name}'...");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error when trying to retrieve data from the database");
            }
        }

        //endpoint to connect person to interest
        [HttpPost]
        public async Task<ActionResult<PersonInterest>> CreatePersonInterest(PersonInterest personInterest)
        {
            try
            {
                var pi = await _personInterestRepository.GetAll(); 
                if(!(pi.Any(pi => pi.PersonId == personInterest.PersonId) && pi.Any(pi => pi.InterestId == personInterest.InterestId)))
                {
                    return BadRequest();
                }
                var createdPI = await _personInterestRepository.Insert(personInterest);
                await _personInterestRepository.Save();
                return CreatedAtAction(nameof(GetPersonInterestById), new { id =createdPI.PersonInterestId}, personInterest);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error when trying to insert data to the database");
            }
        }

    }
}
