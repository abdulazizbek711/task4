using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace project4.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRequired]
        public string? Name { get; set; }

        [BsonRequired]
        public string? Email { get; set; }

        [BsonRequired]
        public DateTime LastLoginTime { get; set; }

        [BsonRequired]
        public DateTime RegistrationTime { get; set; }

        [BsonRequired]
        public bool IsBlocked { get; set; }

        [BsonRequired]
        public string? PasswordHash { get; set; }

        [BsonRequired]
        [BsonRepresentation(BsonType.String)] // Store enum as string
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

