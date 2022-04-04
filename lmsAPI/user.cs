﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace lmsAPI
{
    public class user
    {

        [Key]
        public string email { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        [JsonIgnore]
        public byte[]? passwordHash { get; set; }
        [JsonIgnore]
        public byte[]? passwordSalt { get; set; }
        public roles role_ { get; set; }
        public job_titles jobtitle_ { get; set; }
        public string gender { get; set; } = string.Empty;
        public DateTime birthdate { get; set; }
        public string phone_number { get; set; } = string.Empty;
        public double progress { get; set; }
    }
}
