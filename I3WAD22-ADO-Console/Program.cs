using I3WAD22_ADO_Console.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace I3WAD22_ADO_Console
{
    class Program
    {
        static string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Theatre-DB;Integrated Security=True";
        static void Main(string[] args)
        {
            //Pas nécessaire pour ADO.net mais permet d'avoir la console en unicode (pour les € par exemple)
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            //Spectacle sp1 = new Spectacle();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                #region Mode Connecté (J'ai possibilité de contacté mon serveur)
                #region ExecuteScalar() => Récupération d'une seule information
                //using (SqlCommand command = connection.CreateCommand())
                //{
                //    //Paramétrer la commande SQL...
                //    command.CommandText = "SELECT [prix] FROM [Type] WHERE [idType] = 1";


                //    //Ensuite ouvrir connection
                //    connection.Open();

                //    //Enfin exécuter la commande SQL

                //    decimal ticketTypePrice = (decimal)command.ExecuteScalar();

                //    Console.WriteLine(ticketTypePrice);
                //} 
                #endregion
                #region ExecuteReader => Récupération de multiple informations (colones ou lignes ou les deux)
                //using (SqlCommand command = connection.CreateCommand())
                //{
                //    //Prépare la commande SQL
                //    command.CommandText = "SELECT * FROM [type]";

                //    //Exécution de la commande SQL
                //    connection.Open();
                //    using (SqlDataReader reader = command.ExecuteReader())
                //    {
                //        while (reader.Read())
                //        {
                //            Console.WriteLine($"{reader["idType"]}. {reader["nom"]} : {reader["Prix"]} €");

                //            //OU

                //            //Console.WriteLine($"{reader[0]}. {reader[1]} : {reader[2]} €");
                //        }
                //    }
                //}
                #endregion
                #region Exo ExecuteReader sur Spectacle
                /*
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM [Spectacle]";
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sp1.idSpectacle = (int)reader[nameof(sp1.idSpectacle)];
                            sp1.nom = (string)reader[nameof(sp1.nom)];
                            sp1.description = (string)reader[nameof(sp1.description)];
                        }
                    }
                }
                */
                #endregion
                #endregion
                #region Mode Déconnecté (Je sais que je risque de perdre la connection - exemple : en mobile - je charge le tout avant utilisation)
                //using (SqlCommand command = connection.CreateCommand())
                //{
                //    //Prépare la commande
                //    command.CommandText = "SELECT * FROM [Type]";

                //    //Prépare adaptateur pour remplir mon DataSet ou DataTable
                //    SqlDataAdapter adapter = new SqlDataAdapter(command);

                //    //Prépare DataSet ou DataTable pour récupérer les données
                //    DataTable table_type = new DataTable();

                //    //Demande à l'adaptateur de remplir le DataTable/DataSet grâce à la commande
                //    adapter.Fill(table_type);   //la méthode Fill() établi lui-même la connection
                //                                //(Pas besoin d'Open(), Close(), ...)

                //    //Je manipule le DataSet/DataTable grâce à ses propriétés (Tables, Rows, Columns, ... )
                //    foreach (DataRow row in table_type.Rows)
                //    {
                //        Console.WriteLine($"{row["idType"]}. {row["Nom"]} : {row["Prix"]}€");
                //    }
                //}

                #endregion

                #region Ordres DML (toujours en Mode Connecté)
                #region INSERT avec ExecuteNonQuery (Récupération nombre de lignes affectées)
                /*using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO [Representation] 
                                                ([dateRepresentation],[heureRepresentation],[idSpectacle]) 
                                            VALUES  ('2022-12-20','8:45' ,1),
                                                    ('2022-12-20','10:15',1)";
                    connection.Open();
                    int nbLignes = command.ExecuteNonQuery();
                    Console.WriteLine($"Il y a {nbLignes} nouvelle(s) représentation(s) de l'inauguration...");
                }
                */
                #endregion
                #region UPDATE avec ExecuteScalar (récupération d'information par le OUTPUT SQL)
                /*using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"UPDATE [Representation]
                                            SET [heureRepresentation] = '12:15'
                                            OUTPUT [deleted].[idRepresentation]
                                            WHERE   [idSpectacle] = 1
                                                AND [heureRepresentation] = '8:45'";
                    connection.Open();
                    int? id = (int?)command.ExecuteScalar();    //Possible qu'il n'y aie pas de valeur,
                                                                //si pas de correspondance, donc nullable
                    if (id is null) Console.WriteLine("Aucune mise à jour...");
                    else Console.WriteLine($"La représentation numéro {id} a été reporté à 12h15...");
                }*/
                #endregion
                #region DELETE avec ExecuteReader (récupération de plusieurs informations par le OUTPUT SQL)
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"DELETE FROM [Representation]
                                            OUTPUT  [deleted].[idRepresentation],
                                                    [deleted].[dateRepresentation],
                                                    [deleted].[heureRepresentation]
                                            WHERE [idSpectacle] = 1";
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("Les représentations supprimées sont :");
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["idRepresentation"]} : {reader["dateRepresentation"]} - {reader["heureRepresentation"]}");
                        }
                    }
                }
                #endregion
                #endregion
            }

            //Console.WriteLine($"{sp1.idSpectacle}. {sp1.nom} : {sp1.description}");
        }
    }
}
