namespace Streamphony.Domain.Models;

public class Genre : BaseEntity
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public ICollection<Song> Songs { get; set; } = new HashSet<Song>();
}
