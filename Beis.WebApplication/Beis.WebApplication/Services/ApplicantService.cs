using Beis.Common.Entities.Models;
using Beis.WebApplication.Data;
using Beis.WebApplication.Data.Repositories;

namespace Beis.WebApplication.Services
{
    public class ApplicantService : IApplicantService
    {
        private readonly IApplicantRepository _applicantRepository;
        private readonly IEmailVerificationService _emailVerificationService;
        public ApplicantService(IApplicantRepository applicantRepository, IEmailVerificationService emailVerificationService)
        {
            _applicantRepository = applicantRepository;
            _emailVerificationService = emailVerificationService;
        }

        public async Task<Result<int>> CreateApplicantAsync(WebApplicationDto applicant)
        {
            return await _applicantRepository.AddApplicantAsync(applicant);
            
        }
        public async Task<Result> UpdateApplicantAsync(WebApplicationDto applicant)
        {
            throw new NotImplementedException();
        }


        public async Task<Result> LoadAplicantAndGenerateVerificationLinkAsync(WebApplicationDto applicant)
        {
            int userId = 0;
            var userResponse = await _applicantRepository.FindByEmailAddressAsync(applicant.ApplicantEmailAddress);
            if (userResponse.IsSuccess)
            {
                // send new verification email
                userId = userResponse.Value.id;
            }
            else
            {
                return Result.Fail("Unable to find the email address."); // todo have a result for this to be handled up the chain.
            }


            return await _emailVerificationService.SendVerifyEmailNotificationAsync(userId, RouteNames.MyAccountPage);

        }


        public async Task AddApplicantToDbAndGenerateVerificatonLink(WebApplicationDto applicant)
        {
            int userId = 0;
            var userResponse = await _applicantRepository.FindByEmailAddressAsync(applicant.ApplicantEmailAddress);
            if (userResponse.IsSuccess)
            {
                // send new verification email
                userId = userResponse.Value.id;
            }
            else
            {
                var createResult = await _applicantRepository.AddApplicantAsync(applicant);
                if (createResult.IsFailed)
                {
                    return;
                }
                userId = createResult.Value;
            }
           

            await _emailVerificationService.SendVerifyEmailNotificationAsync(userId, RouteNames.MyAccountPage);

        }

    }
}
