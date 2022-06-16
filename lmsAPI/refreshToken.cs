using System.ComponentModel.DataAnnotations;
namespace lmsAPI
{
    public class refreshToken
    {
        public string? old_token { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
    }
}
