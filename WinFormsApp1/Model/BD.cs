﻿using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace WinFormsApp1.Model
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
                EsborrarDades(); // Borra los datos existentes antes de generar nuevos datos

                if (Conectar())
                {
                    foreach (var cotxe in cotxes)
                    {
                        int idCotxe = InsertarCotxe(cotxe);

                        foreach (var caracteristica in cotxe.Caracteristiques)
                        {
                            InsertarCaracteristica(caracteristica, idCotxe);
                            InsertarDatosEnTablaCotxe(cotxe, caracteristica);
                        }
                    }

                    Console.WriteLine("Datos generados correctamente.");
                    return true;
                }
                else
                {
                    Console.WriteLine("No se puede iniciar la base de datos.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al generar los datos: " + ex.Message);
                return false;
            }
            finally
            {
                Desconectar();
            }
        }

        private static int InsertarCotxe(CotxesModel cotxe)
        {
            int insertedID = -1;
            try
            {
                string query = "INSERT INTO cotxes (marca, model, any) VALUES (@marca, @model, @any);";
                string selectQuery = "SELECT LAST_INSERT_ID();";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@marca", cotxe.Marca);
                    command.Parameters.AddWithValue("@model", cotxe.Model);
                    command.Parameters.AddWithValue("@any", cotxe.Any);

                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine("Registros insertados en cotxes: " + rowsAffected);

                    using (MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection))
                    {
                        insertedID = Convert.ToInt32(selectCommand.ExecuteScalar());
                        Console.WriteLine("Registre insertat amb ID en cotxes: " + insertedID);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar el cotxe en la tabla cotxes: " + ex.Message);
            }
            return insertedID;
        }

        private static void InsertarCaracteristica(CaracteristicaModel caracteristica, int idCotxe)
        {
            try
            {
                string query = "INSERT INTO caracteristiques (cotxe_id, tipus, valor) VALUES (@idCotxe, @tipo, @valor);";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idCotxe", idCotxe);
                    command.Parameters.AddWithValue("@tipo", caracteristica.Tipus);
                    command.Parameters.AddWithValue("@valor", caracteristica.Valor);

                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine("Registros insertados en caracteristiques: " + rowsAffected);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar la característica en la tabla caracteristiques: " + ex.Message);
            }
        }

        private static void InsertarDatosEnTablaCotxe(CotxesModel cotxe, CaracteristicaModel caracteristica)
        {
            try
            {
                int insertedID = -1;

                string cotxeQuery = "INSERT INTO cotxes (marca, model, any) VALUES (@marca, @model, @any);";
                string selectQuery = "SELECT LAST_INSERT_ID();";
                string caracteristicaQuery = "INSERT INTO caracteristiques (cotxe_id, tipus, valor) VALUES (@idCotxe, @tipo, @valor);";
                string createTablaCotxeQuery = "CREATE TABLE IF NOT EXISTS tabla_cotxe (id INT PRIMARY KEY AUTO_INCREMENT, marca VARCHAR(255), model VARCHAR(255), any INT, tipus VARCHAR(255), valor VARCHAR(255));";
                string insertIntoTablaCotxeQuery = "INSERT INTO tabla_cotxe (marca, model, any, tipus, valor) VALUES (@marca, @model, @any, @tipo, @valor);";

                using (MySqlCommand command = new MySqlCommand(createTablaCotxeQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                using (MySqlCommand command = new MySqlCommand(cotxeQuery, connection))
                {
                    command.Parameters.AddWithValue("@marca", cotxe.Marca);
                    command.Parameters.AddWithValue("@model", cotxe.Model);
                    command.Parameters.AddWithValue("@any", cotxe.Any);

                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine("Registros insertados en cotxes: " + rowsAffected);

                    using (MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection))
                    {
                        insertedID = Convert.ToInt32(selectCommand.ExecuteScalar());
                        Console.WriteLine("Registre insertat amb ID en cotxes: " + insertedID);
                    }
                }

                using (MySqlCommand command = new MySqlCommand(caracteristicaQuery, connection))
                {
                    command.Parameters.AddWithValue("@idCotxe", insertedID);
                    command.Parameters.AddWithValue("@tipo", caracteristica.Tipus);
                    command.Parameters.AddWithValue("@valor", caracteristica.Valor);

                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine("Registros insertados en caracteristiques: " + rowsAffected);
                }

                using (MySqlCommand command = new MySqlCommand(insertIntoTablaCotxeQuery, connection))
                {
                    command.Parameters.AddWithValue("@marca", cotxe.Marca);
                    command.Parameters.AddWithValue("@model", cotxe.Model);
                    command.Parameters.AddWithValue("@any", cotxe.Any);
                    command.Parameters.AddWithValue("@tipo", caracteristica.Tipus);
                    command.Parameters.AddWithValue("@valor", caracteristica.Valor);

                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine("Registros insertados en tabla_cotxe: " + rowsAffected);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar los datos en las tablas: " + ex.Message);
            }
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

                    Console.WriteLine("Deleted {0} rows from the tables.", rowsAffected);
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
