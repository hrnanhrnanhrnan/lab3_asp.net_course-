using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace lab3_asp.NET.Models
{
    public class Person
    {
        [Key]
        public int PersonId { get; set; }
        [Required]
        public string Name { get; set; }
        public string TelephoneNumber { get; set; }
    }
}
