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
        public user user_ { get; set; }
        public activities activities_ { get; set; }
        public categories category_ { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string status { get; set; } = string.Empty;
        public bool validated { get; set; }
        public string mentor_email { get; set; } = string.Empty;
        public string activity_note { get; set; } = string.Empty;

        [JsonIgnore]
        public string user_email { get; set; } = string.Empty;
        [JsonIgnore]
        public int activities_id { get; set; }
        [JsonIgnore]
        public int category_id { get; set; }
    }
}
