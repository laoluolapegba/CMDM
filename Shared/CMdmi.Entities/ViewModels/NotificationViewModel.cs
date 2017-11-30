
namespace CMdm.Entities.ViewModels
{
    using System;
    public class NotificationViewModel
    {
        public int Count { get; set; }
        public int NotificationType { get; set; }
        public string BadgeClass { get; set; }
        public string Title { get; set; }
        public string SenderId { get; set; }
        public string MsgBody { get; set; }
        public bool IsDismissed { get; set; }
        public DateTime Timestamp { get; set; }
        public int NoteBadge { get; set; }
    }
}