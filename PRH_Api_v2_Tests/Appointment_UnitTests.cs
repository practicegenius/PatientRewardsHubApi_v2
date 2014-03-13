using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatientRewardsHubApi_v2;
using PatientRewardsHubApi_v2.Models.Appointments;

namespace PRH_Api_v2_Tests
{
    [TestClass]
    public class Appointment_UnitTests
    {
        private PatientRewardsHubApi api = new PatientRewardsHubApi("http://api.v200.branch.patientrewardshub.com/", "apikeyabapikeyab");
        private Random random = new Random();

        [TestMethod]
        public void Test_GetAppointment()
        {
            var res = api.Appointments.GetAppointmentById(1);

            Assert.AreEqual(1, res.Appointment.Id);
        }

        [TestMethod]
        public void Test_Create_Appointment()
        {
            var res = api.Appointments.CreateAppointment(new Appointment()
                                                                                    {
                                                                                        External_id="424A" + random.Next(0,10000).ToString(),
                                                                                        Description = "Test Appointment",
                                                                                        Patient_id = "31",
                                                                                        StartTime = DateTime.Parse("03/12/2014 3:00 pm")
                                                                                           
                                                                                    });

            Assert.IsTrue(res.Appointment.Id > 0);
        }

        [TestMethod]
        public void Test_Update_Appointment()
        {
            var res = api.Appointments.UpdateAppointment(new Appointment()
            {
                Id = 5,
                //External_id = "424A" + random.Next(0, 10000).ToString(),
                Description = "Test Appointment Updated",
                Patient_id = "31",
                StartTime = DateTime.Parse("03/12/2014 3:00 pm")

            });

            Assert.IsTrue(res.Appointment.Description.Contains("Updated"));
        }
    }
}
