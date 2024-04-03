using System.IO.Abstractions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Streamphony.Application.App.Songs.Commands;
using Streamphony.Application.App.Songs.Queries;
using Streamphony.Application.App.Songs.Responses;
using Streamphony.Application.App.Users.Commands;
using Streamphony.Application.App.Users.Queries;
using Streamphony.Application.App.Users.Responses;
using Streamphony.Application.Interfaces;
using Streamphony.Application.Interfaces.Repositories;
using Streamphony.Application.Logging;
using Streamphony.Domain.Models;
using Streamphony.Infrastructure.Logging;
using Streamphony.Infrastructure.Persistence.Repositories;

namespace Streamphony.ConsolePresentation
{
    public class Program
    {
        public static async Task Main()
        {
            var diContainer = new ServiceCollection()
                .AddSingleton<IFileSystem, FileSystem>()
                .AddSingleton<IDateTimeProvider, DateTimeProvider>()
                .AddScoped<ILoggingService, FileLoggingService>()
                .AddScoped<IRepository<Song>, InMemoryRepository<Song>>()
                .AddScoped<IRepository<User>, InMemoryRepository<User>>()
                .AddAutoMapper(typeof(IRepository<Song>).Assembly)
                .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ISongRepository).Assembly))
                .BuildServiceProvider();

            var mediator = diContainer.GetRequiredService<IMediator>();

            var userDto = new UserDto { Email = "jd@gmail.com", Username = "jdname", ArtistName = "JasonD" };
            var user = await mediator.Send(new CreateUser(userDto));

            var songDto = new SongDto { Title = "Yellow", Duration = new TimeSpan(0, 3, 17), OwnerId = user.Id, Url = "fileupload.com/song1.mp3" };
            var song2Dto = new SongDto { Title = "Blue", Duration = new TimeSpan(0, 7, 11), OwnerId = user.Id, Url = "fileupload.com/song2.mp3" };
            var newSong = await mediator.Send(new CreateSong(songDto));
            var song = await mediator.Send(new GetSongById(newSong.Id));

            Console.WriteLine($"Song: {song.Title}, Duration: {song.Duration}, Owner: {song.OwnerId}");

            var userUpdated = await mediator.Send(new GetUserById(user.Id));
            PrintUser(userUpdated);

            var newSong2 = await mediator.Send(new CreateSong(song2Dto));
            userUpdated = await mediator.Send(new GetUserById(user.Id));
            PrintUser(userUpdated);
        }

        private static void PrintUser(UserDto user)
        {
            Console.WriteLine($"\nUser: {user.ArtistName}, Id: {user.Id}");
            Console.WriteLine($"Uploaded songs: {user.UploadedSongs?.Count}");
            foreach (var uploadedSong in user.UploadedSongs!)
            {
                Console.WriteLine($"Song: {uploadedSong.Title}");
                Console.WriteLine($"\tDuration: {uploadedSong.Duration}");
                Console.WriteLine($"\tId: {uploadedSong.Id}");
            }
        }
    }
}