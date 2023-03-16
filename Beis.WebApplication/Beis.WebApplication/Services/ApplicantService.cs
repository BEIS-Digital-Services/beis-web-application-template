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
