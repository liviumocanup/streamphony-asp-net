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

                await _loggingService.LogAsync($"CreateUserAsync called for {user.Id} - success");

                return _mapper.Map<UserDto>(user);
            }
            catch (Exception ex)
            {
                await _loggingService.LogAsync($"CreateUserAsync called for {userDto.Id} - failure: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _repository.GetAll<User>();

            await _loggingService.LogAsync($"GetAllUsersAsync called - success");

            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetUserByIdAsync(Guid id)
        {
            var user = await _repository.GetById<User>(id);

            await _loggingService.LogAsync($"GetUserByIdAsync called for {id} - success");

            return _mapper.Map<UserDto>(user);
        }
    }
}