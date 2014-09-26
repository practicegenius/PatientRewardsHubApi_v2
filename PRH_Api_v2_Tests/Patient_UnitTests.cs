using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatientRewardsHubApi_v2;
using PatientRewardsHubApi_v2.Models.Patients;
using PatientRewardsHubApi_v2.Requests;
using System.Linq;

namespace PRH_Api_v2_Tests
{
    [TestClass]
    public class Patient_UnitTests
    {
        private PatientRewardsHubApi api = new PatientRewardsHubApi("http://api.patientrewardshub.com/", "n9UfoneWE3qA2GHA");
        //private PatientRewardsHubApi api = new PatientRewardsHubApi("http://api.stage.patientrewardshub.com", "apikeyabapikeyab");
        private Random random = new Random();
        
        [TestMethod]
        public void Test_GetPatient()
        {
            var res = api.Patients.GetPatients(4, 5);

            Assert.IsTrue(res.Count == res.Patients.Count);
        }

        [TestMethod]
        public void Test_FindPatientsByExternalId()
        {
            PatientSearch patientSearch = new PatientSearch();
            patientSearch.external_id = "12"; 
            var res = api.Patients.GetPatients(patientSearch);
            Assert.IsTrue(res.Count == res.Patients.Count);
        }

        [TestMethod]
        public void Test_FindPatientsByUsername()
        {
            PatientSearch patientSearch = new PatientSearch();
            patientSearch.username = "abcd";
            var res = api.Patients.GetPatients(patientSearch);
            Assert.IsTrue(res.Count == res.Patients.Count);
        }

        [TestMethod]
        public void Test_FindPatientsByLastname_Doe()
        {
            PatientSearch patientSearch = new PatientSearch();
            patientSearch.limit = 10;
            patientSearch.offset = 0;
            patientSearch.lastname = "Doe";
            var res = api.Patients.GetPatients(patientSearch);

            Assert.IsTrue(res.Count == res.Patients.Count);
            var patientniCount = res.Patients.Where(a => a.Lastname == patientSearch.lastname);
            Assert.IsTrue(patientniCount.Count() > 0);
            Assert.IsTrue(patientniCount.Count() == res.Patients.Count);
        }

        [TestMethod]
        public void Test_FindPatientsByFirstname_Princess()
        {
            PatientSearch patientSearch = new PatientSearch();
            patientSearch.limit = 2;
            patientSearch.offset = 0;
            patientSearch.firstname = "Princess";
            var res = api.Patients.GetPatients(patientSearch);

            var batchniCount = res.Patients.Where(a => a.Firstname == patientSearch.firstname);

            Assert.IsTrue(batchniCount.Count() == res.Patients.Count);

            Assert.IsTrue(res.Count == res.Patients.Count);      
        }

        [TestMethod]
        public void Test_FindPatientsByEmail()
        {
            PatientSearch patientSearch = new PatientSearch();
            patientSearch.email = "mickeymouse@mickeymousepatientRewardsHub.com";
            var res = api.Patients.GetPatients(patientSearch);
            Assert.IsTrue(res.Count == res.Patients.Count);
        }

        [TestMethod]
        public void Test_FindPatientsByResponsibleEmail()
        {
            PatientSearch patientSearch = new PatientSearch();
            patientSearch.responsible_email = "";
            var res = api.Patients.GetPatients(patientSearch);
            Assert.IsTrue(res.Count == res.Patients.Count);
        }

        [TestMethod]
        public void Test_Create_Patient()
        {
            var res = api.Patients.CreatePatient(new Patient()
            {
                External_id = "90" + random.Next(0,10000).ToString(),
                Firstname = "Donald",
                Lastname = "Duck",
                Email = "donaldduck@donaldduckpatientRewardsHub.com",
                Address1 = "123 A St",
                City = "Los Angels",
                State = "CA",
                Zip = "92298",
                Homephone = "7856662222",
                Birthdate = string.Format("1980-{0:D2}-{1:D2}", random.Next(1,12),  random.Next(1,30))
            });

            Assert.IsTrue(Convert.ToInt32(res.Patient.Id) > 0);
        }

