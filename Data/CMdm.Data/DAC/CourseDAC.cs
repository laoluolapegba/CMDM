using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMdm.Entities.Domain.Courses;

namespace CMdm.Data.DAC
{
    public class CourseDAC
    {
        private AppDbContext db;
        public Course Add(Course courseentity)
        {
            using (var db = new AppDbContext())
            {
                //db.Set<Course>().Add(course);
                db.Courses.Add(courseentity);
                db.SaveChanges();

                return courseentity;
            }
        }
    }
}
