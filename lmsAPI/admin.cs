using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace lmsAPI
{
    public class admin
    {
        [Key]
        [MaxLength(45)]
        public string email { get; set; } = string.Empty;
        [MaxLength(50)]
        public string admin_name { get; set; } = string.Empty;
        [JsonIgnore]
        public byte[]? passwordHash { get; set; }
        [JsonIgnore]
        public byte[]? passwordSalt { get; set; }
        public roles role_ { get; set; }
        public job_titles jobtitle_ { get; set; }
        [MaxLength(10)]
        public string gender { get; set; } = string.Empty;
        public string birthdate { get; set; } = string.Empty;
        [MaxLength(15)]
        public string phone_number { get; set; } = string.Empty;
        [MaxLength(200)]
        [JsonIgnore]
        public string photo { get; set; } = string.Empty;
        public bool active { get; set; }
        [JsonIgnore]
        public string? token { get; set; } = string.Empty;

        [JsonIgnore]
        public int role_id { get; set; }
        [JsonIgnore]
        public int jobtitle_id { get; set; }
    }
}
