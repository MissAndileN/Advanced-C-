using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milestone2
{
    internal class Student
    {
        //Private fields for student data
        private string fullName;
        private bool isResidenceStudent;
        private int yearsOnCampus;
        private int yearsAtCurrentResidence;
        private double monthlySalary;
        private double averageMark;

        //Computed property to check if the student qualifies for the discount
        public bool IsEligibleForDiscount =>
            IsResidenceStudent &&
            YearsOnCampus > 1 &&
            AverageMark > 85 &&
            MonthlySalary <= 1000;

        //Constructor to initialize all student properties
        public Student(string fullName, bool isResidenceStudent, int yearsOnCampus, int yearsAtCurrentResidence, double monthlySalary, double averageMark)
        {
            this.FullName = fullName;
            this.IsResidenceStudent = isResidenceStudent;
            this.YearsOnCampus = yearsOnCampus;
            this.YearsAtCurrentResidence = yearsAtCurrentResidence;
            this.MonthlySalary = monthlySalary;
            this.AverageMark = averageMark;
        }

        //Public properties (with encapsulation)
        public string FullName { get => fullName; set => fullName = value; }
        public bool IsResidenceStudent { get => isResidenceStudent; set => isResidenceStudent = value; }
        public int YearsOnCampus { get => yearsOnCampus; set => yearsOnCampus = value; }
        public int YearsAtCurrentResidence { get => yearsAtCurrentResidence; set => yearsAtCurrentResidence = value; }
        public double MonthlySalary { get => monthlySalary; set => monthlySalary = value; }
        public double AverageMark { get => averageMark; set => averageMark = value; }

        //Override ToString() to format how student info is displayed
        public override string ToString()
        {
            return $"Full Name: {fullName} | Residence Student: {isResidenceStudent} | " +
                   $"Years on Campus: {yearsOnCampus} | Years at Residence: {yearsAtCurrentResidence} | " +
                   $"Monthly Salary: {monthlySalary:C} | Average Mark: {averageMark}%";
        }
    }
}
