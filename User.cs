using System;
using System.ComponentModel.DataAnnotations;

namespace project4.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        public DateTime LastLoginTime { get; set; }

        public DateTime RegistrationTime { get; set; }

        public bool IsBlocked { get; set; }

        [Required]
        public string? PasswordHash { get; set; }

        [Required]
        public UserStatus Status { get; set; }

        public User()
        {
            // Initialize properties with default values if needed
            Name = "";
            Email = "";
            LastLoginTime = DateTime.MinValue;
            RegistrationTime = DateTime.MinValue;
            IsBlocked = false;
        }
    }
}

