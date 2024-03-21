using System;
using System.Collections.Generic;

namespace Streamphony.Domain.Models
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Song> Songs { get; set; } = new List<Song>();
    }
}
