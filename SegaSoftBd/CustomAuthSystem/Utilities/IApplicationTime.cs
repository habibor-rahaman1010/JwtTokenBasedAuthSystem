namespace CustomAuthSystem.Utilities
{
    public interface IApplicationTime
    {
        public DateTime GetCurrentTime();
        public DateTime GetUtcNowTime();
    }
}
