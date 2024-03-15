﻿using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private List<Person> _people = new List<Person> {
            new Person
                {
                    Id =1,
                    FirstName="Alireza",
                    LastName = "Oroumand"
                },
            new Person
                {
                    Id =2,
                    FirstName="Farid",
                    LastName = "Taheri"
                },
            new Person
                {
                    Id =3,
                    FirstName="Arash",
                    LastName = "Azhdari"
                }
        };

        [HttpGet("{id}")]
        public Person Get(int id = 1)
        {
            return _people.FirstOrDefault(p => p.Id == id);
        }
    }
}
