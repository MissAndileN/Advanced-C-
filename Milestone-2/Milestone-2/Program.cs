using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milestone2
{
    //Enum for menu navigation
    enum Menu
    {
        CaptureDetails = 1,
        CheckDiscountQualification,
        ShowQualificationStats,
        ExitProgram
    }
    internal class Program
    {
        //Collections for storing student data
        static List<Student> Students = new List<Student>();
        static List<Student> QualifiedStudents = new List<Student>();

        //Counters for discount stats
        static int GrantedDiscountCount = 0;
        static int DeniedDiscountCount = 0;
        static void Main(string[] args)
        {
            int option;
            bool continueRunning = true;

            //Loop for menu-driven program
            while (continueRunning)
            {
                Console.Clear();
                DisplayMenu();

                if (int.TryParse(Console.ReadLine(), out option) && Enum.IsDefined(typeof(Menu), option))
                {
                    ProcessMenuOption((Menu)option, ref continueRunning);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 4.");
                    Console.ReadLine();
                }
            }
        }

        //Menu options display
        static void DisplayMenu()
        {
            Console.WriteLine("BELGIUM CAMPUS MENU");
            Console.WriteLine("1. Capture Details");
            Console.WriteLine("2. Check Discount Qualification");
            Console.WriteLine("3. Show Qualification Stats");
            Console.WriteLine("4. Exit Program");
            Console.WriteLine("Choose an option from the menu:");
        }

        //Process selected menu option
        static void ProcessMenuOption(Menu option, ref bool continueRunning)
        {
            switch (option)
            {
                case Menu.CaptureDetails:
                    CaptureDetails(); //Capture student input
                    break;
                case Menu.CheckDiscountQualification:
                    CheckQualification(); //Evaluate student eligibility
                    break;
                case Menu.ShowQualificationStats:
                    ShowStats(); //Display results
                    break;
                case Menu.ExitProgram:
                    Console.WriteLine("You are now exiting the program.");
                    continueRunning = false;
                    break;
            }
            if (continueRunning) Console.ReadLine();
        }

        //Method to capture and validate student input
        static void CaptureDetails()
        {
            Console.Clear();
            bool continueCapture = true;

            while (continueCapture)
            {
                try
                {
                    //Full Name - required, letters only
                    string fullName;
                    do
                    {
                        Console.WriteLine("Enter Full Name (letters and spaces only):");
                        fullName = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(fullName) || !IsValidName(fullName))
                        {
                            Console.WriteLine("Invalid input. Name must contain letters and spaces only.");
                            fullName = null;
                        }
                    } while (fullName == null);

                    //Residence Student - true/false
                    bool isResidenceStudent;
                    while (true)
                    {
                        Console.WriteLine("Are you a residence student? (true/false):");
                        if (bool.TryParse(Console.ReadLine(), out isResidenceStudent))
                            break;
                        Console.WriteLine("Invalid input. Please enter 'true' or 'false'.");
                    }

                    // ✅ Years on Campus
                    int yearsOnCampus;
                    while (true)
                    {
                        Console.WriteLine("Number of years on campus:");
                        if (int.TryParse(Console.ReadLine(), out yearsOnCampus) && yearsOnCampus >= 0)
                            break;
                        Console.WriteLine("Invalid input. Please enter a non-negative number.");
                    }

                    //Years at Current Residence
                    int yearsAtCurrentResidence;
                    while (true)
                    {
                        Console.WriteLine("Number of years at current residence:");
                        if (int.TryParse(Console.ReadLine(), out yearsAtCurrentResidence) && yearsAtCurrentResidence >= 0)
                            break;
                        Console.WriteLine("Invalid input. Please enter a non-negative number.");
                    }

                    //Monthly Allowance/Salary
                    double monthlySalary;
                    while (true)
                    {
                        Console.WriteLine("Monthly allowance/salary:");
                        if (double.TryParse(Console.ReadLine(), out monthlySalary) && monthlySalary >= 0)
                            break;
                        Console.WriteLine("Invalid input. Please enter a non-negative amount.");
                    }

                    //Average Mark
                    double averageMark;
                    while (true)
                    {
                        Console.WriteLine("Average mark across all subjects:");
                        if (double.TryParse(Console.ReadLine(), out averageMark) && averageMark >= 0 && averageMark <= 100)
                            break;
                        Console.WriteLine("Invalid input. Please enter a mark between 0 and 100.");
                    }

                    //Add student to collection
                    Students.Add(new Student(
                        fullName,
                        isResidenceStudent,
                        yearsOnCampus,
                        yearsAtCurrentResidence,
                        monthlySalary,
                        averageMark
                    ));

                    Console.WriteLine("Student details captured successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }

                Console.WriteLine("Do you want to add another student? (Y/N)");
                continueCapture = Console.ReadKey().KeyChar.ToString().Equals("Y", StringComparison.OrdinalIgnoreCase);
                Console.WriteLine();
            }
        }

        //Helper method to ensure name contains only letters/spaces
        static bool IsValidName(string name)
        {
            foreach (char c in name)
            {
                if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                    return false;
            }
            return true;
        }

        //Method to check and display discount eligibility
        static void CheckQualification()
        {
            Console.Clear();
            if (Students.Count == 0)
            {
                Console.WriteLine("No students available to check. Please capture details first.");
                return;
            }

            Console.WriteLine("Checking discount qualifications...");
            QualifiedStudents.Clear();
            GrantedDiscountCount = 0;
            DeniedDiscountCount = 0;

            foreach (var student in Students)
            {
                if (student.IsEligibleForDiscount)
                {
                    QualifiedStudents.Add(student);
                    GrantedDiscountCount++;
                    Console.WriteLine($"{student.FullName} qualifies for the discount.");
                }
                else
                {
                    DeniedDiscountCount++;
                    Console.WriteLine($"{student.FullName} does not qualify for the discount.");
                }
            }
        }

        //Method to show overall stats
        static void ShowStats()
        {
            Console.Clear();
            Console.WriteLine("Discount Qualification Stats:");
            Console.WriteLine($"Total Students: {Students.Count}");
            Console.WriteLine($"Students granted discount: {GrantedDiscountCount}");
            Console.WriteLine($"Students denied discount: {DeniedDiscountCount}");

            if (QualifiedStudents.Count > 0)
            {
                Console.WriteLine("\nQualified Students:");
                foreach (var student in QualifiedStudents)
                {
                    Console.WriteLine(student.ToString());
                }
            }
        }
    }
}

