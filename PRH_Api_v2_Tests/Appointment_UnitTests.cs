using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatientRewardsHubApi_v2;
using PatientRewardsHubApi_v2.Models.Appointments;

namespace PRH_Api_v2_Tests
{
    [TestClass]
    public class Appointment_UnitTests
    {
        //private PatientRewardsHubApi api = new PatientRewardsHubApi("http://api.v200.branch.patientrewardshub.com/", "apikeyabapikeyab");
        private PatientRewardsHubApi api = new PatientRewardsHubApi("http://api.stage.patientrewardshub.com", "apikeyabapikeyab");
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
            var res = api.Appointments.GetAppointmentByExternalId("b0957697-bd6b-4cc2-9d4a-607ddd1ccbbf");
            if(res.Count > 0)
                api.Appointments.DeleteAppointment(res.Appointments[0].Id);

            if(res.Count > 0)
                Assert.IsTrue(res.Appointments[0].External_id.Contains("b0957697-bd6b-4cc2-9d4a-607ddd1ccbbf"));
        }

        [TestMethod]
        public void Test_GetAppointmentByExternalId41B2877() 
        {
            var res = api.Appointments.GetAppointmentByExternalId("41B2877");
            Assert.IsTrue(res.Appointments[0].Description.Contains("30 mins"));
            Assert.IsTrue(res.Appointments.Count==1);

        }

        [TestMethod]
        public void Test_Create_Appointment()
        {
            var res = api.Appointments.CreateAppointment(new Appointment()
                                                                                    {
                                                                                        External_id="42B" + random.Next(0,10000).ToString(),
                                                                                        Description = "Consultation for 30 mins",
                                                                                        Patient_id = "2326719",
                                                                                        StartTime = DateTime.Parse("09/22/2014 2:00 pm")
                                                                                           
                                                                                    });
            Assert.IsTrue(res.Appointment.Id > 0);
            Assert.IsTrue(res.Appointment.Description.Contains("mins"));
        }

        [TestMethod]
        public void Test_Update_Appointment()
        {
            var res = api.Appointments.GetAppointmentByExternalId("31A5167");
            if (res.Appointments.Count > 0) 
            {
                //res.Appointments[0].External_id = "31A5167";
                //res.Appointments[0].Patient_id = "2326719";
                //res.Appointments[0].StartTime = DateTime.Parse("09/22/2014 2:20 pm");
                res.Appointments[0].Description = "Consultation for 45 mins dayo";
                var resOne = api.Appointments.UpdateAppointment(res.Appointments[0]);
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
                 StartTime =DateTime.Parse("09/25/2014 3:00 pm")
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
