using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using APITrabalhoWilton.DataBase;
using System.Linq;
using APITrabalhoWilton.Models;
using Newtonsoft.Json.Linq;
using APITrabalhoWilton.DTOs;

namespace APITrabalhoWilton.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    public class EnderecoController : ControllerBase
    {
        
        [HttpGet("EnderecoDatabase", Name = "GetEnderecoDatabase")]
        public IEnumerable<GetEnderecoDatabaseDTO> GetEnderecoDatabase()
        {

            var database = new DatabaseEndereco();
            var listaEnderecos = database.GetData();

            return listaEnderecos;
        }

        [HttpGet("EnderecoViaCep", Name = "GetEnderecoViaCep")]
        public GetEnderecoViaCepDTO GetEnderecoViaCep(string cep)
        {

            Uri uri = new Uri($"https://viacep.com.br/ws/{cep}/json/");

            HttpClient client = new HttpClient();
            var response = client.GetAsync(uri).Result;

            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var endereco = Newtonsoft.Json.JsonConvert.DeserializeObject<GetEnderecoViaCepDTO>(json);
                
                if (endereco == null)
                {
                    return null;
                }

                if (endereco.Cep != null)
                {
                    if (endereco.Cep.Contains("-"))
                    {
                        endereco.Cep = endereco.Cep.Replace("-", "");
                    }
                }

                return endereco;
            }
            else
            {
                return null;
            }

        }
        

        [HttpPost(Name = "PostEndereco")]
        public void PostDatabase(string cep, string complemento, string numero)
        {
            var enderecoGet = GetEnderecoViaCep(cep);

            if (enderecoGet == null)
            {
                return;
            }
            
            var endereco = new EnderecoDTO
            {
                Cep = enderecoGet.Cep,
                Logradouro = enderecoGet.Logradouro,
                Complemento = complemento,
                Numero = numero,
                Bairro = enderecoGet.Bairro,
                Cidade = enderecoGet.Localidade,
                UF = enderecoGet.UF
            };

            var database = new DatabaseEndereco();

            database.PostData(endereco);
        }

        [HttpDelete(Name = "DeleteEndereco")]
        public void DeleteEndereco(Guid id)
        {
            var database = new DatabaseEndereco();
            database.DeleteData(id);
        }

        [HttpPut(Name = "PutEndereco")]
        public void PutEndereco(Guid id, string cep, string complemento, string numero)
        {
            var enderecoGet = GetEnderecoViaCep(cep);
            if (enderecoGet == null)
            {
                return;
            }
            var endereco = new UpdateEnderecoDTO
            {
                Id = id,
                Cep = enderecoGet.Cep,
                Logradouro = enderecoGet.Logradouro,
                Complemento = complemento,
                Numero = numero,
                Bairro = enderecoGet.Bairro,
                Cidade = enderecoGet.Localidade,
                UF = enderecoGet.UF
            };
            var database = new DatabaseEndereco();
            database.UpdateData(endereco);
        }
    }
}
