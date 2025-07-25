using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Results.NotificationResults
{
    public class MarkNotificationAsSeenResult
    {
        public int NotificationId { get; set; }
        public bool NotificationStatus { get; set; } = true;
    }
}
