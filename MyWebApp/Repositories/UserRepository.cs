using Microsoft.EntityFrameworkCore;
using MyWebApp.Controllers;
using MyWebApp.DTO.User.Request;
using MyWebApp.Interface;
using MyWebApp.Models;

namespace MyWebApp.Repositories
{
    public class UserRepository : IUserService
    {
        private readonly ILogger<UserRepository> _logger;

        private readonly DBContext _dbContext;

        public UserRepository(DBContext dbContext, ILogger<UserRepository> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        //public method
        public List<UserModel> GetAllUsers()
        {
            try
            {
                var pagiList = _dbContext.Users.Where(item=>item.IsDeleted == false).ToList();

                return pagiList;
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception("getAllUserWithFilters");
            }
        }

        public UserModel? GetUserByUserName(string userName)
        {
            try
            {
               
               var getUser = from user in _dbContext.Users
                          where user.UserName == userName
                             select user;
               if (!getUser.Any()) return null;
               return getUser.First();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception("getUserByUserName");
            }
        }

        public UserModel? GetUserByLogin(RequestUserLoginDTO requestUserDTO)
        {
            try
            {
               var getUser = from user in _dbContext.Users
                             where user.UserName == requestUserDTO.UserName &&
                                   user.Password == requestUserDTO.UserPassword
                             select user;
               if (!getUser.Any()) return null;
               return getUser.First();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception("getUserByLogin");
            }
        }

        public UserModel? RegisterNewUser(RequestRegisterUserDTO requestUserDTO)
        {
            try
            {
                UserModel userModel = new UserModel()
                {
                    UserName = requestUserDTO.UserName,
                    FullName = requestUserDTO.FullName,
                    Email = requestUserDTO.Email,
                    Password = requestUserDTO.UserPassword,
                };

                _dbContext.Users.Add(userModel);

                _dbContext.SaveChanges();

                return userModel;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception("RegisterNewUser");
            }
        }

        public UserModel? CheckUserByID(int userId)
        {
            try
            {
                var getUser = _dbContext.Users.Find(userId);
                return getUser;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception("CheckUserByID");
            }
        }

        public bool DeleteUser(UserModel user)
        {
            try
            {
                user.IsDeleted = true;

                var getUser = _dbContext.Users.Update(user);

                _dbContext.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception("CheckUserByID");
            }
        }

        public bool UpdateUser(UserModel user, RequestUpdateDto requestUpdateDto)
        {
            try
            {
                user.FullName = requestUpdateDto.FullName;
                user.Email = requestUpdateDto.Email;

                var getUser = _dbContext.Users.Update(user);

                _dbContext.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception("CheckUserByID");
            }
        }

        public async Task<UserModel?> GetUserByUserIdAsync(int userId)
        {
            try
            {
               var getUser = await _dbContext.Users.FindAsync(userId);
               return getUser;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception("getUserByUserId");
            }
        }

        public async Task<UserModel?> GetDefaultUser()
        {
            try
            {
                var getUser = await _dbContext.Users.FirstOrDefaultAsync();
                return getUser;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception("getUserByUserId");
            }
        }
    }
}
