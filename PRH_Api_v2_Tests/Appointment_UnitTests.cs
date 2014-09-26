using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatientRewardsHubApi_v2;
using PatientRewardsHubApi_v2.Models.Appointments;

namespace PRH_Api_v2_Tests
{
    [TestClass]
    public class Appointment_UnitTests
    {
        private PatientRewardsHubApi api = new PatientRewardsHubApi("http://api.patientrewardshub.com/", "n9UfoneWE3qA2GHA");

        //This is for the stage server.
        //private PatientRewardsHubApi api = new PatientRewardsHubApi("http://api.stage.patientrewardshub.com", "F4FMlXEAuVEx1Hby"); //"apikeyabapikeyab"
        
        private Random random = new Random();

        [TestMethod]
        public void Test_GetAppointment()
        {
            var res = api.Appointments.GetAppointments(6, 3);
            Assert.IsTrue(res.Count == res.Appointments.Count);
        }

        [TestMethod]
        public void Test_GetAppointmentByExternalId()
        {
            var res = api.Appointments.GetAppointmentByExternalId("115B5878");
            //if(res.Count > 0)
            //    api.Appointments.DeleteAppointment(res.Appointments[0].Id);

            if(res.Count > 0)
                Assert.IsTrue(res.Appointments[0].Description.Contains("Wire"));
        }

        [TestMethod]
        public void Test_Create_Appointment()
        {
            var res = api.Appointments.CreateAppointment(new Appointment()
                                                                                    {
                                                                                        External_id="115B" + random.Next(0,10000).ToString(),
                                                                                        Description = "New patient",
                                                                                        Patient_id = "67219",
                                                                                        StartTime = DateTime.Parse("11/11/2014 12:00 pm")
                                                                                           
                                                                                    });
            Assert.IsTrue(res.Appointment.Id > 0);
            Assert.IsTrue(res.Appointment.Description.Contains("New"));
        }

        [TestMethod]
        public void Test_Update_Appointment()
        {
            var res = api.Appointments.GetAppointmentByExternalId("115B5878");
            if (res.Appointments.Count > 0) 
            {
                res.Appointments[0].Description = "Wire Change for 45 mins";
                var resOne = api.Appointments.UpdateAppointment(res.Appointments[0]);
                var resChanged = api.Appointments.GetAppointmentByExternalId(resOne.Appointment.External_id);

                Assert.IsTrue(resOne.Appointment.Description.Contains("for 45 mins"));
            }
            else
            {
                Assert.Fail("couldn't find appointment");
            }
        }

        [TestMethod]
        public void Test_Delete_Appointment()
        {
            var res = api.Appointments.CreateAppointment(new Appointment()
            {
                 Description = "Create Appointment by u=",
                 Patient_id = "2326755",
                 StartTime =DateTime.Parse("10/25/2014 3:00 pm")
            });

            int id = res.Appointment.Id;
            Assert.IsTrue(id > 0);

            var isDeleted = api.Appointments.DeleteAppointment(id);

            try
            {
                IndividualAppointmentResponse appointmetResponse1 = api.Appointments.GetAppointmentById(id);
                Assert.IsTrue(appointmetResponse1.Appointment == null);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("404"));
            }

        }
    }
}
