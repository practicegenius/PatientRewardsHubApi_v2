using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatientRewardsHubApi_v2;
using PatientRewardsHubApi_v2.Models.Patients;

namespace PRH_Api_v2_Tests
{
    [TestClass]
    public class Patient_UnitTests
    {
        private PatientRewardsHubApi api = new PatientRewardsHubApi("http://api.v200.branch.patientrewardshub.com/", "apikeyabapikeyab");

        [TestMethod]
        public void Test_GetPatient()
        {
            
            var res = api.Patients.GetPatientById(30);
            Assert.IsTrue(res.Patient.Firstname == "Mickey");
            Assert.IsTrue(res.Patient.Lastname == "Mouse");
            
            
            //Assert.AreEqual(4, res.Patient.Id);
        }

        [TestMethod]
        public void Test_GetPatient_Bad_Name()
        {

            var res = api.Patients.GetPatientById(4);
            Assert.AreNotEqual("Donald", res.Patient.Firstname);

            //Assert.AreEqual(4, res.Patient.Id);
        }

        [TestMethod]
        public void Test_Create_Patient()
        {
            var res = api.Patients.CreatePatient(new Patient()
                                                                {
                                                                    External_id = "22",
                                                                    Firstname = "Minnie",
                                                                    Lastname = "Mouse",
                                                                    Email = "minney@patientRewardsHub.com",
                                                                    Address1 = "344 Main St",
                                                                    City = "San Diego",
                                                                    State = "CA",
                                                                    Zip = "US",
                                                                    Homephone = "6192223344",
                                                                    Birthdate = "1980-05-05"
                                                                });

            Assert.IsTrue(Convert.ToInt32(res.Patient.Id) > 0);
        }

        [TestMethod]
        public void Test_Update_Patient()
        {
            var res = api.Patients.UpdatePatient(new Patient()
            {
                //Id=30,
                External_id = "22",
                Firstname = "Minny",
                Lastname = "Mouse",
                Email = "minney@patientRewardsHub.com",
                Address1 = "344 Main St",
                City = "San Diego",
                State = "CA",
                Zip = "US",
                Homephone = "6192223344",
                Birthdate = "1980-05-05"
            });
            Assert.IsTrue(res.Patient.Firstname == "Minny");
        }
       
    }
}
