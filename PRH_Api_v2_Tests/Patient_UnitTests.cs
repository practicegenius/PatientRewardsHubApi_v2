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
        //private PatientRewardsHubApi api = new PatientRewardsHubApi("http://api.v200.branch.patientrewardshub.com/", "apikeyabapikeyab");
        private PatientRewardsHubApi api = new PatientRewardsHubApi("http://api.stage.patientrewardshub.com", "apikeyabapikeyab");
        private Random random = new Random();
        
        [TestMethod]
        public void Test_GetPatient()
        {
            var res = api.Patients.GetPatients(2, 1);

            Assert.IsTrue(res.Count == res.Patients.Count);
        }

        // ************************  To do:  ************************

        [TestMethod]
        public void Test_FindPatientsByExternalId()
        {
            PatientSearch patientSearch = new PatientSearch();
            patientSearch.external_id = "2327032"; //Tom Thomas
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
        public void Test_FindPatientsByLastname_Patientni()
        {
            PatientSearch patientSearch = new PatientSearch();
            patientSearch.limit = 2;
            patientSearch.offset = 0;
            patientSearch.lastname = "Patientnini";
            var res = api.Patients.GetPatients(patientSearch);

            Assert.IsTrue(res.Count == res.Patients.Count);
            var patientniCount = res.Patients.Where(a => a.Lastname == patientSearch.lastname);
            Assert.IsTrue(patientniCount.Count() > 0);
            Assert.IsTrue(patientniCount.Count() == res.Patients.Count);
        }

        [TestMethod]
        public void Test_FindPatientsByFirstname_Batchni()
        {
            PatientSearch patientSearch = new PatientSearch();
            patientSearch.limit = 2;
            patientSearch.offset = 0;
            patientSearch.firstname = "Batchni";
            var res = api.Patients.GetPatients(patientSearch);
            var batchniCount = res.Patients.Where(a => a.Firstname == patientSearch.firstname);

            Assert.IsTrue(batchniCount.Count() == res.Patients.Count);

            Assert.IsTrue(res.Count == res.Patients.Count);      
        }

        [TestMethod]
        public void Test_FindPatientsByEmail()
        {
            PatientSearch patientSearch = new PatientSearch();
            patientSearch.email = "TomThomasPrimay@patientRewardsHub.com";
            var res = api.Patients.GetPatients(patientSearch);
            Assert.IsTrue(res.Count == res.Patients.Count);
        }

        [TestMethod]
        public void Test_FindPatientsByResponsibleEmail()
        {
            PatientSearch patientSearch = new PatientSearch();
            patientSearch.responsible_email = "TomThomasSecondary@patientRewardsHub.com";
            var res = api.Patients.GetPatients(patientSearch);
            Assert.IsTrue(res.Count == res.Patients.Count);
        }

        //[TestMethod]
        //public void Test_GetPatient_6()
        //{
        //    var res = api.Patients.GetPatientById(6);
        //    Assert.IsTrue(res.Patient.Firstname == "Cinder");
        //    Assert.IsTrue(res.Patient.Lastname == "Ella");
        //}

        //[TestMethod]
        //public void Test_GetPatient_8()
        //{
        //    var res = api.Patients.GetPatientById(8);
        //    Assert.IsTrue(res.Patient.Firstname == "Lady");
        //    Assert.IsTrue(res.Patient.Lastname == "Gaga");
        //}


        [TestMethod]
        public void Test_Create_Patient()
        {
            var res = api.Patients.CreatePatient(new Patient()
            {
                External_id = "5" + random.Next(0,10000).ToString(),
                Firstname = "Gary",
                Lastname = "Williams",
                Email = "garywilliams@garywilliamspatientRewardsHub.com",
                Address1 = "123 Main St",
                City = "San Diego",
                State = "CA",
                Zip = "92108",
                Homephone = "6192223344",
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
            var res = api.Patients.GetPatientById(2327074);
            Assert.IsFalse(res.Patient.Firstname == "Victor");
            Assert.IsTrue(res.Patient.Email.Contains("TomThomas"));
            Assert.IsTrue(res.Patient.Middlename == "Y");
            Assert.AreEqual(2327074, res.Patient.Id);
        }

        [TestMethod]
        public void Test_Update_Patient1_email()
        {
            var res = api.Patients.GetPatientById(2327074);
            res.Patient.Email = "TomThomasPrimay@patientRewardsHub.com";
            res.Patient.Responsible_email = "TomThomasSecondary@patientRewardsHub.com";
            res.Patient.Gender = "Male";

            var re = api.Patients.UpdatePatient(res.Patient);
            Assert.IsFalse(res.Patient.Email.Contains("Newer"));
            Assert.IsTrue(res.Patient.Responsible_email.Contains("Secondary"));
            Assert.IsTrue(res.Patient.Gender != "Female");
        }

        [TestMethod]
        public void Test_Update_Patient1_middlename()
        {
            var res = api.Patients.UpdatePatient(new Patient()
            {
                Id = 2327074,
                Middlename = "Y",
            });
            Assert.IsFalse(res.Patient.Middlename == "W");
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
            var res = api.Patients.GetPatientById(2327083);
            res.Patient.Email = "testpatient@patientRewardsHub.com";
            
            var re = api.Patients.UpdatePatient(res.Patient);
            Assert.IsTrue(res.Patient.Email.Contains("testpatient"));
        }

        [TestMethod]
        public void Test_GetPatient_2327088()
        {

            var res = api.Patients.GetPatientById(2327088);
            Assert.IsTrue(res.Patient.Address1 == "Batch1 Ave.");
            Assert.IsTrue(res.Patient.Address2 == "#3");
            Assert.IsTrue(res.Patient.City == "San Diego");
            Assert.IsTrue(res.Patient.State == "CA");
            Assert.IsTrue(res.Patient.Zip == "19191");
            Assert.IsTrue(res.Patient.Lastname == "Patientnini"); 
            Assert.IsTrue(res.Patient.Middlename == "Z");
            Assert.IsTrue(res.Patient.Homephone == "5199992222");
            Assert.IsTrue(res.Patient.Responsible_cellphone == "6199991111");
            Assert.IsTrue(res.Patient.Responsible_homephone == "8589991111");
            Assert.IsTrue(res.Patient.Email == "batchni2327088@patientRewardsHub.com");

            //Assert.IsTrue(res.Patient.Email == "batchni@patientRewardsHub.com");

            //var res = api.Patients.GetPatientById(2327085);
            //Assert.IsTrue(res.Patient.Firstname == "Batchni");

            //Assert.IsFalse(res.Patient.Middlename == "Y");
            //Assert.AreEqual(2327085, res.Patient.Id);
        }

        [TestMethod]
        public void Test_Deletepatient_2()
        {
            var res = api.Patients.GetPatientById(2327109);
            int id = res.Patient.Id;
            var isDelete = api.Patients.DeletePatient(id);

            //Assert.IsTrue(isDelete);

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

       //[TestMethod]
       //public void Test_Create_Patient_3()
       //{
       //    var res = api.Patients.CreatePatient(new Patient()
       //    {
       //        External_id = "53" + random.Next(0, 10000).ToString(),
       //        Firstname = "Zoey",
       //        Lastname = "Bunny",
       //        Email = "Zoey@patientRewardsHub.com",
       //        Address1 = "1100 Zoey Ave",
       //        City = "San Diego",
       //        State = "789",
       //        Zip = "abcdefg",
       //        Homephone = "88846515151",
       //        Birthdate = string.Format("1980-{0:D2}-{1:D2}", random.Next(1, 12), random.Next(1, 30))
       //    });

       //    Assert.IsTrue(Convert.ToInt32(res.Patient.Id) > 0); 
       //}

        //[TestMethod]
        //public void Test_Update_Patient() 
        //{
        //    var res = api.Patients.UpdatePatient(new Patient()
        //    {
        //        Id=130,
        //        //External_id = "22",
        //        Firstname = "Minny",
        //        Lastname = "Mouse",
        //        Email = "minney@patientRewardsHub.com",
        //        Address1 = "444 Main St",
        //        City = "San Diego",
        //        State = "CA",
        //        Zip = "US",
        //        Homephone = "6192223344",
        //        Birthdate = "1980-05-05"
        //    });
        //    Assert.IsTrue(res.Patient.Firstname == "Minny");
        //}


        [TestMethod]
        public void Test_Delete_Patient()
        {
            var res = api.Patients.CreatePatient(new Patient()
            {
                External_id = "1234" + random.Next(0, 10000).ToString(),
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
            //IndividualPatientResponse patientResponse = api.Patients.GetPatientById(id);
            //Assert.IsTrue(patientResponse.Patient != null);
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
            var res = api.Patients.GetPatientById(2327088);
            res.Patient.Email = "batchni2327088@patientRewardsHub.com";
            res.Patient.Address1 = "Batch1 Ave.";
            res.Patient.Address2 = "#3";
            res.Patient.City = "San Diego";
            res.Patient.State = "CA";
            res.Patient.Zip = "19191";
            res.Patient.Lastname = "Patientnini";
            res.Patient.Middlename = "Z";
            res.Patient.Homephone = "5199992222";
            res.Patient.Responsible_cellphone = "6199991111";
            res.Patient.Responsible_homephone = "8589991111";
         
            //res.Patient.Responsible_email = "batchniTest@patientRewardsHub.com";
            //res.Patient.Gender = "Male";

            var re = api.Patients.UpdatePatient(res.Patient);
            //Assert.IsTrue(res.Patient.Email == "batchni@patientRewardsHub.com");
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
