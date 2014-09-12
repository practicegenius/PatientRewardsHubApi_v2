using Newtonsoft.Json;
using PatientRewardsHubApi_v2.Models.Authentications;

namespace PatientRewardsHubApi_v2.Models.Authentications
{
    public class AuthenticationResponse
    {
        [JsonProperty("authentication")]
        public Authentication Authentication { get; set; }
    }
}
