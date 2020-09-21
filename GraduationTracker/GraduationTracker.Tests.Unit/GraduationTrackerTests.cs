using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace GraduationTracker.Tests.Unit
{
    [TestClass]
    public class GraduationTrackerTests
    {
        [TestMethod]
        public void TestHasCredits()
        {
            var tracker = new GraduationTracker();
            var diploma = Repository.GetDiploma(1);
            var students = Enumerable.Range(1, 4).ToArray().Select(w => Repository.GetStudent(w)).ToArray();
            Assert.IsTrue(students.Select(w => tracker.HasGraduated(diploma, w)).Where(c => c.Item1).Any());
        }

        [TestMethod]
        public void TestHasNoCredits()
        {
            var tracker = new GraduationTracker();
            var diploma = Repository.GetDiploma(1);
            var students = Enumerable.Range(4, 1).ToArray().Select(w => Repository.GetStudent(w)).ToArray();
            Assert.IsFalse(students.Select(w => tracker.HasGraduated(diploma, w)).Where(c => c.Item1).Any());
        }
    }
}
