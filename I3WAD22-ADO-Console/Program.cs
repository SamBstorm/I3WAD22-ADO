using System;
using System.Data.SqlClient;

namespace I3WAD22_ADO_Console
{
    class Program
    {
        static string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Theatre-DB;Integrated Security=True";
        static void Main(string[] args)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    //Paramétrer la commande SQL...
                    command.CommandText = "SELECT [nom] FROM [Type] WHERE [idType] = 1";


                    //Ensuite ouvrir connection
                    connection.Open();

                    //Enfin exécuter la commande SQL

                    string ticketTypeName = (string)command.ExecuteScalar();

                    Console.WriteLine(ticketTypeName);
                }
            }
        }
    }
}
