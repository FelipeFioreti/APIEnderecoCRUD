namespace APITrabalhoWilton.DTOs
{
    public record PostEnderecoDTO
    {
        public string Cep { get; set; } = string.Empty;
        public string Complemento { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
    }
}
