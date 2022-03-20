using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace lmsAPI
{
    public class admin
    {

        [Key]
        public string email { get; set; } = string.Empty;
        public string admin_name { get; set; } = string.Empty;
        public byte[]? passwordHash { get; set; }
        public byte[]? passwordSalt { get; set; }
        [JsonIgnore]
        public roles? role_ { get; set; }

    }
}
