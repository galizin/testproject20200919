using System;
using System.Linq;

namespace GraduationTracker
{
    public partial class GraduationTracker
    {
        public Tuple<bool, STANDING> HasGraduated(Diploma diploma, Student student)
        {
            int credits = 0;
            int average = 0;
            int courses = 0;

            foreach (int req in diploma.Requirements)
            {
                Requirement requirement = Repository.GetRequirement(req);
                Course course = student.Courses.SingleOrDefault(c => c.Id == requirement.Courses[0]);
                if (course != null)
                {
                    average += course.Mark;
                    credits += course.Mark > requirement.MinimumMark ? requirement.Credits : 0;
                    courses++;
                }
            }

            if (courses == 0)
                throw new Exception("no courses matching requirements were found");

            average = average / courses;

            STANDING standing = STANDING.None;

            if (average < 50)
                standing = STANDING.Remedial;
            else if (average < 80)
                standing = STANDING.Average;
            else if (average < 95)
                standing = STANDING.MagnaCumLaude;
            else
                standing = STANDING.SummaCumLaude;

            return new Tuple<bool, STANDING>(!(standing == STANDING.Remedial || standing == STANDING.None), standing);
        }
    }
}
