using Domain.People.Entity;
using Domain.People.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.People
{
    public class PersonCommandRepository : IPersonCommandRepository
    {
        private readonly AppDbContext context;

        public PersonCommandRepository(AppDbContext context)
        {
            this.context = context;
        }

        public void Add(Person person)
        {
            context.People.Add(person);
            context.SaveChanges();
        }
    }
}
