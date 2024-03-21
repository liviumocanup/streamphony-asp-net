using System;

namespace Streamphony.Domain.Models
{
    public class Comment : BaseEntity
    {
        public string Text { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid SongId { get; set; }
        public Song Song { get; set; }
    }
}