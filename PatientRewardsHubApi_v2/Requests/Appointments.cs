#if ASYNC
using System.Threading.Tasks;
#endif
using PatientRewardsHubApi_v2.Models.Patients;
using PatientRewardsHubApi_v2.Models.Appointments;
using ZendeskApi_v2.Models.Appointments;

namespace PatientRewardsHubApi_v2.Requests
{
    public class Appointments : Core
    {
        public Appointments(string yourPatientRewardsHubUrl, string apiToken)
            : base(yourPatientRewardsHubUrl, apiToken)
        {
        }

        public Appointments(string yourPatientRewardsHubUrl, string user, string password)
            : base(yourPatientRewardsHubUrl, user, password)
        {
        }
#if SYNC
        //// already here... did i create this??
        //public GroupAppointmentResponse GetAppointments()
        //{
        //    GroupAppointmentResponse groupAppointmentResponse = new GroupAppointmentResponse();
        //    Appointments appointments = GenericGet<Appointments>("appointment");

        //    return GenericGet<GroupAppointmentResponse>("appointments.json");
        //}

        public GroupAppointmentResponse GetAppointments(int limit, int offset)
        {
            GroupAppointmentResponse groupAppointmentResponse = GenericGet<GroupAppointmentResponse>(string.Format("appointment?limit={0}&offset={1}", limit, offset));
            return groupAppointmentResponse;
        }

        public IndividualAppointmentResponse GetAppointmentById(long appointmentId)
        {
            //return GenericGet<IndividualAppointmentResponse>(string.Format("appointments/{0}.json", appointmentId));

            IndividualAppointmentResponse individualAppointmentResponse = new IndividualAppointmentResponse();
            Appointment appointment = GenericGet<Appointment>(string.Format("appointment/{0}", appointmentId));
            individualAppointmentResponse.Appointment = appointment;
            return individualAppointmentResponse;  
        }

        public GroupAppointmentResponse GetAppointmentByExternalId(string externalId)
        {
            //return GenericGet<IndividualAppointmentResponse>(string.Format("appointments/{0}.json", appointmentId));

            //IndividualAppointmentResponse individualAppointmentResponse = new IndividualAppointmentResponse();
            GroupAppointmentResponse appointment = GenericGet<GroupAppointmentResponse>(string.Format("appointment?external_id={0}&limit=1&offset=0", externalId));
            //individualAppointmentResponse.Appointment = appointment;
            //return individualAppointmentResponse;
            return appointment;
        }


        public IndividualAppointmentResponse CreateAppointment(Appointment appointment)
        {
            var body = appointment; //new { appointment };
            IndividualAppointmentResponse individualAppointmentResponse = new IndividualAppointmentResponse();
            Appointment retAppointment = GenericPost<Appointment>("appointment", body);
            if (retAppointment != null)
            {
                appointment.Id = retAppointment.Id;
                individualAppointmentResponse.Appointment = appointment;
            }
            return individualAppointmentResponse;

            //return GenericPost<IndividualAppointmentResponse>("appointments.json", body);
        }

        public IndividualAppointmentResponse UpdateAppointment(Appointment appointment)
        {
            var body = appointment ;
            IndividualAppointmentResponse individualAppointmentResponse = new IndividualAppointmentResponse();
            Appointment retAppointment = GenericPut<Appointment>(string.Format("appointment/{0}", appointment.Id), body);
            individualAppointmentResponse.Appointment = appointment;
            return individualAppointmentResponse;
        }

        public bool DeleteAppointment(long id)
        {
            return GenericDelete(string.Format("appointment/{0}", id));
        }


#endif
#if ASYNC
        public async Task<GroupAppointmentResponse> GetAppointmentsAsync()
        {
            return await GenericGetAsync<GroupAppointmentResponse>("appointments.json");
        }

        public async Task<IndividualAppointmentResponse> GetAppointmentByIdAsync(long appointmentId)
        {
            return await GenericGetAsync<IndividualAppointmentResponse>(string.Format("appointments/{0}.json", appointmentId));
        }

        public async Task<IndividualAppointmentResponse> CreateAppointmentAsync(Appointment appointment)
        {
            var body = new { appointment };
            return await GenericPostAsync<IndividualAppointmentResponse>("appointments.json", body);
        }

        public async Task<IndividualAppointmentResponse> UpdateAppointmentAsync(Appointment appointment)
        {
            var body = new { appointment };
            return await GenericPutAsync<IndividualAppointmentResponse>(string.Format("appointments/{0}.json", appointment.Id), body);
        }

        public async Task<bool> DeleteAppointmentAsync(long id)
        {
            return await GenericDeleteAsync(string.Format("appointments/{0}.json", id));
        }

#endif
    }
}