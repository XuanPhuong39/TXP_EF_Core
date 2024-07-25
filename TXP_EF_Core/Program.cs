using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using TXP_EF_Core;

class Program
{
    static void Main(string[] args)
    {
        using (var context = new SchoolContext())
        {
            // Create the database if it doesn't exist
            context.Database.EnsureCreated();

            // Create entity objects
            var grd1 = new Grade() { GradeName = "1st Grade" };
            var std1 = new Student() { FirstName = "Yash", LastName = "Malhotra", Grade = grd1 };

            // Add entities to the context
            context.Students.Add(std1);

            // Save data to the database tables
            context.SaveChanges();

            // Retrieve all the students from the database
            foreach (var student in context.Students)
            {
                Console.WriteLine($"First Name: {student.FirstName}, Last Name: {student.LastName}");
            }
        }
        using (var context = new SchoolContext())
        {
            var student = context.Students.FirstOrDefault();
            DisplayStates(context.ChangeTracker.Entries());
        }
        using (var context = new SchoolContext())
        {
            context.Students.Add(new Student() { FirstName = "Bill", LastName = "Gates" });
            DisplayStates(context.ChangeTracker.Entries());
        }
        using (var context = new SchoolContext())
        {
            var student = context.Students.FirstOrDefault();
            student.LastName = "Friss";
            DisplayStates(context.ChangeTracker.Entries());
        }
        using (var context = new SchoolContext())
        {
            var student = context.Students.FirstOrDefault();
            context.Students.Remove(student);
            DisplayStates(context.ChangeTracker.Entries());
        }
        var disconnectedEntity = new Student() { StudentId = 1, FirstName = "Bill" };

        using (var context = new SchoolContext())
        {
            Console.Write(context.Entry(disconnectedEntity).State);
        }
        var context2 = new SchoolContext();
        var studentsWithSameName = context2.Students
                                          .Where(s => s.FirstName == GetName())
                                          .ToList();
        var context3 = new SchoolContext();

        var studentWithGrade = context3.Students
                                   .Where(s => s.FirstName == "Bill")
                                   .Include(s => s.Grade)
                                   .FirstOrDefault();
        var context4 = new SchoolContext();

        var studentWithGrade2 = context4.Students
                                .Where(s => s.FirstName == "Bill")
                                .Include("Grade")
                                .FirstOrDefault();


    }
    static void DisplayStates(IEnumerable<EntityEntry> entries)
    {
        foreach (var entry in entries)
        {
            Console.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: {entry.State}");
        }
    }
    public static string GetName()
    {
        return "Bill";
    }
}
