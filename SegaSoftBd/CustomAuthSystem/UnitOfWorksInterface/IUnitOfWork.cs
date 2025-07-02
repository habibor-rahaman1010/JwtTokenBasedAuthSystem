namespace CustomAuthSystem.UnitOfWorksInterface
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        public void Save();
        public Task SaveAsync();
    }
}
