using System.ComponentModel.DataAnnotations;
namespace lmsAPI
{
    public class edit_status
    {
        public int id { get; set; }
        public string user_email { get; set; } = string.Empty;
        [MaxLength(50)]
        public string status { get; set; } = string.Empty;
    }
}
