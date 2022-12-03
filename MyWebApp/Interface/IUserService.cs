using MyWebApp.DTO.User.Request;
using MyWebApp.Models;

namespace MyWebApp.Interface
{
    public interface IUserService
    {        
        //Users Method
        UserModel? RegisterNewUser(RequestRegisterUserDTO requestUserDTO);
        UserModel? GetUserByUserName(string userId);
        UserModel? GetUserByLogin(RequestUserLoginDTO requestUserDTO);
        UserModel? CheckUserByID(int userId);

        bool DeleteUser(UserModel user);
        bool UpdateUser(UserModel user, RequestUpdateDto requestUpdateDto);

        //List Users
        List<UserModel> GetAllUsers();

        //Aync Method
        Task<UserModel?> GetUserByUserIdAsync(int userID);
        Task<UserModel?> GetDefaultUser();
    }
}
