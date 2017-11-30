using System;
using System.Collections.Generic;
using CMdm.Entities.Domain.Courses;
using CMdm.Entities.Core;

namespace CMdm.Services.Courses
{
    public interface ICourseService
    {
        #region Courses

        IList<Course> GetAllCourses();
        /// <summary>
        /// Gets all courses
        /// </summary>
        /// <param name="createdFromUtc">Created date from (UTC); null to load all records</param>
        /// <param name="createdToUtc">Created date to (UTC); null to load all records</param>
        /// <param name="affiliateId">Affiliate identifier</param>
        /// <param name="vendorId">Vendor identifier</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Customers</returns>
        //IPagedList<Course> GetAllCourses(DateTime? createdFromUtc = null,
        //    DateTime? createdToUtc = null, int affiliateId = 0, int vendorId = 0,
        //    int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Delete a course
        /// </summary>
        /// <param name="courses">Course</param>
        void DeleteCourse(Course course);

        /// <summary>
        /// Gets a course
        /// </summary>
        /// <param name="courseId">Course identifier</param>
        /// <returns>A courses</returns>
        Course GetCourseById(int courseId);

        /// <summary>
        /// Get courses by identifiers
        /// </summary>
        /// <param name="courserIds">Course identifiers</param>
        /// <returns>Courses</returns>
        IList<Course> GetCoursesByIds(int[] courseIds);

        /// <summary>
        /// Insert a course
        /// </summary>
        /// <param name="course">Customer</param>
        void InsertCourse(Course course);

        /// <summary>
        /// Updates the course
        /// </summary>
        /// <param name="course">Course</param>
        void UpdateCourse(Course course);


        #endregion
    }
}
