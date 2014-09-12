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
        public Authentications(string yourPatientRewardsHubUrl, string apiToken)
            : base(yourPatientRewardsHubUrl, apiToken)
        {
        }

        public Authentications(string yourPatientRewardsHubUrl, string user, string password)
            : base(yourPatientRewardsHubUrl, user, password)
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

        public Authentication CreateAuthentication()
        {
            var body = new Authentication { Username = this.User, Password = this.Password }; 
            //AuthenticationResponse authenticationResponse = new AuthenticationResponse();
            Authentication retAuthentication = GenericPost<Authentication>("authentication", body);

            return retAuthentication;
        }
#endif
    }
}
