using AutoMapper;
using Streamphony.Domain.Models;
using Streamphony.Application.Interfaces;
using Streamphony.Application.Interfaces.Repositories;
using Streamphony.Application.DTOs;

namespace Streamphony.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILoggingService _loggingService;

        public UserService(IRepository repository, IMapper mapper, ILoggingService loggingService)
        {
            _repository = repository;
            _mapper = mapper;
            _loggingService = loggingService;
        }

        public async Task<UserDto> CreateUserAsync(UserDto userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);

                _repository.Add(user);

                await _repository.SaveChangesAsync();

                await _loggingService.LogAsync($"id {user.Id} - success");

                return _mapper.Map<UserDto>(user);
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                await _loggingService.LogAsync($"id {userDto.Id} - failure: {errorMessage}");
                throw;
            }
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _repository.GetAll<User>();

            await _loggingService.LogAsync($"success");

            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetUserByIdAsync(Guid id)
        {
            var user = await _repository.GetById<User>(id);
            if (user != null)
            {
                await _loggingService.LogAsync($"id {id} - success");
            }
            else
            {
                await _loggingService.LogAsync($"id {id} - failure: not found");
            }

            return _mapper.Map<UserDto>(user);
        }

    }
}