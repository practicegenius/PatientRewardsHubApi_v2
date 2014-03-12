using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatientRewardsHubApi_v2;

namespace PRH_Api_v2_Tests
{
    [TestClass]
    public class UnitTest1
    {
        //private PatientRewardsHubApi api = new PatientRewardsHubApi("http://api.v200.branch.patientrewardshub.com/", "apikeyabapikeyab");
        [TestMethod]
        public void Test_GetPatient()
        {
            PatientRewardsHubApi api = new PatientRewardsHubApi("http://api.v200.branch.patientrewardshub.com/", "apikeyabapikeyab");
            var res = api.Patients.GetPatientById(4);
            Assert.IsTrue(res.Patient.Firstname == "Mickey");
        }
    }
}
