#if ASYNC
using System.Threading.Tasks;
#endif
using PatientRewardsHubApi_v2.Models.Authentications;
using ZendeskApi_v2.Models.Appointments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatientRewardsHubApi_v2.Requests
{
    public class Authentications: Core 
    {
        //public Authentications(string yourPatientRewardsHubUrl, string apiToken)
        //    : base(yourPatientRewardsHubUrl, apiToken)
        //{
        //}

        public Authentications(string yourPatientRewardsHubUrl)
            : base(yourPatientRewardsHubUrl, "")
        {
        } 

#if SYNC
        //public AuthenticationResponse GetAuthenticationAccessToken(string accesstoken)
        //{
        //    AuthenticationResponse authenticationResponse = new AuthenticationResponse();
        //    Authentication authentication = GenericPost<Authentication>(string.Format("access_token/{0}", accesstoken));
        //    authenticationResponse.Authentication = authentication;
        //    authenticationResponse.Authentication.AccessToken = (string)accesstoken;
        //    return authenticationResponse;
        //}

        public Authentication CreateAuthentication(string userName, string password)
        {
            this.User = userName;
            this.Password = password;
            var body = new Authentication { Username = this.User, Password = this.Password, ApiToken = this.ApiToken };
            //AuthenticationResponse authenticationResponse = new AuthenticationResponse();
            Authentication retAuthentication = GenericPost<Authentication>("authentication", body);

            return retAuthentication;
        }
        //public Authentication CreateAuthentication(string userName, string password)
        //{
        //    this.User = userName;
        //    this.Password = password;
        //    return CreateAuthentication();
        //}
#endif
    }
}
