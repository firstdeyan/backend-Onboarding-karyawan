﻿using System.ComponentModel.DataAnnotations;

namespace lmsAPI
{
    public class editAdmin
    {
        [Key]
        public string email { get; set; } = string.Empty;
        public string admin_name { get; set; } = string.Empty;
        public int role_id { get; set; } = 1;
    }
}
