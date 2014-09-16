#if ASYNC
using System.Threading.Tasks;
#endif
using PatientRewardsHubApi_v2.Models.Authentications;
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

        public Authentications(string yourPatientRewardsHubUrl, string user, string password, string application_token)
            : base(yourPatientRewardsHubUrl, user, password, application_token)
        {
        }
#if SYNC
        public Authentication CreateAuthentication()
        {
            var body = new Authentication { Username = this.User, Password = this.Password, ApplicationToken = this.ApplicationToken };
            Authentication retAuthentication = GenericPost<Authentication>("authentication", body);

            return retAuthentication;
        }
#endif
    }
}
