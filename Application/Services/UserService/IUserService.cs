namespace Application.Services.UserService;

public interface IUserService
{
    Task CheckUserExistById(int userId);
}
