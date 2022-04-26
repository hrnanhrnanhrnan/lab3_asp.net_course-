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

        //endpoint to fetch personinterest from personinterest table by id
        [HttpGet("getbyid:{id}")]
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

        //endpoint to fetch all interests filtered on name
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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error when trying to retrieve data from the database");
            }
        }

        //endpoint to connect person to interest
        [HttpPost]
        public async Task<ActionResult<PersonInterest>> UpdatePersonInterest(PersonInterest personInterest)
        {
            try
            {
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
