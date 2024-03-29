using Domain.People.Entity;
using Domain.People.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zamin.Core.Domain.ValueObjects;

namespace Infrastructure.People
{
    public class PersonCommandRepository : IPersonCommandRepository
    {
        private readonly AppDbContext _context;

        public PersonCommandRepository(AppDbContext context)
        {
            this._context = context;
        }

        public void Add(Person person)
        {
            _context.People.Add(person);
            _context.SaveChanges();
        }

        public Person Get(BusinessId businessId)
        {
            return _context.People.AsEnumerable().First(c => c.BusinessId.Value == businessId.Value);
        }
    }
}
