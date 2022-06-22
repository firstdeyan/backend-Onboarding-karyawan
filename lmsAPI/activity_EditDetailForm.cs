using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace lmsAPI
{
    public class activity_EditDetailForm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int activity_id { get; set; }
        [MaxLength(50)]
        public string detail_name { get; set; } = string.Empty;
        [MaxLength(500)]
        public string detail_desc { get; set; } = string.Empty;
        [MaxLength(100)]
        public IFormFile[]? files { get; set; }

        [MaxLength(20)]
        public string detail_type { get; set; } = string.Empty;
        public string? detail_link { get; set; } = string.Empty;

        public int detail_urutan { get; set; }
    }
}
