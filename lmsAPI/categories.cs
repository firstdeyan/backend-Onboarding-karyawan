using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lmsAPI
{
    public class categories
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [MaxLength(100)]
        public string category_name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string category_description { get; set; } = string.Empty;
        public int duration { get; set; }
    }
}
