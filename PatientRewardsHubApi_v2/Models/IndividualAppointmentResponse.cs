using Newtonsoft.Json;
using PatientRewardsHubApi_v2.Models.Appointments;

namespace PatientRewardsHubApi_v2.Models.Appointments
{
    public class IndividualAppointmentResponse
    {
        [JsonProperty("appointment")]
        public Appointment Appointment { get; set; }
    }
}