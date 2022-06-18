using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace lmsAPI
{
    public class activities_owned
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [MaxLength(45)]
        public user user_ { get; set; }
        [MaxLength(25)]
        public activities activities_ { get; set; }
        [JsonIgnore]
        public categories category_ { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        [MaxLength(20)]
        public string status { get; set; } = string.Empty;
        [MaxLength(45)]
        public string mentor_email { get; set; } = string.Empty;
        [MaxLength(200)]
        public string activity_note { get; set; } = string.Empty;

        [JsonIgnore]
        public string user_email { get; set; } = string.Empty;
        [JsonIgnore]
        public int activities_id { get; set; }
        public int category_id { get; set; }
     
    }
}
