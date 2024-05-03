using Microsoft.EntityFrameworkCore;
using Streamphony.Application.Abstractions.Repositories;
using Streamphony.Domain.Models;
using Streamphony.Infrastructure.Persistence.Contexts;

namespace Streamphony.Infrastructure.Persistence.Repositories;

public class GenreRepository(ApplicationDbContext context) : Repository<Genre>(context), IGenreRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Genre?> GetByName(string name, CancellationToken cancellationToken)
    {
        return await _context.Genres.FirstOrDefaultAsync(genre => genre.Name == name, cancellationToken);
    }
}