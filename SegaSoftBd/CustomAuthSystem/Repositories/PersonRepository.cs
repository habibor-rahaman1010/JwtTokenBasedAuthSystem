using CustomAuthSystem.DatabaseContext;
using CustomAuthSystem.DomainEntities;
using CustomAuthSystem.RepositoriesInterafce;

namespace CustomAuthSystem.Repositories
{
    public class PersonRepository : Repository<Person, Guid>, IPersonRepository
    {
        private readonly SegaSoftDbContext _segaSoftDbContext;

        public PersonRepository(SegaSoftDbContext dbContext) : base(dbContext)
        {
            _segaSoftDbContext = dbContext;
        }
    }
}
