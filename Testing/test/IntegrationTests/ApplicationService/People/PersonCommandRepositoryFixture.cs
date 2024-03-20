using Domain.People.Repositories;
using Infrastructure;
using Infrastructure.People;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTests.ApplicationService.People
{
    public class PersonCommandRepositoryFixture : IDisposable
    {
        public IPersonCommandRepository PersonCommandRepository { get; }
        public AppDbContext Context { get; }

        public PersonCommandRepositoryFixture()
        {

            DbContextOptionsBuilder<AppDbContext> builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseSqlServer("server=.,1433; database=PeopleIntegrationTest; user id=sa; password=Abcd_1234; MultipleActiveResultSets=True; TrustServerCertificate=True;");
            Context = new AppDbContext(builder.Options);
            Context.Database.EnsureCreated();
            PersonCommandRepository = new PersonCommandRepository(Context);
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
        }
    }
}
