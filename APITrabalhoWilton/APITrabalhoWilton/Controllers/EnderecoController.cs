using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using APITrabalhoWilton.DataBase;
using System.Linq;
using APITrabalhoWilton.Models;
using Newtonsoft.Json.Linq;
using APITrabalhoWilton.DTOs;
using Newtonsoft.Json;

namespace APITrabalhoWilton.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    public class EnderecoController : ControllerBase
    {

        private readonly DatabaseEndereco databaseEndereco;

        public EnderecoController(DatabaseEndereco databaseEndereco)
        {
                this.databaseEndereco = databaseEndereco;
        }

        [HttpGet("EnderecoDatabase")]
        public IEnumerable<GetEnderecoDatabaseDTO> GetEnderecoDatabase()
        {
            var listaEnderecos = databaseEndereco.GetData();

            List<GetEnderecoDatabaseDTO> listaEnderecosDTO = new List<GetEnderecoDatabaseDTO>();
            foreach (var endereco in listaEnderecos)
            {
                var enderecoDTO = new GetEnderecoDatabaseDTO
                {
                    Id = endereco.Id,
                    Cep = endereco.Cep,
                    Logradouro = endereco.Logradouro,
                    Complemento = endereco.Complemento,
                    Numero = endereco.Numero,
                    Bairro = endereco.Bairro,
                    Cidade = endereco.Cidade,
                    UF = endereco.UF
                };

                listaEnderecosDTO.Add(enderecoDTO);
            }

            return listaEnderecosDTO; 
        }
        
        // Método para obter o endereço utilizando Api externa ViaCep
        [HttpGet("EnderecoViaCep/{cep}")]
        public async Task<ActionResult<GetEnderecoViaCepDTO>> GetEnderecoViaCep(string cep)
        {

            Uri uri = new Uri($"https://viacep.com.br/ws/{cep}/json/");

            HttpClient client = new HttpClient();
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var endereco = JsonConvert.DeserializeObject<GetEnderecoViaCepDTO>(json);
                
                if (endereco == null)
                {
                    return BadRequest("Erro ao processar resposta");
                }

                if (endereco.Cep == null)
                {
                    return BadRequest("Erro. Endereço não pode ser nulo.");
                }

                endereco.Cep = endereco.Cep.Replace("-", "");

                return Ok(endereco);
            }
            else
            {
                return NotFound("CEP não encontrado.");
            }

        }

        // Método para obter o endereço via CEP internamente
        private async Task<GetEnderecoViaCepDTO?> GetEnderecoViaCepInterno(string cep)
        {

            Uri uri = new Uri($"https://viacep.com.br/ws/{cep}/json/");

            HttpClient client = new HttpClient();
            
            var response = await client.GetAsync(uri);

            if (!response.IsSuccessStatusCode) {
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            var endereco = JsonConvert.DeserializeObject<GetEnderecoViaCepDTO>(json);
            
            if(endereco == null)
            {
                return null;
            }

            if (endereco.Cep == null)
            {
                return null;
            }

            endereco.Cep = endereco.Cep.Replace("-", "");

            return endereco;
        }

        [HttpPost("PostEndereco")]
        public async Task<IActionResult> PostDatabase([FromBody] PostEnderecoDTO dto)
        {
            var enderecoViaCep = await GetEnderecoViaCepInterno(dto.Cep);
            if (enderecoViaCep == null)
                return NotFound("CEP inválido");

            var endereco = new Endereco
            {
                Cep = enderecoViaCep.Cep,
                Logradouro = enderecoViaCep.Logradouro,
                Complemento = dto.Complemento,
                Numero = dto.Numero,
                Bairro = enderecoViaCep.Bairro,
                Cidade = enderecoViaCep.Localidade,
                UF = enderecoViaCep.UF
            };

            databaseEndereco.PostData(endereco);
            return Ok();
        }

        [HttpDelete("DeleteEndereco/{id}")]
        public void DeleteEndereco(Guid id)
        {
            databaseEndereco.DeleteData(id);
        }

        [HttpPut("PutEndereco/{id}")]
        public async Task<IActionResult> PutEndereco(Guid id,[FromBody] UpdateEnderecoDTO dto)
        {
            var enderecoViaCep = await GetEnderecoViaCepInterno(dto.Cep);

            if (enderecoViaCep == null)
            {
                return NotFound("Endereço não encontrado");
            }

            var endereco = new Endereco
            {
                Id = id,
                Cep = enderecoViaCep.Cep,
                Logradouro = enderecoViaCep.Logradouro,
                Complemento = enderecoViaCep.Complemento,
                Numero = enderecoViaCep.Numero,
                Bairro = enderecoViaCep.Bairro,
                Cidade = enderecoViaCep.Localidade,
                UF = enderecoViaCep.UF
            };

            databaseEndereco.UpdateData(endereco);

            return Ok("Endereço atualizado com sucesso.");
        }
    }
}
