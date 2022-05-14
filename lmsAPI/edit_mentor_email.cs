using System.ComponentModel.DataAnnotations;
namespace lmsAPI
{
    public class edit_mentor_email
    {
        public int id { get; set; }
        public string user_email { get; set; } = string.Empty;
        [MaxLength(200)]
        public string mentor_email { get; set; } = string.Empty;
    }
}
