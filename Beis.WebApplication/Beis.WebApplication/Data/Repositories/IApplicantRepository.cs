using Beis.Common.Entities.Models;
using Beis.Common.Repositories.Interfaces;

namespace Beis.WebApplication.Data.Repositories
{
    public interface IApplicantRepository : IBaseUsersRepository
    {
        Task<Result<int>> AddApplicantAsync(WebApplicationDto dto);
        Task<Result<Applicant>> FindByEmailAddressAsync(string emailAddress);
        Task<Result<Applicant>> FindByIdAsync(int userId);
    }
}