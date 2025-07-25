using PCStore.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Results.NotificationResults
{
    public class GetAllNotificationsByUserIdResult
    {
        public int NotificationId { get; set; }
        public NotifType NotificationType { get; set; }
        public DateTime NotificationDate { get; set; }
        public bool NotificationStatus { get; set; } 
        public string NotificationTitle { get; set; }= null!;
    }
}
