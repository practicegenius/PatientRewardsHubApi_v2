using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PatientRewardsHubApi_v2.Models
{
    public class PatientError
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// The Integrator's Patient ID
        /// </summary>
        [JsonProperty("external_id")]
        public string External_id { get; set; }

        [JsonProperty("firstname")]
        public string Firstname { get; set; }

        [JsonProperty("middlename")]
        public string Middlename { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("address1")]
        public string Address1 { get; set; }

        [JsonProperty("address2")]
        public string Address2 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("homephone")]
        public string Homephone { get; set; }

        [JsonProperty("birthdate")]
        public string Birthdate { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("responsible_firstname")]
        public string Responsible_firstname { get; set; }

        [JsonProperty("responsible_lastname")]
        public string Responsible_lastname { get; set; }

        [JsonProperty("responsible_email")]
        public string Responsible_email { get; set; }

        [JsonProperty("responsible_cellphone")]
        public string Responsible_cellphone { get; set; }

        [JsonProperty("responsible_homephone")]
        public string Responsible_homephone { get; set; }
    }
}
