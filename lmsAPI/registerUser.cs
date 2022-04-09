using System.ComponentModel.DataAnnotations;

namespace lmsAPI
{
    public class registerUser
    {
        public string email { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public int role_id { get; set; } = 1;
        public int jobtitle_id { get; set; } = 1;
        public string gender { get; set; } = string.Empty;
        public string birthdate { get; set; } = string.Empty;
        public string phone_number { get; set; } = string.Empty;

    }
}
