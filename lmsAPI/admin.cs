using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace lmsAPI
{
    public class admin
    {
        
        [Key]
        public string email { get; set; } = string.Empty;
        public string admin_name { get; set; } = string.Empty;
        [JsonIgnore]
        public byte[]? passwordHash { get; set; }
        [JsonIgnore]
        public byte[]? passwordSalt { get; set; }
        public roles role_ { get; set; }
        public job_titles jobtitle_ { get; set; }
        public string gender { get; set; } = string.Empty;
        public string birthdate { get; set; } = string.Empty;
        public string phone_number { get; set; } = string.Empty;


    }
}
