using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace lmsAPI
{
    public class activities
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string activity_name { get; set; } = string.Empty;
        public string activity_description { get; set; } = string.Empty;
        public categories category_ { get; set; }

        [JsonIgnore]
        public int category_id { get; set; }
    }
}
