using Application.Features.Users.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;

namespace Application.Services.UserService;

public class UserManager : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserManager(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task CheckUserExistById(int userId)
    {
        bool result = await _userRepository.AnyAsync(predicate: user => user.Id == userId);

        if (!result)
            throw new BusinessException(UsersMessages.UserIdAssociatedUserNotFound);
    }
}
