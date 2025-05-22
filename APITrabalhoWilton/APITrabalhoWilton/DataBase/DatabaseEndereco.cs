using APITrabalhoWilton.Controllers;
using APITrabalhoWilton.DTOs;
using APITrabalhoWilton.Models;
using Microsoft.Data.SqlClient;

namespace APITrabalhoWilton.DataBase
{
    public class DatabaseEndereco
    {

        public string DatabaseConnection()
        {
            var json = File.ReadAllText("appsettings.json");
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json)!;
            string connectionString = jsonObj!["ConnectionStrings"]["DatabaseConnection"];

            return connectionString;
        }

        public List<Endereco> GetData()
        {
            List<Endereco> enderecos = new List<Endereco>();

            try
            {
                using (SqlConnection con = new SqlConnection(DatabaseConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Endereco", con))
                    {
                        cmd.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var endereco = new Endereco
                                    {
                                        Id = Guid.Parse(reader["Id"].ToString()!),
                                        Cep = reader["Cep"].ToString()!,
                                        Logradouro = reader["Logradouro"].ToString()!,
                                        Complemento = reader["Complemento"].ToString()!,
                                        Numero = reader["Numero"].ToString()!,
                                        Bairro = reader["Bairro"].ToString()!,
                                        Cidade = reader["Cidade"].ToString()!,
                                        UF = reader["UF"].ToString()!
                                    };
                                    enderecos.Add(endereco);
                                }
                            }
                            else
                            {
                                return new List<Endereco>();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return enderecos;
        }

        public void PostData(Endereco endereco)
        {
            try
            {

                using (SqlConnection con = new SqlConnection(DatabaseConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Endereco (Id, Cep, Logradouro, Complemento, Numero, Bairro, Cidade, UF) VALUES (@Id, @Cep, @Logradouro, @Complemento, @Numero, @Bairro, @Cidade, @UF)", con))
                    {
                        cmd.Parameters.AddWithValue("@Id", Guid.NewGuid());
                        cmd.Parameters.AddWithValue("@Cep", endereco.Cep);
                        cmd.Parameters.AddWithValue("@Logradouro", endereco.Logradouro);
                        cmd.Parameters.AddWithValue("@Complemento", endereco.Complemento);
                        cmd.Parameters.AddWithValue("@Numero", endereco.Numero);
                        cmd.Parameters.AddWithValue("@Bairro", endereco.Bairro);
                        cmd.Parameters.AddWithValue("@Cidade", endereco.Cidade);
                        cmd.Parameters.AddWithValue("@UF", endereco.UF);
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void DeleteData(Guid id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(DatabaseConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Endereco WHERE Id = @Id", con))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void UpdateData(Endereco endereco)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(DatabaseConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE Endereco SET Cep = @Cep, Logradouro = @Logradouro, Complemento = @Complemento, Numero = @Numero, Bairro = @Bairro, Cidade = @Cidade, UF = @UF WHERE Id = @Id", con))
                    {
                        cmd.Parameters.AddWithValue("@Id", endereco.Id);
                        cmd.Parameters.AddWithValue("@Cep", endereco.Cep);
                        cmd.Parameters.AddWithValue("@Logradouro", endereco.Logradouro);
                        cmd.Parameters.AddWithValue("@Complemento", endereco.Complemento);
                        cmd.Parameters.AddWithValue("@Numero", endereco.Numero);
                        cmd.Parameters.AddWithValue("@Bairro", endereco.Bairro);
                        cmd.Parameters.AddWithValue("@Cidade", endereco.Cidade);
                        cmd.Parameters.AddWithValue("@UF", endereco.UF);
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
    }
}
