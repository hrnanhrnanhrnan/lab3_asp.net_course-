using lab3_asp.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab3_asp.NET.API.Utilities
{
    public static class ExtensionMethods
    {
        public static Person PersonFromName(this IEnumerable<Person> persons, string name)
        {
            return persons.FirstOrDefault(person => person.Name.ToLower().Contains(name.ToLower()));
        }

        public static Interest InterestFromName(this IEnumerable<Interest> interests, string name)
        {
            return interests.FirstOrDefault(interest => interest.Title.ToLower().Contains(name.ToLower()));
        }
    }
}
