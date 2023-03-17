namespace Beis.WebApplication.Interfaces
{
    public interface IApplicantService
    {
        Task AddApplicantToDbAndGenerateVerificatonLink(WebApplicationDto applicant);
        Task<Result<int>> CreateApplicantAsync(WebApplicationDto applicant);
        Task<Result> LoadAplicantAndGenerateVerificationLinkAsync(WebApplicationDto applicant);
    }
}