namespace CMdm.Data.Rbac
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("CM_NOTIFICATION")]
    public partial class CM_NOTIFICATION
    {
        //    public int NotificationId { get; set; }
        //    public string Title { get; set; }
        //    public NotificationType NotificationType { get; set; }
        //    public string Controller { get; set; }
        //    public string Action { get; set; }
        //    public string UserId { get; set; }
        //    public bool IsDismissed { get; set; }


        [Key]
        public int NOTEID { get; set; }
        [Required]
        [StringLength(50)]
        public string TITLE { get; set; }
        public int NOTIFICATIONTYPE { get; set; }
        [Required]
        [StringLength(30)]
        public string CONTROLLER { get; set; }
        [Required]
        [StringLength(30)]
        public string ACTION { get; set; }
        [Required]
        [StringLength(20)]
        public string USERID { get; set; }
        public bool ISDISMISSED { get; set; }
        public DateTime NOTEDATE { get; set; }
        public string SENDERID { get; set; }
        public string MSGBODY { get; set; }
        public int NOTEBADGE { get; set; }
    }
    public enum NotificationType
    {
        General = 1,
        Inbox = 2
    }
    public enum NotificationBadge
    {
        Orders = 1,
        Authorisation = 2,
        UserMgmt = 3,
        CashLevel = 4
    }
}