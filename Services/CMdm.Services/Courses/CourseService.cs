using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMdm.Data;
using CMdm.Entities.Core;
using CMdm.Entities.Core.Data;
using CMdm.Entities.Domain.Courses;

namespace CMdm.Services.Courses
{
    /// <summary>
    /// Store service
    /// </summary>
    public partial class CourseService : ICourseService
    {
        #region Fields

        private readonly IRepository<Course> _courseRepository;
        private AppDbContext _database;
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="storeRepository">Store repository</param>
        /// <param name="eventPublisher">Event published</param>
        public CourseService(IRepository<Course> courseRepository)
        {
            this._courseRepository = courseRepository;
        }
        public CourseService()
        {
            this._database = new AppDbContext();
        }

        #endregion
        #region Methods
        /// <summary>
        /// Deletes a course
        /// </summary>
        /// <param name="course"></param>
        public void DeleteCourse(Course course)
        {
            if (course == null)
                throw new ArgumentNullException("course");

            var allCourses = GetAllCourses();
            if (allCourses.Count == 1)
                throw new Exception("You cannot delete the only configured course");

            _courseRepository.Delete(course);
            //event notification
            //_eventPublisher.EntityDeleted(store);
        }

        /// <summary>
        /// Gets all stores
        /// </summary>
        /// <returns>Stores</returns>
        public virtual IList<Course> GetAllCourses()
        {
            return
            (
                 from s in _database.Courses
                            orderby s.CourseCode, s.Id
                            select s
                //var courses = query.ToList();
               // return courses;
            ).ToList();
        }
        //public IPagedList<Course> GetAllCourses(DateTime? createdFromUtc = default(DateTime?), DateTime? createdToUtc = default(DateTime?), int affiliateId = 0, int vendorId = 0, int pageIndex = 0, int pageSize = int.MaxValue)
        //{
        //    throw new NotImplementedException();
        //}


        public Course GetCourseById(int courseId)
        {
            if (courseId == 0)
                return null;
            return _database.Courses.Find(courseId);
        }

        public IList<Course> GetCoursesByIds(int[] courseIds)
        {
            throw new NotImplementedException();
        }

        public void InsertCourse(Course courseentity)
        {
            if (courseentity == null)
                throw new ArgumentNullException("course");
            _database.Courses.Add(courseentity);
            _database.SaveChanges();
            
            //event notification
            //_eventPublisher.EntityInserted(store);
        }


        public void UpdateCourse(Course course)
        {
            if (course == null)
                throw new ArgumentNullException("course");

            _courseRepository.Update(course);

            //event notification
            //_eventPublisher.EntityUpdated(store);
        }
        #endregion
    }
}
