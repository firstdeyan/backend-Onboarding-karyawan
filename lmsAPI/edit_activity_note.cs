using System.ComponentModel.DataAnnotations;
namespace lmsAPI
{
    public class edit_activity_note
    {
        public int id { get; set; }
        public string user_email { get; set; } = string.Empty;
        [MaxLength(200)]
        public string activity_note { get; set; } = string.Empty;
    }
}
