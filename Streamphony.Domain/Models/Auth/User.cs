using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Streamphony.Domain.Models
{
    public class User : IdentityUser<Guid>
    {
        private string _artistName;

        public string ArtistName
        {
            get => _artistName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("ArtistName cannot be null or empty.");
                }
                _artistName = value;
            }
        }
        public string ProfilePictureUrl { get; set; }

        public ICollection<Song> UploadedSongs { get; set; }
        public ICollection<Album> Albums { get; set; }

        public void AddUploadedSong(Song song)
        {
            if (song == null) throw new ArgumentNullException(nameof(song));
            // Additional business logic can be enforced here
            UploadedSongs.Add(song);
        }

        public void RemoveUploadedSong(Song song)
        {
            if (song == null) throw new ArgumentNullException(nameof(song));
            // Additional business logic can be enforced here
            UploadedSongs.Remove(song);
        }

        public void AddAlbum(Album album)
        {
            if (album == null) throw new ArgumentNullException(nameof(album));
            // Additional business logic can be enforced here
            Albums.Add(album);
        }

        public void RemoveAlbum(Album album)
        {
            if (album == null) throw new ArgumentNullException(nameof(album));
            // Additional business logic can be enforced here
            Albums.Remove(album);
        }
    }
}
