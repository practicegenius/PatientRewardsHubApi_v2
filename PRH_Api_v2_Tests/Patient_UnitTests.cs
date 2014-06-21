using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatientRewardsHubApi_v2;
using PatientRewardsHubApi_v2.Models.Patients;

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


        [TestMethod]
        public void Test_GetPatient_2326709()
        {
            var res = api.Patients.GetPatientById(2326709);
            Assert.IsTrue(res.Patient.Firstname == "Mini");

            Assert.IsTrue(res.Patient.Middlename == "");
            Assert.AreEqual(2326709, res.Patient.Id);
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

        //[TestMethod]
        //public void Test_GetPatient_40()
        //{
        //    var res = api.Patients.GetPatientById(40);
        //    Assert.IsTrue(res.Patient.Middlename == "P");
        //    Assert.IsTrue(res.Patient.Address2 == "#101");

        //    Assert.AreEqual(40, res.Patient.Id);
        //}

        //[TestMethod]
        //public void Test_GetPatient_130()
        //{
        //    var res = api.Patients.GetPatientById(130);
        //    Assert.IsTrue(res.Patient.Firstname == "Batch130");
        //    Assert.IsFalse(res.Patient.Middlename == "P");

        //    Assert.AreEqual(130, res.Patient.Id);
        //}


        [TestMethod]
        public void Test_Create_Patient()
        {
            var res = api.Patients.CreatePatient(new Patient()
            {
                External_id = "20" + random.Next(0,10000).ToString(),
                Firstname = "Mini",
                Lastname = "Mouse",
                Email = "Henrietta@patientRewardsHub.com",
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
                External_id = "230" + random.Next(0, 10000).ToString(),
                Firstname = "Bill",
                Lastname = "House",
                Email = "Bill@patientRewardsHub.com",
                Address1 = "343 Left St",
                City = "Webster City",
                State = "IA",
                Zip = "50595",
                Homephone = "5158359338",
                Birthdate = string.Format("1980-{0:D2}-{1:D2}", random.Next(1, 12), random.Next(1, 30))
            });
        }

     //  [TestMethod]
     //   public void Test_Create_Patient_2()
     //   {          
     //       var res = api.Patients.CreatePatient(new Patient()
     //       {
     //           External_id = "23" + random.Next(0,10000).ToString(),
     //           Firstname = "Harry",
     //           Lastname = "Face",
     //           Email = "Harry@patientRewardsHub.com",
     //           Address1 = "3782 Gravel Rd",
     //           City = "Harryvill",
     //           State = "TL",
     //           Zip = "451886",
     //           Homephone = "88846515151",
     //           Birthdate = string.Format("1980-{0:D2}-{1:D2}", random.Next(1,12),  random.Next(1,30))
     //       });

     //       Assert.IsTrue(Convert.ToInt32(res.Patient.Id) > 0);
     //}

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
        public void Test_Update_Patient2326709()
        {
            var res = api.Patients.UpdatePatient(new Patient()
            {
                Id = 2326709,
                Middlename = "",
            });
            Assert.IsFalse(res.Patient.Middlename == "W"); 
        }

        //[TestMethod]
        //public void Test_Update_Patient21()
        //{
        //    var res = api.Patients.UpdatePatient(new Patient()
        //    {
        //        Id = 21,
        //        Address2 = "#222"
        //    });
        //    Assert.IsTrue(res.Patient.Address2 == "#222");
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
            Assert.IsTrue(isDeleted);

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
        public void Test_Patient_Batch_CreateTest()
        {
            List<Patient> patients = new List<Patient>();

            patients.Add(new Patient()
            {
                External_id = "111" + random.Next(0, 10000).ToString(),
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
                External_id = "012" + random.Next(0, 10000).ToString(),
                Firstname = "Batch012",
                Lastname = "Patient",
                Email = "batch012@patientRewardsHub.com",
                Address1 = "012 Gravel Rd",
                City = "Webster",
                State = "NY",
                Zip = "14580",
                Homephone = "88846515151",
                Birthdate = string.Format("1980-{0:D2}-{1:D2}", random.Next(1, 12), random.Next(1, 30))
            });
            patients.Add(new Patient()
            {
                External_id = "013" + random.Next(0, 10000).ToString(),
                Firstname = "Batch013",
                Lastname = "Patient",
                Email = "batch013@patientRewardsHub.com",
                Address1 = "013Gravel Rd",
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
