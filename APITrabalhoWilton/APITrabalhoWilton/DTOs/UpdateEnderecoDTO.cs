namespace APITrabalhoWilton.DTOs
{
    public record UpdateEnderecoDTO : EnderecoDTO
    {
        public Guid Id { get; set; }
    }
}
