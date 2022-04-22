using lab3_asp.NET.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab3_asp.NET.API.Contexts
{
    public class PersonDbContext : DbContext
    {
        public PersonDbContext(DbContextOptions<PersonDbContext> options) : base(options)
        {
        }

        public DbSet<Interest> Interests { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonInterest> PersonInterests { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //seed data
            modelBuilder.Entity<Person>().HasData(new List<Person>()
            {
                new Person(){PersonId=1, Name="Peter", TelephoneNumber="070945094590"},
                new Person(){PersonId=2, Name="Håkan", TelephoneNumber="0090489283"},
                new Person(){PersonId=3, Name="Mendela", TelephoneNumber="090032940"},
                new Person(){PersonId=4, Name="Maja", TelephoneNumber="078578748834"},
                new Person(){PersonId=5, Name="Sandra", TelephoneNumber="0809504905940"}
            });

            modelBuilder.Entity<Interest>().HasData(new List<Interest>()
            {
                new Interest(){InterestId=1, Title="Programming", Description="Programming websites and smart contracts" },
                new Interest(){InterestId=2, Title="Gaming", Description="Play videogames with friends"},
                new Interest(){InterestId=3, Title="Travel", Description="Travel the world"}
            });

            modelBuilder.Entity<Link>().HasData(new List<Link>()
            {
                new Link(){LinkId=1, Url="https://rinkeby.etherscan.io/", InterestId=1},
                new Link(){LinkId=2, Url="https://www.codecademy.com/learn/react-101", InterestId=1},
                new Link(){LinkId=3, Url="https://stackoverflow.com/", InterestId=1},
                new Link(){LinkId=4, Url="https://www.trivago.se/", InterestId=3},
                new Link(){LinkId=5, Url="https://www.pcgamer.com/news/", InterestId=2},
            });

            modelBuilder.Entity<PersonInterest>().HasData(new List<PersonInterest>()
            {
                new PersonInterest(){PersonInterestId=1, PersonId=1, InterestId=1},
                new PersonInterest(){PersonInterestId=2, PersonId=1, InterestId=2},
                new PersonInterest(){PersonInterestId=3, PersonId=2, InterestId=3},
                new PersonInterest(){PersonInterestId=4, PersonId=3, InterestId=2},
                new PersonInterest(){PersonInterestId=5, PersonId=4, InterestId=1},
                new PersonInterest(){PersonInterestId=6, PersonId=4, InterestId=2},
                new PersonInterest(){PersonInterestId=7, PersonId=4, InterestId=3},
                new PersonInterest(){PersonInterestId=8, PersonId=5, InterestId=2},
                new PersonInterest(){PersonInterestId=9, PersonId=1, InterestId=3}
            });
        }
    }
}
