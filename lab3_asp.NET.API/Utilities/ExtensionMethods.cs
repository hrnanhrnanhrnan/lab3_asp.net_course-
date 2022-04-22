using lab3_asp.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab3_asp.NET.API.Utilities
{
    public static class ExtensionMethods
    {
        // extension method to IEnumerable<Person> to fetch and return a single person from the name parameter
        public static Person PersonFromName(this IEnumerable<Person> persons, string name)
        {
            return persons.FirstOrDefault(person => person.Name.ToLower().Contains(name.ToLower()));
        }

        // extension method to IEnumerable<Interest> to fetch and return a single Interest from the name parameter
        public static Interest InterestFromName(this IEnumerable<Interest> interests, string name)
        {
            return interests.FirstOrDefault(interest => interest.Title.ToLower().Contains(name.ToLower()));
        }
    }
}