        [TestMethod]
        public void Test_Create_Patient_1()
        {
            var res = api.Patients.CreatePatient(new Patient()
            {
                External_id = "232" + random.Next(0, 10000).ToString(),
                Firstname = "Tom",
                Lastname = "Thomas",
                Email = "TomThomas@patientRewardsHub.com",
                Address1 = "343 Left St",
                City = "Webster City",
                State = "IA",
                Zip = "50595",
                Homephone = "5158359338",
                Birthdate = string.Format("1980-{0:D2}-{1:D2}", random.Next(1, 12), random.Next(1, 30))
            });

            Assert.IsTrue(res.Patient.Lastname == "Thomas");
        }

        [TestMethod]
        public void Test_GetPatient_1()
        {
            var res = api.Patients.GetPatientById(11703079);
            Assert.IsFalse(res.Patient.Firstname == "Tommy");
            Assert.IsTrue(res.Patient.Email.Contains("tomthomas"));
            Assert.AreEqual(11703079, res.Patient.Id);
        }

        [TestMethod]
        public void Test_Update_Patient1_email()
        {
            var res = api.Patients.GetPatientById(11703079);
            res.Patient.Email = "tomthomasthird@patientRewardsHub.com";

            int id = res.Patient.Id;
            Assert.IsTrue(id > 0);

            var resOne = api.Patients.UpdatePatient(res.Patient);

            var resChanged = api.Patients.GetPatientById(11703079);
            Assert.IsTrue(resChanged.Patient.Email.Contains("tomthomasthird"));

        }

        [TestMethod]
        public void Test_Update_Patient1_middlename()
        {
            var res = api.Patients.UpdatePatient(new Patient()
            {
                Id = 11703079,
                Middlename = "W",
            });

            var resChanged = api.Patients.GetPatientById(11703079);
            Assert.IsTrue(res.Patient.Middlename == "W");
        }

        [TestMethod]
        public void Test_Update_Patient_City()
        {
            var res = api.Patients.CreatePatient(new Patient()
            {
                External_id = "634" + random.Next(0, 10000).ToString(),
                Firstname = "John",
                Lastname = "Doe",
                Email = "johndoetest@patientRewardsHub.com",
                Address1 = "1234 Ocean Rd",
                City = "La Jolla",
                State = "CA",
                Zip = "92137",
                Homephone = "88846515151",
                Birthdate = string.Format("1980-{0:D2}-{1:D2}", random.Next(1, 12), random.Next(1, 30))
            });

            int id = res.Patient.Id;
            Assert.IsTrue(id > 0);
            res.Patient.City = "La Costa";

            var re = api.Patients.UpdatePatient(res.Patient);
            var resChanged = api.Patients.GetPatientById(re.Patient.Id);

            Assert.IsTrue(resChanged.Patient.City == "La Costa");
        }

        [TestMethod]
        public void Test_Create_Patient_2()
        {
            var res = api.Patients.CreatePatient(new Patient()
            {
                External_id = "22C" + random.Next(0, 10000).ToString(),
                Firstname = "Test",
                Lastname = "Patient",
                Email = "tpatient@patientRewardsHub.com",
                Address1 = "111 A Ave",
                City = "La Jolla",
                State = "CA",
                Zip = "92137",
                Homephone = "88846515151",
                Birthdate = string.Format("1979-{0:D2}-{1:D2}", random.Next(1, 12), random.Next(1, 30))
            });

            Assert.IsTrue(Convert.ToInt32(res.Patient.Id) > 0);
        }

        [TestMethod]
        public void Test_Update_Patient2_email()
        {
            var res = api.Patients.GetPatientById(11703170);
            res.Patient.Email = "testpatient2@patientRewardsHub.com";
            
            var re = api.Patients.UpdatePatient(res.Patient);
            var reChanged = api.Patients.GetPatientById(res.Patient.Id);
            Assert.IsTrue(reChanged.Patient.Email.Contains("testpatient2"));
        }

        [TestMethod]
        public void Test_Deletepatient_2()
        {
            var res = api.Patients.GetPatientById(67393);
            int id = res.Patient.Id;
            var isDelete = api.Patients.DeletePatient(id);

            try
            {
                IndividualPatientResponse patientResponse1 = api.Patients.GetPatientById(id);
                Assert.IsTrue(patientResponse1.Patient == null);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("404"));
            }
        }

