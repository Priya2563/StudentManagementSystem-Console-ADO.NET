using System.Runtime.InteropServices.Marshalling;
using System;
using Microsoft.Data.SqlClient;
namespace CrudConsoleApp_StudentManagementSystem
{
    internal class Program
    {
        static string connectionString = @"Server=.\SQLEXPRESS;DataBase=priyadb;Trusted_Connection=True;TrustServerCertificate=True;";

        static void Main(string[] args)
        {
            Console.WriteLine("=========STUDENT MANAGEMENT MENU============");
            Console.WriteLine("1 : Add Student");
            Console.WriteLine("2 : View all Students");
            Console.WriteLine("3 : Update Student By ID");

            Console.WriteLine("4 : Delete Student By Id");
            Console.WriteLine("5 : Search Student By Name");
            Console.WriteLine("6 : Exit");
            Console.WriteLine("Choose an option 1-6 for Above Operation");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1": AddStudent(); break;
                case "2": ViewAllStudent(); break;
                case "3": UpdateStudent(); break;
                case "4":DeleteStudent(); break;
                case "5":SearchStudent(); break;
                case "6": Console.WriteLine("Exiting"); return;
                default: Console.WriteLine("Invalid Option Try Again."); break;

            }

        }
        static void AddStudent()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                Console.WriteLine("Enter Student Name");
                string name = Console.ReadLine();
                Console.WriteLine("Enter Student Age");
                int age = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Student City");
                string city = Console.ReadLine();
                string InsertQuery = "Insert into student (name, age,city) values (@name,@age,@city)";
                using (SqlCommand cmd = new SqlCommand(InsertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@age", age);
                    cmd.Parameters.AddWithValue("@city", city);
                    int rows = cmd.ExecuteNonQuery();
                    Console.WriteLine($"Added {rows} Student Successfully .");
                }
            }
        }
        static void ViewAllStudent()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string SelectQuery = "select * from student";
                using (SqlCommand cmd = new SqlCommand(SelectQuery, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Id : {reader["id"]},Name : {reader["name"]}, Age : {reader["age"]}, City : {reader["city"]}");
                        }
                    }
                }
            }

        }
        static void UpdateStudent()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                Console.WriteLine("Enter ID of student to update");
                int id = int.Parse(Console.ReadLine());

                Console.Write("Enter new name: ");
                string name = Console.ReadLine();

                Console.Write("Enter new age: ");
                int age = int.Parse(Console.ReadLine());

                Console.Write("Enter new city: ");
                string city = Console.ReadLine();
                string UpdateQuery = "update student set name=@name, age=@age,city=@city where id=@id";
                using (SqlCommand cmd = new SqlCommand(UpdateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@age", age);
                    cmd.Parameters.AddWithValue("@city", city);
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        Console.WriteLine($"Updated {rows} student Successfully");
                    }
                    else
                    {
                        Console.WriteLine("No Student Found with that ID");
                    }
                }
            }
        }
        static void DeleteStudent()
        {
            Console.WriteLine("Enter ID of Student to Deleted : ");
            int id = int.Parse(Console.ReadLine());
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string Query = "delete from student where id=@id";
                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        Console.WriteLine("Student Deleted Suuccessfully.");
                    }
                    else
                    {
                        Console.WriteLine("Student not found with that id.");
                    }
                }
            }
        }

        static void SearchStudent()
        {
            Console.WriteLine("Enter the Student name Which you want to Search");
            string name = Console.ReadLine();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select * from Student where name like @name";
                using (SqlCommand cmd = new SqlCommand(query,conn))
                {
                    cmd.Parameters.AddWithValue("@name", "%" + name + "%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine(" Search REsults");
                        bool found = false;
                        while (reader.Read()){
                            Console.WriteLine($"Id: {reader["Id"]}, Name: {reader["Name"]}, Age: {reader["Age"]}, City: {reader["City"]}");
                            found = true;
                        }
                        if (!found)
                            Console.WriteLine(" ?  No student found with that name.");
                    }
                }
            }

        }
    }
}
