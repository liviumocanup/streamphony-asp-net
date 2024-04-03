namespace Streamphony.Application.DTOs
{
    public class GenreDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}