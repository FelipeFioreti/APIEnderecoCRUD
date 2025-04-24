namespace APITrabalhoWilton.DTOs
{
    public record GetEnderecoDatabaseDTO : EnderecoDTO
    {
        public Guid Id { get; set; }
    }
}
