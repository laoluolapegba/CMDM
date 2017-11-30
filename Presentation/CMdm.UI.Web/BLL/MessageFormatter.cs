using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMdm.UI.Web.BLL
{
    public enum MessageType { Success, Error, Notice }

    public class MessageFormatter
    {
        public static string GetFormattedSuccessMessage(string message)
        {
            return GetFormattedMessage(message, MessageType.Success);
        }

        public static string GetFormattedErrorMessage(string message)
        {
            return GetFormattedMessage(message, MessageType.Error);
        }

        public static string GetFormattedNoticeMessage(string message)
        {
            return GetFormattedMessage(message, MessageType.Notice);
        }

        public static string GetFormattedMessage(string message, MessageType messageType = MessageType.Notice)
        {
            switch (messageType)
            {
                case MessageType.Success: return "<div class='alert alert-success'>" + message + "</div>";
                case MessageType.Error: return "<div class='alert alert-danger alert-dismissable'><a href='' class='close'>×</a>" + message + "</div>";
                default: return "<div class='alert alert-warning alert-dismissable'><a href='' class='close'>×</a>" + message + "</div>";
            }
        }

    }
}