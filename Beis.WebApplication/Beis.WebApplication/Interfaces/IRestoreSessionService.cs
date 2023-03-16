namespace Beis.WebApplication.Interfaces
{
    public interface IRestoreSessionService
    {
        Task<Result> RestoreSessionFromDb(string emailAddress);
    }
}