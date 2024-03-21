using System;

namespace Streamphony.Domain.Models
{
    public class ListeningHistory : BaseEntity
    {
        public Guid UserId { get; init; }
        public User User { get; set; }
        public Guid SongId { get; init; }
        public Song Song { get; set; }
        public int ListenCount { get; set; }
        public DateTime LastPlayedAt { get; set; }

        public void RecordPlay()
        {
            ListenCount++;
            LastPlayedAt = DateTime.UtcNow;
        }
    }
}
