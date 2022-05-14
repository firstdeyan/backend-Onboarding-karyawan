using System.ComponentModel.DataAnnotations;
namespace lmsAPI
{
    public class editUser
    {
        [Key]
        public string email { get; set; } = string.Empty;
        [MaxLength(100)]
        public string name { get; set; } = string.Empty;
        public int role_id { get; set; } = 1;
        public int jobtitle_id { get; set; } = 1;
        [MaxLength(25)]
        public string gender { get; set; } = string.Empty;
        public string birthdate { get; set; } = string.Empty;
        [MaxLength(15)]
        public string phone_number { get; set; } = string.Empty;

    }
}
