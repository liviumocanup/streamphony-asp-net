public class CommentDto
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public Guid UserId { get; set; }
    public Guid SongId { get; set; }
}
