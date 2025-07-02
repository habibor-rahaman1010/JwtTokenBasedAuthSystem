using CustomAuthSystem.DomainEntities;

namespace CustomAuthSystem.ServicesInterface
{
    public interface IPersonManagementService
    {
        public Task<(IList<Person> Items, int CurrentPage, int TotalPages, int TotalItems)> GetPersonAsync(int pageIndex, int pageSize,
            string? search = null, CancellationToken cancellationToken = default);

        public Task<Person> GetByIdPersonAsync(Guid id);
        public Task AddPersonAsync(Person person);
        public Task UpdatePersonAsync(Person person);
        public Task DeletePersonAsync(Person person);
    }
}
