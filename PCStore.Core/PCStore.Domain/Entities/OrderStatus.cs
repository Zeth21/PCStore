using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCStore.Domain.Entities
{
    public class OrderStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StatusId { get; set; }
        public DateTime StatusDate { get; set; } = DateTime.Now;
        public int StatusNameId { get; set; }
        public int OrderId { get; set; }

        [ForeignKey("StatusNameId")]
        public StatusName? StatusName { get; set; }

        [ForeignKey("OrderId")]
        public Order? Order { get; set; }

    }
}
