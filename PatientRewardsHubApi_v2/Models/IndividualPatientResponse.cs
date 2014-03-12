using Newtonsoft.Json;
using PatientRewardsHubApi_v2.Models.Patients;

namespace PatientRewardsHubApi_v2.Models.Patients
{
    public class IndividualPatientResponse
    {
        [JsonProperty("patient")]
        public Patient Patient { get; set; }
    }
}