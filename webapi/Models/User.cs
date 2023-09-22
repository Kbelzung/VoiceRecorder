using System;
using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        public string? LastName { get; set; }

        public string? FirstName { get; set; }

        public DateTime CreationDate { get; set; }
    }

}
