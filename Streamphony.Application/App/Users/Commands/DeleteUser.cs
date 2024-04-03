using MediatR;
using Streamphony.Application.Interfaces;
using Streamphony.Application.Interfaces.Repositories;
using Streamphony.Domain.Models;

namespace Streamphony.Application.App.Users.Commands;

public record DeleteUser(Guid Id) : IRequest<bool>;

public class DeleteUserHandler(IRepository<User> repository, ILoggingService loggingService) : IRequestHandler<DeleteUser, bool>
{
    private readonly IRepository<User> _repository = repository;
    private readonly ILoggingService _loggingService = loggingService;

    public async Task<bool> Handle(DeleteUser request, CancellationToken cancellationToken)
    {
        Guid id = request.Id;
        try
        {
            var userToDelete = await _repository.GetById(id);
            if (userToDelete == null) return false;

            await _repository.Delete(id);
            await _repository.SaveChangesAsync();
            await _loggingService.LogAsync($"User id {id} - deleted successfully");

            return true;
        }
        catch (Exception ex)
        {
            await _loggingService.LogAsync($"Error deleting user id {id}: {ex.Message}");
            throw;
        }
    }
}