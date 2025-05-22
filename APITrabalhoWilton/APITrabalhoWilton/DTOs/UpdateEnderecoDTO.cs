namespace APITrabalhoWilton.DTOs
{
    public record UpdateEnderecoDTO
    {
        public string Cep { get; set; } = String.Empty;
        public string Complemento { get; set; } = String.Empty;
        public string Numero { get; set; } = String.Empty;

    }
}
