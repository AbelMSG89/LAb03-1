
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace WpfApp01C
{
    public class StudentDataService
    {
        private static string connectionString = "Server=.\\SQLEXPRESS;Database=Lab03CDB;Integrated Security=true;TrustServerCertificate=true;";

        public List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ProductId, Name, Price FROM Students";
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Student student = new Student
                            {
                                ProductId = Convert.ToInt32(reader[0]),
                                Name = reader[1].ToString(),
                                Price = Convert.ToDecimal(reader[2])
                            };
                            students.Add(student);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error al obtener estudiantes: {ex.Message}");
                }
            }

            return students;
        }

        public List<Student> SearchStudentsByName(string searchTerm)
        {
            List<Student> students = new List<Student>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ProductId, Name, Price FROM Students WHERE Name LIKE @searchTerm";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@searchTerm", $"%{searchTerm}%");

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Student student = new Student
                            {
                                ProductId = Convert.ToInt32(reader[0]),
                                Name = reader[1].ToString(),
                                Price = Convert.ToDecimal(reader[2])
                            };
                            students.Add(student);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error al buscar estudiantes: {ex.Message}");
                }
            }

            return students;
        }
    }
}