﻿using System.ComponentModel.DataAnnotations;

namespace RestFull_Api.Models.Domains.DTO
{
    public class loginRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
