using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace lmsAPI
{
    public class activities_owned_form
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string user_email { get; set; } = string.Empty;
        public int activity_id { get; set; }
        public int category_id { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string status { get; set; } = string.Empty;
        public bool late { get; set; }
        public string mentor_email { get; set; } = string.Empty;
        public string activity_note { get; set; } = string.Empty;
    }
}
