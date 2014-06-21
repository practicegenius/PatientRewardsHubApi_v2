// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PatientRewardsHubApi_v2.Models.Appointments;

namespace ZendeskApi_v2.Models.Appointments
{

    public class GroupAppointmentResponse : GroupResponseBase
    {
        [JsonProperty("count")]
        public int? Count { get; set; }

        [JsonProperty("appointments")]
        public IList<Appointment> Appointments { get; set; }
    }
}
