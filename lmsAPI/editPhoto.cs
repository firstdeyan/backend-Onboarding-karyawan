using System.ComponentModel.DataAnnotations;
namespace lmsAPI
{
    public class editPhoto
    {
        public string email { get; set; } = string.Empty;
        public IFormFile[]? files { get; set; }
    }
}
