using Domain.People.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zamin.Core.Domain.ValueObjects;

namespace Domain.People.Repositories
{
    public interface IPersonCommandRepository
    {
        void Add(Person person);
        Person Get(BusinessId id);
    }
}
