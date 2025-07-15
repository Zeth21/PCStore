using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PCStore.Domain.Entities
{
    public class StatusName
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StatusNameId { get; set; }
        public required string StatusNameString { get; set; }

        [JsonIgnore]
        public ICollection<OrderStatus>? OrderStatues { get; set; }
    }
}