        [TestMethod]
        public void Test_Delete_Patient()
        {
            var res = api.Patients.CreatePatient(new Patient()
            {
                External_id = "5534" + random.Next(0, 10000).ToString(),
                Firstname = "James",
                Lastname = "Dean",
                Email = "jd@patientRewardsHub.com",
                Address1 = "1234 Gravel Rd",
                City = "Harryvill",
                State = "TL",
                Zip = "451886",
                Homephone = "88846515151",
                Birthdate = string.Format("1980-{0:D2}-{1:D2}", random.Next(1, 12), random.Next(1, 30))
            });

            int id = res.Patient.Id;
            Assert.IsTrue(id > 0);

            var isDeleted = api.Patients.DeletePatient(id);
            //Assert.IsTrue(isDeleted);

            //IndividualPatientResponse patientResponse1 = api.Patients.GetPatientById(id);
            //Assert.IsTrue(patientResponse1.Patient == null);

            try
            {
                IndividualPatientResponse patientResponse1 = api.Patients.GetPatientById(id);
                Assert.IsTrue(patientResponse1.Patient == null);
            }
            catch (Exception ex)
            { 
                Assert.IsTrue(ex.Message.Contains("404"));
            }
        }

        [TestMethod]
        public void Test_Update_Batchni_email()
        {
            var res = api.Patients.GetPatientById(11703169);
            res.Patient.Email = "batchni11703169@patientRewardsHub.com";
            res.Patient.Address1 = "444 Gravel Rd";
            res.Patient.Address2 = "#3";
            res.Patient.City = "San Diego";
            res.Patient.State = "CA";
            res.Patient.Zip = "19191";
            res.Patient.Middlename = "Z";
            res.Patient.Homephone = "5199992222";
            res.Patient.Responsible_cellphone = "6199991111";
            res.Patient.Responsible_homephone = "8589991111";

            var re = api.Patients.UpdatePatient(res.Patient);

            var reChanged = api.Patients.GetPatientById(re.Patient.Id);

            Assert.IsTrue(res.Patient.Address1.Contains("444"));
            Assert.IsNotNull(res.Patient.Middlename);
        }

        [TestMethod]
        public void Test_Patient_Batch_CreateTest()
        {
            List<Patient> patients = new List<Patient>();

            patients.Add(new Patient()
            {
                External_id = "11M" + random.Next(0, 10000).ToString(),
                Firstname = "Test",
                Lastname = "Patient",
                Email = "batch011@patientRewardsHub.com",
                Address1 = "111 Gravel Rd",
                City = "Harryvill",
                State = "TL",
                Zip = "451886",
                Homephone = "88846515151",
                Birthdate = string.Format("1980-{0:D2}-{1:D2}", random.Next(1, 12), random.Next(1, 30))
            });
            patients.Add(new Patient()
            {
                External_id = "12M" + random.Next(0, 10000).ToString(),
                Firstname = "Batchni",
                Lastname = "Patientni",
                Email = "batchni@patientRewardsHub.com",
                Address1 = "2 Gravel Rd",
                City = "Webster",
                State = "NY",
                Zip = "14580",
                Homephone = "88846515151",
                Birthdate = string.Format("1980-{0:D2}-{1:D2}", random.Next(1, 12), random.Next(1, 30)) 
            });
            patients.Add(new Patient()
            {
                External_id = "13M" + random.Next(0, 10000).ToString(),
                Firstname = "Batchsan",
                Lastname = "Patientsan",
                Email = "batchsan@patientRewardsHub.com",
                Address1 = "3 Gravel Rd",
                City = "Webster",
                State = "NY",
                Zip = "14580",
                Homephone = "88846515151",
                Birthdate = string.Format("1980-{0:D2}-{1:D2}", random.Next(1, 12), random.Next(1, 30))
            });

            var groupPatientResponse = api.Patients.CreatePatients(patients.ToArray());

            Assert.IsTrue(groupPatientResponse.Errors.Count == 0);
            foreach (Patient patient in groupPatientResponse.Patients)
            {
                Assert.IsTrue(patient.Id > 0);
            }
        }  
    }
}
