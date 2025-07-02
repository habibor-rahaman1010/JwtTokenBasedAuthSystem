using CustomAuthSystem.DatabaseContext;
using CustomAuthSystem.RepositoriesInterafce;
using CustomAuthSystem.UnitOfWorksInterface;

namespace CustomAuthSystem.UnitOfWorks
{
    public class SegaSoftBbUnitOfWork : UnitOfWork, ISegaSoftBbUnitOfWork
    {
        private readonly SegaSoftDbContext _dbContext;
        public IPersonRepository PersonRepository { get; private set; }

        public SegaSoftBbUnitOfWork(SegaSoftDbContext dbContext, 
            IPersonRepository personRepository) : base(dbContext)
        {
            _dbContext = dbContext;
            PersonRepository = personRepository;

        }
    }
}
