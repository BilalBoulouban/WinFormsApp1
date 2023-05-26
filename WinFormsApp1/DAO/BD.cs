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

        public static bool InsertarCotxe(CotxesModel cotxe)
        {
            try
            {
                if (Conectar())
                {
                    string query = "INSERT INTO cotxes (marca, model, any) VALUES (@marca, @model, @any);";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@marca", cotxe.Marca);
                        command.Parameters.AddWithValue("@model", cotxe.Model);
                        command.Parameters.AddWithValue("@any", cotxe.Any);

                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine("Registros insertados en cotxes: " + rowsAffected);

                        foreach (CaracteristicaModel caracteristica in cotxe.Caracteristiques)
                        {
                            InsertarCaracteristica(caracteristica, cotxe.Marca, cotxe.Model);
                        }

                        Console.WriteLine("Cotxe insertado correctamente.");
                        return true;
                    }
                }
                else
                {
                    Console.WriteLine("No se puede iniciar la base de datos.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar el cotxe en la tabla cotxes: " + ex.Message);
                return false;
            }
            finally
            {
                Desconectar();
            }
        }

        private static void InsertarCaracteristica(CaracteristicaModel caracteristica, string marca, string model)
        {
            try
            {
                if (Conectar())
                {
                    string query = "INSERT INTO caracteristiques (marca, model, tipus, valor) VALUES (@marca, @model, @tipo, @valor);";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@marca", marca);
                        command.Parameters.AddWithValue("@model", model);
                        command.Parameters.AddWithValue("@tipo", caracteristica.Tipus);
                        command.Parameters.AddWithValue("@valor", caracteristica.Valor);

                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine("Registros insertados en caracteristiques: " + rowsAffected);
                    }
                }
                else
                {
                    Console.WriteLine("No se puede iniciar la base de datos.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar la característica en la tabla caracteristiques: " + ex.Message);
            }
            finally
            {
                Desconectar();
            }
        }

        public static int EsborrarDades()
        {
            int rowsAffected = 0;
            try
            {
                if (Conectar())
                {
                    string tableName = "tabla_cotxes";
                    string deleteQuery = "DELETE FROM " + tableName;

                    using (MySqlCommand command = new MySqlCommand(deleteQuery, connection))
                    {
                        command.CommandType = CommandType.Text;
                        try
                        {
                            rowsAffected = command.ExecuteNonQuery();
                            Console.WriteLine("Deleted {0} rows from the table {1}.", rowsAffected, tableName);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error deleting records: " + ex.Message);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No se puede iniciar la base de datos.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al conectar con la base de datos: " + ex.Message);
            }
            finally
            {
                Desconectar();
            }
            return rowsAffected;
        }
    }
}
