﻿using Microsoft.EntityFrameworkCore;
using MyWebApp.Controllers;
using MyWebApp.DTO.User.Request;
using MyWebApp.Interface;
using MyWebApp.Models;

namespace MyWebApp.Repositories
{
    public class UserRepository : IUserService
    {
        private readonly int _perPage = 5;
        private readonly ILogger<UserRepository> _logger;
        private readonly ICustomPagination _pagination;

        private readonly DBContext _dbContext;

        private readonly List<string> _listSortOrder = new List<string>() { "username", "fullname", "email" };
        private readonly List<string> _listQueries = new List<string>() { "user_name", "user_fullname" };
        private readonly List<string> _listSortOrderDatabase = new List<string>() { "user_username", "user_fullname", "user_email" };

        public UserRepository(DBContext dbContext, ILogger<UserRepository> logger, ICustomPagination pagination)
        {
            _logger = logger;
            _dbContext = dbContext;
            _pagination = pagination;
        }

        //private method
        private IQueryable<UserModel> orderList(IQueryable<UserModel> userList, bool isDesc, string sortBy)
        {
            if (sortBy == _listSortOrder[0])
            {
                return isDesc ? userList.OrderByDescending(x => x.UserName) : userList.OrderBy(x => x.UserName);
            }

            if (sortBy == _listSortOrder[1])
            {
                return isDesc ? userList.OrderByDescending(x => x.FullName) : userList.OrderBy(x => x.FullName);
            }

            return isDesc ? userList.OrderByDescending(x => x.Email) : userList.OrderBy(x => x.Email);
        }

        private List<UserModel> getOrderPagiList(IQueryable<UserModel> userList, int currentPage, bool isDesc, string sortBy)
        {
            return orderList(userList, isDesc, sortBy).Skip(currentPage * _perPage).Take(_perPage).ToList();
        }

        //public method
        public List<string> GetListSortOrder()
        {
            return _listSortOrder;
        }

        public Tuple<int,List<UserModel>> GetAllUsersWithFilters(StateModel stateModel)
        {
            try
            {
                int pages = 0;
                var pagiList = _dbContext.Users;

                if (!string.IsNullOrEmpty(stateModel.FilterParam))
                {
                    var filterList = from users in pagiList where  
                                     EF.Functions.Like(users.UserName, string.Format("%{0}%", stateModel.FilterParam)) ||
                                     EF.Functions.Like(users.FullName, string.Format("%{0}%", stateModel.FilterParam))
                                     select users;

                    pages = ((filterList.Count()) % _perPage == 0) ? filterList.Count() / _perPage : filterList.Count() / _perPage + 1;

                    return new Tuple<int, List<UserModel>>(pages, getOrderPagiList(filterList, stateModel.Page, stateModel.IsDesc, stateModel.SortBy));
                }

                pages = ((pagiList.Count()) % _perPage == 0) ? pagiList.Count() / _perPage : pagiList.Count() / _perPage + 1;

                return new Tuple<int, List<UserModel>>(pages, getOrderPagiList(pagiList, stateModel.Page, stateModel.IsDesc, stateModel.SortBy));
            }
            catch
            {
                throw new Exception("getAllUserWithFilters");
            }
        }

        public Tuple<int, List<UserModel>> GetAllUsersWithRawFilters(StateModel stateModel)
        {
            try
            {
                int pages = 0;
                var pagiList = _dbContext.Users;

                stateModel.SortBy = _listSortOrderDatabase[_listSortOrder.IndexOf(stateModel.SortBy)];

                List<UserModel> test = _pagination.GetByRawOrderPagiList<UserModel>(pagiList, stateModel, _perPage, _listQueries.ToArray());

                return new Tuple<int, List<UserModel>>(pages, test);
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
