using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using WinFormsApp1.Model;

namespace WinFormsApp1.DAO
{
    public static class BD
    {
        private const string server = "db4free.net";
        private const string port = "3306";
        private const string database = "cotxesbb";
        private const string username = "bilalbb";
        private const string password = "8r#P06%1o7Il";

        private const string connectionString = "Server=" + server + ";Port=" + port + ";Database=" + database + ";Username=" + username + ";Password=" + password;
        private static MySqlConnection connection;

        private static bool Conectar()
        {
            connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        private static void Desconectar()
        {
            if (connection != null)
            {
                connection.Close();
            }
        }

        public static bool EnviarDatosBDD(List<CotxesModel> cotxes)
        {
            try
            {
                EsborrarDades();

                if (Conectar())
                {
                    foreach (var cotxe in cotxes)
                    {
                        int idCotxe = InsertarCotxe(cotxe);

                        foreach (var caracteristica in cotxe.Caracteristiques)
                        {
                            InsertarCaracteristica(caracteristica, idCotxe);
                        }
                        InsertarConcessionari(cotxe.Concessionari);
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                Desconectar();
            }
        }

        public static int InsertarCotxe(CotxesModel cotxe)
        {
            int insertedID = -1;
            try
            {
                Conectar();
                string query = "INSERT INTO cotxes (marca, model, any) VALUES (@marca, @model, @any);";
                string selectQuery = "SELECT LAST_INSERT_ID();";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@marca", cotxe.Marca);
                    command.Parameters.AddWithValue("@model", cotxe.Model);
                    command.Parameters.AddWithValue("@any", cotxe.Any);

                    int rowsAffected = command.ExecuteNonQuery();

                    using (MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection))
                    {
                        insertedID = Convert.ToInt32(selectCommand.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return insertedID;
        }

        public static void InsertarCaracteristica(CaracteristicaModel caracteristica, int idCotxe)
        {
            try
            {
                Conectar();
                string query = "INSERT INTO caracteristiques (cotxe_id, tipus, valor) VALUES (@idCotxe, @tipo, @valor);";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idCotxe", idCotxe);
                    command.Parameters.AddWithValue("@tipo", caracteristica.Tipus);
                    command.Parameters.AddWithValue("@valor", caracteristica.Valor);

                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static int InsertarConcessionari(ConcessionariModel concessionari)
        {
            int insertedID = -1;
            try
            {
                Conectar();

                string query = "INSERT INTO concessionaris (nom, carrer, ciutat, codiPostal, telefon, dilluns, dimarts, dimecres, dijous, divendres, dissabte, diumenge) " +
                    "VALUES (@nom, @carrer, @ciutat, @codiPostal, @telefon, @dilluns, @dimarts, @dimecres, @dijous, @divendres, @dissabte, @diumenge);";
                string selectQuery = "SELECT LAST_INSERT_ID();";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nom", concessionari.Nom);
                    command.Parameters.AddWithValue("@carrer", concessionari.Carrer);
                    command.Parameters.AddWithValue("@ciutat", concessionari.Ciutat);
                    command.Parameters.AddWithValue("@codiPostal", concessionari.CodiPostal);
                    command.Parameters.AddWithValue("@telefon", concessionari.Telefon);
                    command.Parameters.AddWithValue("@dilluns", concessionari.Dilluns);
                    command.Parameters.AddWithValue("@dimarts", concessionari.Dimarts);
                    command.Parameters.AddWithValue("@dimecres", concessionari.Dimecres);
                    command.Parameters.AddWithValue("@dijous", concessionari.Dijous);
                    command.Parameters.AddWithValue("@divendres", concessionari.Divendres);
                    command.Parameters.AddWithValue("@dissabte", concessionari.Dissabte);
                    command.Parameters.AddWithValue("@diumenge", concessionari.Diumenge);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        using (MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection))
                        {
                            insertedID = Convert.ToInt32(selectCommand.ExecuteScalar());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                Desconectar();
            }

            return insertedID;
        }



        public static int EsborrarDades()
        {
            int rowsAffected = 0;
            try
            {
                if (Conectar())
                {
                    string deleteCotxesQuery = "DELETE FROM cotxes";
                    string deleteCaracteristiquesQuery = "DELETE FROM caracteristiques";
                    string deleteTablaCotxesQuery = "DELETE FROM tabla_cotxes";

                    using (MySqlCommand command = new MySqlCommand(deleteCotxesQuery, connection))
                    {
                        command.CommandType = CommandType.Text;
                        rowsAffected += command.ExecuteNonQuery();
                    }

                    using (MySqlCommand command = new MySqlCommand(deleteCaracteristiquesQuery, connection))
                    {
                        command.CommandType = CommandType.Text;
                        rowsAffected += command.ExecuteNonQuery();
                    }

                    using (MySqlCommand command = new MySqlCommand(deleteTablaCotxesQuery, connection))
                    {
                        command.CommandType = CommandType.Text;
                        rowsAffected += command.ExecuteNonQuery();
                    }

                }
                else
                {
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                Desconectar();
            }
            return rowsAffected;
        }
    }
}
