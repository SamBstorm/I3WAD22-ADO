using I3WAD22_ADO_Exo_Console.Models;
using System;
using System.Data.SqlClient;

namespace I3WAD22_ADO_Exo_Console
{
    class Program
    {
        private static string _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Theatre-DB;Integrated Security=True"; 
        static void Main(string[] args)
        {
            Spectacle rj = new Spectacle() { nom = "Roméo & Juliette", description = "Pièce de William Shakespeare." };

            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @$"INSERT INTO 
                                                [Spectacle]([nom],[description])
                                             OUTPUT [inserted].[idSpectacle]
                                             VALUES
                                                ('{rj.nom.Replace("'","''")}', '{rj.description.Replace("'","''")}')";
                    connection.Open();
                    rj.idSpectacle = (int)command.ExecuteScalar();
                    connection.Close();
                }
                Representation r1 = new Representation() {
                    dateRepresentation = new DateTime(2022, 12, 24),
                    heureRepresentation = new DateTime(1,1,1,18,0,0),
                    idSpectacle = rj.idSpectacle
                };
                Representation r2 = new Representation()
                {
                    dateRepresentation = new DateTime(2022, 12, 31),
                    heureRepresentation = new DateTime(1, 1, 1, 16, 0, 0),
                    idSpectacle = rj.idSpectacle
                };
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @$"INSERT INTO
                                                [Representation](dateRepresentation, heureRepresentation, idSpectacle)
                                             VALUES
                                                ('{r1.dateRepresentation.ToString("yyyy-MM-dd")}', '{r1.heureRepresentation.TimeOfDay}', {r1.idSpectacle}),
                                                ('{r2.dateRepresentation.ToString("yyyy-MM-dd")}', '{r2.heureRepresentation.TimeOfDay}', {r2.idSpectacle})";
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                Spectacle inauguration = new Spectacle() { nom = "Inauguration", description = "Réception d'inauguration seulement sur invitation" };

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $@"UPDATE [Spectacle]
                                             SET [description] = '{inauguration.description.Replace("'","''")}'
                                             OUTPUT [inserted].[idSpectacle]
                                             WHERE [nom]='{inauguration.nom}'";
                    connection.Open();
                    inauguration.idSpectacle = (int)command.ExecuteScalar();
                    connection.Close();
                }
                Console.WriteLine($"Le spectacle : '{inauguration.nom}' (identifiant : {inauguration.idSpectacle}) a été mis à jour.");

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $@"DELETE FROM [Representation]
                                             WHERE [idSpectacle] = {inauguration.idSpectacle}";
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
    }
}
