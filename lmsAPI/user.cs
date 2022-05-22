using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace lmsAPI
{
    public class user
    {

        [Key]
        [MaxLength(200)]
        public string email { get; set; } = string.Empty;
        [MaxLength(100)]
        public string name { get; set; } = string.Empty;
        [JsonIgnore]
        public byte[]? passwordHash { get; set; }
        [JsonIgnore]
        public byte[]? passwordSalt { get; set; }
        public roles role_ { get; set; }
        public job_titles jobtitle_ { get; set; }
        [MaxLength(25)]
        public string gender { get; set; } = string.Empty;
        public string birthdate { get; set; } = string.Empty;
        [MaxLength(15)]
        public string phone_number { get; set; } = string.Empty;
        [MaxLength(200)]
        public string photo { get; set; } = string.Empty;
        public double progress { get; set; }
        public int finishedActivities { get; set; }
       
        public int assignedActivities { get; set; }
        public bool active { get; set; }

        [JsonIgnore]
        public int role_id { get; set; }
 
    }
}
