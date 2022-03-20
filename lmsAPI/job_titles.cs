using System.ComponentModel.DataAnnotations;

namespace lmsAPI
{
    public class job_titles
    {   
        [Key]
        public int id { get; set; }
        public string jobtitle_name { get; set; } = string.Empty;
        public string jobtitle_decription { get; set;} = string.Empty;
    }

}
