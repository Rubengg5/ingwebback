using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Parcial3.Models
{
    public class Usuario
    {
        [BsonId]
        public Guid Id { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}
