using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMdm.UI.Web.Models.Course
{
    public class CourseViewModel
    {
        public int id { get; set; }

        /// <summary>
        /// Gets or sets a int value for the categoryid column.
        /// </summary>

        public int moduleid { get; set; }

        /// <summary>
        /// Gets or sets a string value for the idnumber column.
        /// </summary>

        public string CourseCode { get; set; }

        /// <summary>
        /// Gets or sets a string value for the course_name column.
        /// </summary>

        public string course_name { get; set; }

        /// <summary>
        /// Gets or sets a string value for the course_description column.
        /// </summary>

        public string course_description { get; set; }

        /// <summary>
        /// Gets or sets a string value for the course_summary column.
        /// </summary>

        public string course_summary { get; set; }

        /// <summary>
        /// Gets or sets a DateTime value for the start_date column.
        /// </summary>

        public DateTime startdate { get; set; }

        /// <summary>
        /// Gets or sets a DateTime value for the end_date column.
        /// </summary>

        public DateTime enddate { get; set; }

        /// <summary>
        /// Gets or sets a bool value for the enabled column.
        /// </summary>

        public bool isactive { get; set; }

        /// <summary>
        /// Gets or sets a int value for the completion_tracking column.
        /// </summary>

        public bool completion_tracking { get; set; }

        /// <summary>
        /// Gets or sets a int value for the completion_notify column.
        /// </summary>

        public bool completion_notify { get; set; }
    }
}