﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace lmsAPI
{
    public class activity_details
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public activities activity_ { get; set; }
        public string detail_name { get; set; } = string.Empty;
        public string detail_desc { get; set; } = string.Empty;
        public string detail_link { get; set; } = string.Empty;
        public string detail_type { get; set; } = string.Empty;
        public int detail_urutan { get; set; }
        [JsonIgnore]
        public int activity_id { get; set; }
    }
}