#if ASYNC
using System.Threading.Tasks;
#endif
using PatientRewardsHubApi_v2.Models.Patients;
using PatientRewardsHubApi_v2.Models.Appointments;
using ZendeskApi_v2.Models.Patients;
using System.Collections.Generic;
using System.Linq;


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
        public GroupPatientResponse GetPatients(int limit, int offset)
        {
            GroupPatientResponse groupPatientResponse = GenericGet<GroupPatientResponse>(string.Format("patient?limit={0}&offset={1}", limit, offset));  // ??
            return groupPatientResponse;
        }

        public GroupPatientResponse GetPatients(PatientSearch patientSearch)
        {
            GroupPatientResponse groupPatientResponse = GenericGet<GroupPatientResponse>(patientSearch.ToString());  
            return groupPatientResponse;
        }

        public IndividualPatientResponse GetPatientById(long patientId)
        {
            IndividualPatientResponse individualPatientResponse = new IndividualPatientResponse();           
            Patient patient = GenericGet<Patient>(string.Format("patient/{0}", patientId)); // GenericGet<Patient>("patient/121");
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
        }

        public IndividualPatientResponse UpdatePatient(Patient patient)
        {
            var body = patient;
            IndividualPatientResponse individualPatientResponse = new IndividualPatientResponse();
            Patient retPatient =  GenericPut<Patient>(string.Format("patient/{0}", patient.Id), body);

            individualPatientResponse.Patient = patient;
            
            return individualPatientResponse;

        }

        public bool DeletePatient(long id)
        {
            return GenericDelete(string.Format("patient/{0}", id));
        }

        public GroupPatientResponse CreatePatients(Patient[] patients)
        {
            var body = patients; // new { patient };
            GroupPatientResponse groupPatientResponse = new GroupPatientResponse();
            var retpatients = GenericPost<GroupPatientResponse>("patient/batch", body);

            //mws:  retPatients will be a "GroupPatientResponse" have "Patients" and "Errors"

            //mws:  merge "Patient[] patients" with "retpatients"

            foreach (Patient patient in patients)
            {
                // org
                var newPatientId = (from p in retpatients.Patients
                                    where p.External_id == patient.External_id
                                    select p.Id).FirstOrDefault();
                // This is Org.
                if (newPatientId > 0)
                {
                    patient.Id = newPatientId;
                }
            }

            groupPatientResponse.Patients = patients.ToList();
            groupPatientResponse.Errors = retpatients.Errors;

            return groupPatientResponse;
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