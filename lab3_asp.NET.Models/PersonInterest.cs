using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace lab3_asp.NET.Models
{
    public class PersonInterest
    {
        [Key]
        public int PersonInterestId { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int InterestId { get; set; }
        public Interest Interest { get; set; }
    }
}
