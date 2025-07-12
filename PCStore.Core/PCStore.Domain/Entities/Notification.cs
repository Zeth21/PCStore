using PCStore.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCStore.Domain.Entities
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotificationId { get; set; }
        public NotifType NotificationType { get; set; }
        public DateTime NotificationDate { get; set; }
        public bool NotificationStatus { get; set; }
        public string? NotificationContent { get; set; }
        public string? NotificationUserId { get; set; }

        [ForeignKey("NotificationUserId")]
        public User? User { get; set; }
    }
}
