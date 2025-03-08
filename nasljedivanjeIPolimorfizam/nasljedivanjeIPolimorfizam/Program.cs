using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static nasljedivanjeIPolimorfizam.Program;

namespace nasljedivanjeIPolimorfizam
{
    internal class Program
    {
        public class Desert
        {
            private string name;
            private double weight;
            private int calories;

            public Desert(string name, double weight, int calories)
            {
                this.name = name;
                this.weight = weight;
                this.calories = calories;
            }
            public string Name { get => name; set => name = value; }
            public double Weight { get => weight; set => weight = value; }
            public int Calories { get => calories; set => calories = value; }

            public override string ToString()
            {
                string ispis = "Desert: " + name + ", Weight: " + weight + ", Calories: " + calories;
                return ispis;

            }
            public string getDesertType()
            {
                return "desert";
            }


        }

        public class Cake : Desert
        {
            private bool containsGluten;
            private string cakeType;

            public Cake(string name, double weight, int calories, bool containsGluten, string cakeType)
                : base(name, weight, calories)
            {
                this.containsGluten = containsGluten;
                this.cakeType = cakeType;
            }
            public bool ContainsGluten { get => containsGluten; set => containsGluten = value; }
            public string CakeType { get => cakeType; set => cakeType = value; }

            public override string ToString()
            {
                return base.ToString() + "Sadrzi Gluten: " + containsGluten + ", Tip torte: " + cakeType;
            }

            public string GetDessertType()
            {
                return cakeType + "cake";
            }


        }

        public class IceCream : Desert
        {
            private string flavour;
            private string color;

            public IceCream(string flavour, string color, string name, double weight, int calories)
                : base(name, weight, calories)
            {
                this.flavour = flavour;
                this.color = color;
            }

            public string Flavour { get => flavour; set => flavour = value; }
            public string Color { get => color; set => color = value; }

            public override string ToString()
            {
                return base.ToString() + "Okus: " + flavour + ", boja: " + color;
            }
            public string GetDesertType()
            {
                return "ice cream";
            }
        }

        public class Person
        {
            private string name;
            private string surname;
            private int age;

            public Person(string name, string surname, int age)
            {
                this.name = name;
                this.surname = surname;
                this.age = age;
            }

            public string Name { get => name; set => name = value; }

            public string Surname { get => surname; set => surname = value; }

            public int Age { get => age; set => age = value; }


            public override string ToString()
            {
                return name + surname + ", Godina: " + age;
            }

            public override bool Equals(object obj)
            {
                if (obj is Person other)
                {
                    return this.name == other.name && this.surname == other.surname && this.age == other.age;
                }
                return false;
            }

        }

        public class Student : Person
        {
            private string studentId;
            private short academicYear;

            public Student(string name, string surname, int age, string studentId, short academicYear)
      : base(name, surname, age)
            {
                this.studentId = studentId;
                this.academicYear = academicYear;
            }

            public string StudentId { get => studentId; set => studentId = value; }
            public short AcademicYear { get => academicYear; set => academicYear = value; }

            public override string ToString()
            {
                return base.ToString() + "Student ID: " + studentId + ", Academic Year: " + academicYear;
            }

            public override bool Equals(object obj)
            {
                if (obj is Student other)
                {
                    return this.studentId == other.studentId;
                }
                return false;
            }
        }

        public class Teacher : Person
        {
            public string email;
            public string subject;
            public double salary;

            public Teacher(string name, string surname, int age, string email, string subject, double salary)
                : base(name, surname, age)
            {
                this.email = email;
                this.subject = subject;
                this.salary = salary;
            }

            public string Email { get => email; set => email = value; }
            public string Subject { get => subject; set => subject = value; }
            public double Salary { get => salary; set => salary = value; }

            public void IncreaseSalary(int percentage)
            {
                salary += salary * percentage / 100;
            }

            public static void IncreaseSalary(int percentage, params Teacher[] teachers)
            {
                foreach (var teacher in teachers)
                {
                    teacher.IncreaseSalary(percentage);
                }
            }

            public override string ToString()
            {
                return base.ToString() + "Email: " + email + ", Subject: " + subject + ", Salary: " + salary;
            }

