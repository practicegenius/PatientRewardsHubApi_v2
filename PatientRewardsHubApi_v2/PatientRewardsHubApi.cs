using System;
using System.Text;
using PatientRewardsHubApi_v2.Requests; 

#if Net35 
using System.Web;
using System.Security.Cryptography;
#endif

namespace PatientRewardsHubApi_v2
{
    public class PatientRewardsHubApi
    {
        public Patients Patients { get; set; }
        public Appointments Appointments { get; set; }
        public Authentications Authentications { get; set; }

        public string PatientRewardsHubUrl { get; set; }

        /// <summary>
        /// Constructor that takes 3 params.
        /// </summary>
        /// <param name="yourPatientRewardsHubUrl">Will be formated to "https://yoursite.patientrewardshub.com/api/v2"</param>
        /// <param name="apiToken"></param>
        public PatientRewardsHubApi(string yourPatientRewardsHubUrl, string apiToken = "")
        {
            var formattedUrl = GetFormattedPatientRewardsHubUrl(yourPatientRewardsHubUrl).AbsoluteUri;

            Patients = new Patients(formattedUrl, apiToken);
            Appointments = new Appointments(formattedUrl, apiToken);
            

            PatientRewardsHubUrl = formattedUrl;
        }

        public PatientRewardsHubApi(string yourPatientRewardsHubUrl, string user, string password = "", string application_token = "")
        {
            var formattedUrl = GetFormattedPatientRewardsHubUrl(yourPatientRewardsHubUrl).AbsoluteUri;

            Authentications = new Authentications(formattedUrl, user, password, application_token);

            PatientRewardsHubUrl = formattedUrl;
        }

        Uri GetFormattedPatientRewardsHubUrl(string yourPatientRewardsHubUrl)
        {                        
            yourPatientRewardsHubUrl = yourPatientRewardsHubUrl.ToLower();
            if (!yourPatientRewardsHubUrl.Contains("stage"))
            {
                //#if(!DEBUG) //mws:  turn off https for debugging
                //Make sure the Authority is https://
                if (yourPatientRewardsHubUrl.StartsWith("http://"))
                    yourPatientRewardsHubUrl = yourPatientRewardsHubUrl.Replace("http://", "https://");            

            
                if (!yourPatientRewardsHubUrl.StartsWith("https://"))
                    yourPatientRewardsHubUrl = "https://" + yourPatientRewardsHubUrl;
                        
                //if (!yourPatientRewardsHubUrl.EndsWith("/api"))
                //{
                //    //ensure that url ends with ".patientrewardshub.com/api/v2"
                //    yourPatientRewardsHubUrl = yourPatientRewardsHubUrl.Split(new[] { ".patientrewardshub.com" }, StringSplitOptions.RemoveEmptyEntries)[0] + ".patientrewardshub.com";               
                //}
                //#endif
            }
            return new Uri(yourPatientRewardsHubUrl);
        }

#if Net35 
        public string GetLoginUrl(string name, string email, string authenticationToken, string returnToUrl = "")
        {
            string url = string.Format("{0}/access/remoteauth/", PatientRewardsHubUrl);

            string timestamp = GetUnixEpoch(DateTime.Now).ToString();


            string message = string.Format("{0}|{1}|||||{2}|{3}", name, email, authenticationToken, timestamp);
            //string message = name + email + token + timestamp;
            string hash = Md5(message);

            string result = url + "?name=" + HttpUtility.UrlEncode(name) +
                            "&email=" + HttpUtility.UrlEncode(email) +
                            "&timestamp=" + timestamp +
                            "&hash=" + hash;

            if (returnToUrl.Length > 0)
                result += "&return_to=" + returnToUrl;

            return result;
        }

        private double GetUnixEpoch(DateTime dateTime)
        {
            var unixTime = dateTime.ToUniversalTime() -
                           new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return unixTime.TotalSeconds;
        }


        public string Md5(string strChange)
        {
            //Change the syllable into UTF8 code
            byte[] pass = Encoding.UTF8.GetBytes(strChange);

            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(pass);
            string strPassword = ByteArrayToHexString(md5.Hash);
            return strPassword;
        }

        public string ByteArrayToHexString(byte[] Bytes)
        {
            // important bit, you have to change the byte array to hex string or PatientRewardsHub will reject
            StringBuilder Result;
            string HexAlphabet = "0123456789abcdef";

            Result = new StringBuilder();

            foreach (byte B in Bytes)
            {
                Result.Append(HexAlphabet[(int)(B >> 4)]);
                Result.Append(HexAlphabet[(int)(B & 0xF)]);
            }

            return Result.ToString();
        }
#endif

    }
}
