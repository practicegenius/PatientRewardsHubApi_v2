#if ASYNC
using System.Threading.Tasks;
#endif
using PatientRewardsHubApi_v2.Models.Patients;
using PatientRewardsHubApi_v2.Models.Appointments;
using ZendeskApi_v2.Models.Patients;


namespace PatientRewardsHubApi_v2.Requests
{
    public class Patients : Core
    {
        public Patients(string yourPatientRewardsHubUrl, string apiToken)
            : base(yourPatientRewardsHubUrl, apiToken)
        {
        }

        public Patients(string yourPatientRewardsHubUrl, string user, string password)
            : base(yourPatientRewardsHubUrl, user, password)
        {
        }
#if SYNC
        public GroupPatientResponse GetPatients()
        {
            return GenericGet<GroupPatientResponse>("patients.json");
        }

        public IndividualPatientResponse GetPatientById(long patientId)
        {
            //return GenericGet<IndividualPatientResponse>(string.Format("patients/{0}.json", patientId));
            //return GenericGet<IndividualPatientResponse>(string.Format("patient/{0}", patientId));

            IndividualPatientResponse individualPatientResponse = new IndividualPatientResponse();           
            Patient patient = GenericGet<Patient>(string.Format("patient/{0}", patientId));
            individualPatientResponse.Patient = patient;
            individualPatientResponse.Patient.Id = (int)patientId;
            return individualPatientResponse;            
        }


        public IndividualPatientResponse CreatePatient(Patient patient)
        {
            var body = patient; // new { patient };
            IndividualPatientResponse individualPatientResponse = new IndividualPatientResponse();
            Patient retpatient = GenericPost<Patient>("patient", body);
            if (retpatient != null)
            {
                patient.Id = retpatient.Id;
                individualPatientResponse.Patient = patient;
            }
            
            return individualPatientResponse;

            //return GenericPost<IndividualPatientResponse>("patient", body);
        }

        public IndividualPatientResponse UpdatePatient(Patient patient)
        {
            var body = patient ;
            IndividualPatientResponse individualPatientResponse = new IndividualPatientResponse();
            Patient retPatient =  GenericPut<Patient>(string.Format("patient/{0}", patient.Id), body);

            individualPatientResponse.Patient = patient;
            
            return individualPatientResponse;


            //return GenericPut<Patient>(string.Format("patient/{0}", patient.Id), body);
        }

        public bool DeletePatient(long id)
        {
            return GenericDelete(string.Format("patient/{0}", id));
        }

#endif
#if ASYNC
        public async Task<GroupPatientResponse> GetPatientsAsync()
        {
            return await GenericGetAsync<GroupPatientResponse>("patients.json");
        }

        public async Task<IndividualPatientResponse> GetPatientByIdAsync(long patientId)
        {
            return await GenericGetAsync<IndividualPatientResponse>(string.Format("patients/{0}.json", patientId));
        }

        public async Task<IndividualPatientResponse> CreatePatientAsync(Patient patient)
        {
            var body = new { patient };
            return await GenericPostAsync<IndividualPatientResponse>("patients.json", body);
        }

        public async Task<IndividualPatientResponse> UpdatePatientAsync(Patient patient)
        {
            var body = new { patient };
            return await GenericPutAsync<IndividualPatientResponse>(string.Format("patients/{0}.json", patient.Id), body);
        }

        public async Task<bool> DeletePatientAsync(long id)
        {
            return await GenericDeleteAsync(string.Format("patients/{0}.json", id));
        }

#endif
    }
}