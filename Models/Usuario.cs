using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Usuario
    {
        [BsonId]
        public Guid Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string BirthDay { get; set; }
        [Required]
        public int PasswordSalt { get; set; }
        [Required]
        public int PasswordHash { get; set; }
    }
}
