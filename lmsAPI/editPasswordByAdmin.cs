using System.ComponentModel.DataAnnotations;
namespace lmsAPI
{
    public class editPasswordByAdmin
    {
        public string email { get; set; } = string.Empty;
        [MaxLength(180)]
        public string new_password { get; set; } = string.Empty;
    }
}