            public override bool Equals(object obj)
            {
                if (obj is Teacher other)
                {
                    return this.email == other.email;
                }
                return false;
            }

        }



        public class CompetitionEntry
        {
            private Teacher teacher;
            private Desert dessert;
            private Student[] students;
            private int[] grades;
            private int ratingCount;

            public CompetitionEntry(Teacher teacher, Desert dessert)
            {
                this.teacher = teacher;
                this.dessert = dessert;
                this.students = new Student[3];
                this.grades = new int[3];
                this.ratingCount = 0;
            }

            public Teacher Teacher => teacher;
            public Desert Dessert => dessert;
            public Student[] Students => students;
            public int[] Grades => grades;

            public bool AddRating(Student student, int grade)
            {
                if (ratingCount >= 3)
                {
                    return false;
                }

                for (int i = 0; i < ratingCount; i++)
                {
                    if (students[i].Equals(student))
                    {
                        return false;
                    }
                }

                students[ratingCount] = student;
                grades[ratingCount] = grade;
                ratingCount++;
                return true;
            }

            public double GetRating()
            {
                if (ratingCount == 0)
                {
                    return 0; 
                }

                int sum = 0;
                for (int i = 0; i < ratingCount; i++)
                {
                    sum += grades[i];
                }
                return (double)sum / ratingCount;
            }
        }


        public class UniMasterChef
        {
            private CompetitionEntry[] entries;
            private int entryCount;

            public UniMasterChef(int numberOfEntries)
            {
                this.entries = new CompetitionEntry[numberOfEntries];
                this.entryCount = 0;
            }

            public bool AddEntry(CompetitionEntry entry)
            {
                if (entryCount >= entries.Length)
                {
                    return false; 
                }
                entries[entryCount++] = entry;
                return true;
            }

            public Desert GetBestDessert()
            {
                if (entryCount == 0)
                {
                    return null; 
                }

                CompetitionEntry bestEntry = entries[0];
                for (int i = 1; i < entryCount; i++)
                {
                    if (entries[i].GetRating() > bestEntry.GetRating())
                    {
                        bestEntry = entries[i];
                    }
                }
                return bestEntry.Dessert;
            }

            public static Person[] GetInvolvedPeople(CompetitionEntry entry)
            {
                List<Person> people = new List<Person>();
                people.Add(entry.Teacher);

                foreach (Student student in entry.Students)
                {
                    if (student != null)
                    {
                        people.Add(student);
                    }
                }

                return people.ToArray();
            }
        }




        static void Main(string[] args)
        {
            Desert genericDessert = new Desert("Chocolate Mousse", 120, 300);
            Cake cake = new Cake("Raspberry chocolate cake #3", 350.5, 400, false, "birthday");
            Teacher t1 = new Teacher("Dario", "Tušek", 42, "dario.tusek@fer.hr", "OOP", 10000);
            Teacher t2 = new Teacher("Doris", "Bezmalinović", 43, "doris.bezmalinovic@fer.hr", "OOP", 10000);
            Student s1 = new Student("Janko", "Horvat", 18, "0036312123", 1);
            Student s2 = new Student("Ana", "Kovač", 19, "0036387656", 2);
            Student s3 = new Student("Ivana", "Stanić", 19, "0036392357", 1);
            UniMasterChef competition = new UniMasterChef(2);
            CompetitionEntry e1 = new CompetitionEntry(t1, genericDessert);
            competition.AddEntry(e1);
            Console.WriteLine("Entry 1 rating: " + e1.GetRating());
            e1.AddRating(s1, 4);
            e1.AddRating(s2, 5);
            Console.WriteLine("Entry 1 rating: " + e1.GetRating());
            CompetitionEntry e2 = new CompetitionEntry(t2, cake);
            e2.AddRating(s1, 4);
            e2.AddRating(s3, 5);
            e2.AddRating(s2, 5);
            competition.AddEntry(e2);
            Console.WriteLine("Entry 2 rating: " + e2.GetRating());
            Console.WriteLine("Best dessert is: " + competition.GetBestDessert().Name);
            Person[] e2persons = UniMasterChef.GetInvolvedPeople(e2);

            foreach (Person p in e2persons)
            {
                Console.WriteLine(p);
            }
            Console.ReadKey();
        }
    }
}

