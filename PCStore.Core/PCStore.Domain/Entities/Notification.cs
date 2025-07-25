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
        public DateTime NotificationDate { get; set; } = DateTime.Now;
        public bool NotificationStatus { get; set; } = false;
        public string NotificationTitle { get; set; } = null!;
        public string NotificationContent { get; set; } = null!;
        public string NotificationUserId { get; set; } = null!;

        [ForeignKey("NotificationUserId")]
        public User? User { get; set; }
    }
}
