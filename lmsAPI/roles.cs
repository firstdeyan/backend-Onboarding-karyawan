using System.ComponentModel.DataAnnotations;

namespace lmsAPI
{
    public class roles
    {
        [Key]
        public int id { get; set; }
        public string role_name { get; set; } = string.Empty;
        public string role_description { get; set; } = string.Empty;
        public string role_platform { get; set; } = string.Empty;
  

    }
}
