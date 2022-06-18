using System.ComponentModel.DataAnnotations;

namespace lmsAPI
{
    public class registerUser
    {
        [MaxLength(45)]
        public string email { get; set; } = string.Empty;
        [MaxLength(50)]
        public string name { get; set; } = string.Empty;
        [MaxLength(180)]
        public string password { get; set; } = string.Empty;
        public int role_id { get; set; } = 1;
        public int jobtitle_id { get; set; } = 1;
        [MaxLength(10)]
        public string gender { get; set; } = string.Empty;
        public string birthdate { get; set; } = string.Empty;
        [MaxLength(15)]
        public string phone_number { get; set; } = string.Empty;
        [MaxLength(200)]
        public IFormFile[]? files { get; set; }

    }
}
