using System.Net;

namespace PatientRewardsHubApi_v2
{
    public class RequestResult
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public string Content { get; set; }
    }
}