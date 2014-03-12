// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PatientRewardsHubApi_v2.Helpers;

namespace PatientRewardsHubApi_v2.Models.Appointments
{

    public class Appointment
    {
        /// <summary>
        /// The PRH Appointment ID
        /// </summary>
        [JsonProperty("id")]
        public Int32 Id { get; set; }

        /// <summary>
        /// The Integrator's Appointment ID
        /// </summary>
        [JsonProperty("external_id")]
        public string External_id { get; set; }

        /// <summary>
        /// The PRH Patient ID
        /// </summary>
        [JsonProperty("patient_id")]
        public string Patient_id { get; set; }

        private long _Start_time;
        /// <summary>
        /// The Unix Timestamp format of the Appointment Start Time in UTC
        /// </summary>
        [JsonProperty("start_time")]       		
		protected long Start_time
		{
			get
			{
                return _Start_time; 
			}
			set
			{
				 _Start_time = value;
			}
		}

       [JsonIgnore]
		public DateTime StartTime
		{
			get
			{
				return DateTimeHelper.DateTimeFromUnixTimestampSeconds(Start_time);
			}
			set
			{
                _Start_time = DateTimeHelper.GetUnixTimestampSeconds(value);
			}
		}



        [JsonProperty("description")]
        public string Description { get; set; }

    }
}
