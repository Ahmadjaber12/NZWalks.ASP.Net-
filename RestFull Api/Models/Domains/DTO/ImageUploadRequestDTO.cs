﻿using System.ComponentModel.DataAnnotations;

namespace RestFull_Api.Models.Domains.DTO
{
    public class ImageUploadRequestDTO
    {
        [Required]
        public IFormFile File {  get; set; }

        [Required]
        public string FileName { get; set; }

        public string? FileDescription { get; set; }
    }
}
