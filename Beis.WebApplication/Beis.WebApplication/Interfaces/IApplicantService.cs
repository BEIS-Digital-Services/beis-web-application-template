namespace Beis.WebApplication.Interfaces
{
    public interface IApplicantService
    {
        Task AddApplicantToDbAndGenerateVerificatonLink(WebApplicationDto applicant);
    }
}