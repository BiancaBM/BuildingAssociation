using System.ComponentModel.DataAnnotations;

namespace Repositories.Entities
{
    public class BaseEntity
    {
        [Key]
        public long? UniqueId { get; set; }
    }
}
