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
        //private PatientRewardsHubApi api = new PatientRewardsHubApi("http://api.v200.branch.patientrewardshub.com/", "apikeyabapikeyab");
        private PatientRewardsHubApi api = new PatientRewardsHubApi("http://api.stage.patientrewardshub.com", "amigo", "1234ab", "h7kFieazlUMZ9HL9QuMJpoNKbCMG27jj");
        private Random random = new Random();

        [TestMethod]
        public void Test_GetAuthentication()
        {
            var res = api.Authentications.CreateAuthentication();

            Assert.IsTrue(res.AccessToken == "F4FMlXEAuVEx1Hby");
        }
    }
}
