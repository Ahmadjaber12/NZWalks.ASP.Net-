using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RestFull_Api.Models.Domains.DTO
{
    public class RegisterDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
       public string Username { get; set; }

        [Required]
        [DataType (DataType.Password)]
       public string Password { get; set; }

        public string[] Roles{ get; set; }
    }
}
