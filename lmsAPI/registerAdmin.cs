namespace lmsAPI
{
    public class registerAdmin
    {
        public string email { get; set; } = string.Empty;
        public string admin_name { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public int role_id { get; set; } = 1;
        public int jobtitle_id { get; set; } = 1;
        public string gender { get; set; } = string.Empty;
        public string birthdate { get; set; } = string.Empty;
        public string phone_number { get; set; } = string.Empty;
    }
}
