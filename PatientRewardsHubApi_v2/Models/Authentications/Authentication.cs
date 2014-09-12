// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace PatientRewardsHubApi_v2.Models.Authentications
{

    public class Authentication
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
