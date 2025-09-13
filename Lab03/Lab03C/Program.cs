using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ConsoleApp
{
    // Clase para representar un estudiante
    public class Student
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    class Program
    {
        private static string connectionString = "Server=.\\SQLEXPRESS;Database=Lab03CDB;Integrated Security=true;";
    

        static void Main(string[] args)
        {
            Console.WriteLine("=== Prueba de Funciones de Base de Datos ===\n");

            // Llamar a las funciones
            Console.WriteLine("1. Obteniendo estudiantes con DataTable:");
            DataTable dtStudents = GetStudentsAsDataTable();
            MostrarDataTable(dtStudents);

            Console.WriteLine("\n2. Obteniendo estudiantes como Lista de objetos:");
            List<Student> listStudents = GetStudentsAsList();
            MostrarLista(listStudents);

            Console.ReadKey();
        }

        // 4. Función que retorna una lista de estudiantes usando DataTable (2 puntos)
        public static DataTable GetStudentsAsDataTable()
        {
            DataTable dataTable = new DataTable();

            try
            {
                // Crear conexión
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Abrir conexión
                    connection.Open();
                    Console.WriteLine("Conexión establecida exitosamente.");

                    // Crear comando SQL
                    string query = "SELECT ProductId, Name, Price FROM students";
                    SqlCommand command = new SqlCommand(query, connection);

                    // Crear adaptador de datos
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    // Llenar el DataTable
                    adapter.Fill(dataTable);

                    Console.WriteLine($"Se obtuvieron {dataTable.Rows.Count} registros.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos: {ex.Message}");
            }

            return dataTable;
        }

        // 5. Función que retorna una lista de estudiantes usando una lista de objetos (2 puntos)
        public static List<Student> GetStudentsAsList()
        {
            List<Student> students = new List<Student>();

            try
            {
                // Crear conexión
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Abrir conexión
                    connection.Open();
                    Console.WriteLine("Conexión establecida exitosamente.");

                    // Crear comando SQL
                    string query = "SELECT ProductId, Name, Price FROM students";
                    SqlCommand command = new SqlCommand(query, connection);

                    // Ejecutar consulta y leer datos
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Student student = new Student
                            {
                                // Usar conversiones más seguras
                                ProductId = Convert.ToInt32(reader[0]),
                                Name = reader[1].ToString(),
                                Price = Convert.ToDecimal(reader[2])
                            };
                            students.Add(student);
                        }
                    }

                    Console.WriteLine($"Se obtuvieron {students.Count} registros.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos: {ex.Message}");
            }

            return students;
        }

        // Función auxiliar para mostrar el contenido de un DataTable
        private static void MostrarDataTable(DataTable dataTable)
        {
            if (dataTable.Rows.Count == 0)
            {
                Console.WriteLine("No hay datos para mostrar.");
                return;
            }

            // Mostrar encabezados
            foreach (DataColumn column in dataTable.Columns)
            {
                Console.Write($"{column.ColumnName}\t");
            }
            Console.WriteLine();
            Console.WriteLine(new string('-', 50));

            // Mostrar filas
            foreach (DataRow row in dataTable.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    Console.Write($"{item}\t");
                }
                Console.WriteLine();
            }
        }

        // Función auxiliar para mostrar la lista de estudiantes
        private static void MostrarLista(List<Student> students)
        {
            if (students.Count == 0)
            {
                Console.WriteLine("No hay datos para mostrar.");
                return;
            }

            Console.WriteLine("ProductId\tName\t\tPrice");
            Console.WriteLine(new string('-', 50));

            foreach (Student student in students)
            {
                Console.WriteLine($"{student.ProductId}\t\t{student.Name}\t\t{student.Price:C}");
            }
        }

        // EJEMPLO ADICIONAL: Modo desconectado explícito (como en tu imagen)
        public static DataTable EjemploModoDesconectadoExplicito()
        {
            DataTable dataTable = new DataTable();

            try
            {
                // Crear conexión con cadena específica
                SqlConnection sqlConnection = new SqlConnection("Data Source=HUG0\\SQLEXPRESS01;" +
                    "Initial Catalog=Neptuno;" +
                    "Integrated Security=True;" +
                    "TrustServerCertificate=true");

                // Abrir conexión
                sqlConnection.Open();

                // Crear comando
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM proveedores", sqlConnection);

                // Crear adaptador
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                // Llenar DataTable
                sqlDataAdapter.Fill(dataTable);

                // CERRAR CONEXIÓN EXPLÍCITAMENTE
                sqlConnection.Close();

                // DataTable es una tabla en memoria y trabaja desconectado
                // Aquí puedes usar dataTable sin conexión activa
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return dataTable;
        }

        // Función adicional: Conectar y desconectar explícitamente
        public static void TestConnection()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    Console.WriteLine("Intentando conectar a la base de datos...");
                    connection.Open();
                    Console.WriteLine("✓ Conexión establecida exitosamente.");

                    // La conexión se cierra automáticamente al salir del using
                    Console.WriteLine("✓ Conexión cerrada.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error de conexión: {ex.Message}");
            }
        }
    }
}