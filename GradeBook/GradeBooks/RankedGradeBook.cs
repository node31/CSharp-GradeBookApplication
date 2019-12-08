using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            this.Type = Enums.GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if(this.Students.Count < 5)
            {
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");
            }

            int numStudents = this.Students.Count;
            int threshold = (int)Math.Ceiling(numStudents * 0.2);
            //var grades = Students.OrderByDescending(x => x.AverageGrade).Select(x => x.AverageGrade).ToList();
            List<double> grades = new List<double>();
            foreach(var student in this.Students)
            {
                grades.Add(student.AverageGrade);
            }

            grades.Sort();
            grades.Reverse();
            if(grades[threshold - 1] <= averageGrade)
            {
                return 'A';
            }else if (grades[(threshold * 2) - 1] <= averageGrade)
            {
                return 'B';
            }else if(grades[(threshold*3)-1] <= averageGrade)
            {
                return 'C';
            }else if (grades[(threshold * 4) - 1] <= averageGrade)
            {
                return 'D';
            }

            return 'F';
        }

        public override void CalculateStatistics()
        {
            if(this.Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if(this.Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStudentStatistics(name);
        }
    }
}
