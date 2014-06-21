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
            var res = api.Appointments.GetAppointments(2, 3);
            Assert.IsTrue(res.Count == res.Appointments.Count);
        }

        [TestMethod]
        public void Test_GetAppointmentByExternalId()
        {
            var res = api.Appointments.GetAppointmentByExternalId("11A4904");
            if(res.Count > 0)
                api.Appointments.DeleteAppointment(res.Appointments[0].Id);

            if(res.Count > 0)
                Assert.IsTrue(res.Appointments[0].External_id.Contains("11A4904"));
        }

        [TestMethod]
        public void Test_GetAppointment2() 
        {
            var res = api.Appointments.GetAppointmentByExternalId("11A4904");
            Assert.IsTrue(res.Appointments.Count==0);

        }

        [TestMethod]
        public void Test_Create_Appointment()
        {
            var res = api.Appointments.CreateAppointment(new Appointment()
                                                                                    {
                                                                                        External_id="11A" + random.Next(0,10000).ToString(),
                                                                                        Description = "September 15, 2014-Test Appointment u= dayo 02",
                                                                                        Patient_id = "2326709",
                                                                                        StartTime = DateTime.Parse("08/22/2014 3:00 pm")
                                                                                           
                                                                                    });
            Assert.IsTrue(res.Appointment.Id > 0);
            Assert.IsTrue(res.Appointment.Description.Contains("-T"));
        }

        [TestMethod]
        public void Test_Update_Appointment()
        {
            var res = api.Appointments.UpdateAppointment(new Appointment()
            {
                Id = 222964,
                External_id = "77A" + random.Next(0, 10000).ToString(),
                Description = "Update Appointment Updated by u= 06/20/2014",
                Patient_id = "2326709",
                StartTime = DateTime.Parse("09/13/2014 3:00 pm")

            });

            Assert.IsTrue(res.Appointment.Description.Contains("u="));
        }

        [TestMethod]
        public void Test_Delete_Appointment()
        {
            var res = api.Appointments.CreateAppointment(new Appointment()
            {
                 Description = "Create Appointment by u=",
                 Patient_id = "2326709",
                 StartTime =DateTime.Parse("05/25/2014 3:00 pm")
            });

            int id = res.Appointment.Id;
            Assert.IsTrue(id > 0);

            var isDeleted = api.Appointments.DeleteAppointment(id);
            Assert.IsTrue(isDeleted);
            //try
            //{
            //    IndividualAppointmentResponse appointmetResponse1 = api.Appointments.GetAppointmentById(id);
            //    Assert.IsTrue(appointmetResponse1.Appointment == null);
            //}
            //catch (Exception ex)
            //{
            //    Assert.IsTrue(ex.Message.Contains("404"));
            //}

        }
    }
}
