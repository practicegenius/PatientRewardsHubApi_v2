using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PatientRewardsHubApi_v2.Models.Patients;

namespace PatientRewardsHubApi_v2.Models
{
    [JsonObject("Error")]
    public class Error
    {
        [JsonProperty("patient")]
        public Patient patient { get; set; }

        [JsonProperty("errors")]
        public List<PatientError> errors { get; set; }
    }
}
