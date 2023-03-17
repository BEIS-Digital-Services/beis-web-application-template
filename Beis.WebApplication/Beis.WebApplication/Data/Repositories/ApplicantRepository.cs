using Beis.Common.Entities;
using Beis.Common.Entities.Interfaces;
using Beis.Common.Entities.Models;
using Beis.Common.Repositories;
using Beis.WebApplication.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Beis.WebApplication.Data.Repositories
{
    public class ApplicantRepository : BaseUsersRepository, IApplicantRepository
    {
        private readonly WebApplicationDbContext<Applicant> _dbContext;

        public ApplicantRepository(ILogger<BaseUsersRepository> logger, WebApplicationDbContext<Applicant> dbContext) 
            : base(logger, dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Result<int>> AddApplicantAsync(WebApplicationDto dto)
        {
            var applicant = new Applicant
            {
                full_name = dto.ApplicantName,
                email_address = dto.ApplicantEmailAddress
               
                
                
            };
            return await base.CreateAsync(applicant);
        }

        public async Task<Result<Applicant>> FindByIdAsync(int userId)
        {
            var user = await _dbContext.applicants.FindAsync(userId);
            if (user == null)
            {
                return Result.Fail("User not found.");
            }
            return Result.Ok<Applicant>(user);
        }

        public async Task<Result<Applicant>> FindByEmailAddressAsync(string emailAddress)
        {
            var user = await _dbContext.applicants.SingleOrDefaultAsync(x => EF.Functions.Like(x.email_address, emailAddress));
            if (user == null)
            {
                return Result.Fail($"User {emailAddress} not found.");
            }
            return Result.Ok<Applicant>(user);
        }
    }
  
}
