using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatientRewardsHubApi_v2.Requests
{
    public class PatientSearch
    {
        public PatientSearch()
        {

        }
        public PatientSearch(int limit, int offset, string external_id, string username, string firstname, string lastname, string email, string responsible_email)
        {
            this.limit = limit;
            this.offset = offset;
            this.external_id = external_id;
            this.username = username;
            this.firstname = firstname;
            this.lastname = lastname;
            this.email = email;
            this.responsible_email = responsible_email;
        }
        /// <summary>
        /// The max number of records to return.  Defaults to 25
        /// </summary>
        public int limit{get;set;} //Return only this number of patients. 

        /// <summary>
        /// The starting record to get back from the results.  Defaults to 0
        /// </summary>
        public int offset{get;set;} //Return patients after a count of this many. 

        /// <summary>
        /// Optional: Find patients by external id. 
        /// </summary>
        public string external_id{get;set;}

        /// <summary>
        /// Optional: Find patients by username. 
        /// </summary>
        public string username{get;set;}

        /// <summary>
        /// Optional: Find patients by first name.
        /// </summary>
        public string firstname{get;set;} 

        /// <summary>
        /// Optional: Find patients by last name.
        /// </summary>
        public string lastname{get;set;} 

        /// <summary>
        /// Optional: Find patients by E-Mail.
        /// </summary>
        public string email{get;set;} 

        /// <summary>
        /// Optional: Find patients by responsible E-mail.
        /// </summary>
        public string responsible_email { get; set; }

        public override string ToString()
        {
            if (this.limit < 1) { this.limit = 25;}
            if (this.offset < 0) { this.offset = 0; }

            string paramString = string.Format("patient?limit={0}&offset={1}", this.limit, this.offset);

            if(!string.IsNullOrWhiteSpace(this.external_id))
            {
                paramString += string.Format("&external_id={0}", this.external_id);
            }
            if (!string.IsNullOrWhiteSpace(this.username))
            {
                paramString += string.Format("&username={0}", this.username);
            }
            if (!string.IsNullOrWhiteSpace(this.firstname))
            {
                paramString += string.Format("&firstname={0}", this.firstname);
            }
            if (!string.IsNullOrWhiteSpace(this.lastname))
            {
                paramString += string.Format("&lastname={0}", this.lastname);
            }
            if (!string.IsNullOrWhiteSpace(this.email))
            {
                paramString += string.Format("&email={0}", this.email);
            }
            if (!string.IsNullOrWhiteSpace(this.responsible_email))
            {
                paramString += string.Format("&responsible_email={0}", this.responsible_email);
            }
            return paramString;
        }

    }
}
