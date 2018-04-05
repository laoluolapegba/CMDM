using CMdm.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace CMdm.UI.Web.Models.Logging
{
    public partial class LogListModel
    {
        public LogListModel()
        {
            AvailableLogLevels = new List<SelectListItem>();
        }
        public int Id { get; set; }
        [DisplayName("CreatedOnFrom")]
        [UIHint("DateNullable")]
        public DateTime? CreatedOnFrom { get; set; }

        [DisplayName("CreatedOnTo")]
        [UIHint("DateNullable")]
        public DateTime? CreatedOnTo { get; set; }

        [DisplayName("Message")]
        [AllowHtml]
        public string Message { get; set; }

        [DisplayName("LogLevel")]
        public int LogLevelId { get; set; }


        public IList<SelectListItem> AvailableLogLevels { get; set; }
    }
}