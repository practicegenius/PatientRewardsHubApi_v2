# Getting Started

### Authentication
For each practice you must create and store a unique authentication_token for that practice. (Please note that this is different than your application token that is unique to your organization).  

````cpp
string baseUrl          = "https://api.patientrewardshub.com";
string username         = "doctor"; // Dynamically set through your interface
string password         = "1234ab"; // Dynamically set through your interface
string applicationToken = "ABCDEFGHIJKLMNOPQRSTUVWXYZ012345"; // Your unique application token provided by PracticeGenius

PatientRewardsHubApi api = new PatientRewardsHubApi(baseUrl, username, password, applicationToken);
string authentication_token = api.Authentications.CreateAuthentication();
// Store the authentication_token for this practice
````

### Making Calls
You will now make all subsequent calls utilizing the authentication_token stored from the last step:

````cpp
string formattedUrl = GetFormattedPatientRewardsHubUrl(yourPatientRewardsHubUrl).AbsoluteUri;
// the string authenticationToken being the token saved previously
Patients patients = new Patients(formattedUrl, authenticationToken);
````
