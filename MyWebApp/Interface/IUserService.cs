using MyWebApp.DTO.User.Request;
using MyWebApp.Models;

namespace MyWebApp.Interface
{
    public interface IUserService
    {
        List<string> GetListSortOrder();
        
        //Users Method
        UserModel? RegisterNewUser(RequestRegisterUserDTO requestUserDTO);
        UserModel? GetUserByUserName(string userId);
        UserModel? GetUserByLogin(RequestUserLoginDTO requestUserDTO);

        //List Users
        Tuple<int, List<UserModel>> GetAllUsersWithFilters(StateModel stateModel);
        Tuple<int, List<UserModel>> GetAllUsersWithRawFilters(StateModel stateModel);

        //Aync Method
        Task<UserModel?> GetUserByUserIdAsync(int userID);
        Task<UserModel?> GetDefaultUser();
    }
}
