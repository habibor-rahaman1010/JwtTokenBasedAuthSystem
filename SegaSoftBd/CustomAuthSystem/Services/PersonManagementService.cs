using CustomAuthSystem.DomainEntities;
using CustomAuthSystem.ServicesInterface;
using CustomAuthSystem.UnitOfWorksInterface;
using System.Linq.Expressions;

namespace CustomAuthSystem.Services
{
    public class PersonManagementService : IPersonManagementService
    {
        private readonly ISegaSoftBbUnitOfWork _segaSoftBbUnitOfWork;

        public PersonManagementService(ISegaSoftBbUnitOfWork segaSoftBbUnitOfWork)
        {
            _segaSoftBbUnitOfWork = segaSoftBbUnitOfWork;
        }
        public async Task AddPersonAsync(Person person)
        {
            await _segaSoftBbUnitOfWork.PersonRepository.AddAsync(person);
            await _segaSoftBbUnitOfWork.SaveAsync();
        }

        public async Task<(IList<Person> Items, int CurrentPage, int TotalPages, int TotalItems)> GetPersonAsync(int pageIndex, int pageSize, string? search = null, CancellationToken cancellationToken = default)
        {
            Expression<Func<Person, bool>>? filter = null;
            if (!string.IsNullOrWhiteSpace(search))
            {
                filter = x => x.Name.Contains(search) || x.Email.Contains(search);
            }
            return await _segaSoftBbUnitOfWork.PersonRepository.GetAllAsync(pageIndex, pageSize, filter, null, cancellationToken);
        }

        public async Task<Person> GetByIdPersonAsync(Guid id)
        {
            return await _segaSoftBbUnitOfWork.PersonRepository.GetByIdAsync(id);
        }

        public async Task DeletePersonAsync(Person person)
        {
            await _segaSoftBbUnitOfWork.PersonRepository.DeleteAsync(person);
            await _segaSoftBbUnitOfWork.SaveAsync();
        }

        public async Task UpdatePersonAsync(Person person)
        {
            await _segaSoftBbUnitOfWork.PersonRepository.UpdateAsync(person);
            await _segaSoftBbUnitOfWork.SaveAsync();
        }
    }
}
