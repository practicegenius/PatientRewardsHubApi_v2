﻿// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PatientRewardsHubApi_v2.Models;
using PatientRewardsHubApi_v2.Models.Patients;

namespace ZendeskApi_v2.Models.Patients
{

    public class GroupPatientResponse : GroupResponseBase
    {
        [JsonProperty("count")]
        public int? Count { get; set; }

        [JsonProperty("patients")]
        public IList<Patient> Patients { get; set; }

        [JsonProperty("Errors")]
        public IList<Error> Errors { get; set; }
    }
}
