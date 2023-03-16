using Beis.Common.Models;

namespace Beis.WebApplication.Models
{
    public class WebApplicationDto : ICookieBannerDto
    {
        public CookieBannerViewModel CookieBannerViewModel { get; set; }
        public string ApplicantName { get;  set; }
        public string ApplicantEmailAddress { get; set; }
    }
}
