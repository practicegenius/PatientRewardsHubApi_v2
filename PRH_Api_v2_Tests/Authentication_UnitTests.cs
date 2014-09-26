using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatientRewardsHubApi_v2;
using PatientRewardsHubApi_v2.Models.Authentications;

namespace PRH_Api_v2_Tests
{
    [TestClass]
    public class Authentication_UnitTests
    {       
        private Random random = new Random();

        [TestMethod]
        public void Test_Authentication_GetAccessToken()
        {

            PatientRewardsHubApi api = new PatientRewardsHubApi("http://api.patientrewardshub.com");
            var res = api.Authentications.CreateAuthentication("", "", ""); // username, password, application 

            Assert.IsTrue(res.AccessToken == "");  //AccessToken is the old APIKey
        }

        [TestMethod]
        public void Test_Authentication_GetAccessToken_Constructor()
        {

            var authentications = new PatientRewardsHubApi_v2.Requests.Authentications("https://api.patientrewardshub.com", "", "", "");
            var res = authentications.CreateAuthentication();

            Assert.IsTrue(res.AccessToken == "n9UfoneWE3qA2GHA");  //AccessToken is the old APIKey
        }
    }
}
