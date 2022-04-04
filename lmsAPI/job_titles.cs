using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lmsAPI
{
    public class job_titles
    {   
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string jobtitle_name { get; set; } = string.Empty;
        public string jobtitle_description { get; set;} = string.Empty;
    }

}
