using System.ComponentModel.DataAnnotations;

namespace lmsAPI
{
    public class login
    {
        [EmailAddress(ErrorMessage = "Format email salah")]
        public string email { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
    }
}
