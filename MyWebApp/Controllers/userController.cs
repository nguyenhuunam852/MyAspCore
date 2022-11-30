using Microsoft.AspNetCore.Mvc;
using MyWebApp.Attributes;
using MyWebApp.DTO;
using MyWebApp.DTO.State.Request;
using MyWebApp.DTO.User.Response;
using MyWebApp.Interface;
using MyWebApp.Models;

#nullable disable

namespace MyWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        private readonly IUserService _userInterface;
        private readonly IStateService _stateInterface;
        private readonly ILogger<userController> _logger;

        private readonly Dictionary<int, Tuple<int, List<string>>> _listErrors = new Dictionary<int, Tuple<int, List<string>>>()
        {
            { 1, new Tuple<int, List<string>>(StatusCodes.Status404NotFound, new List<string>() { "Sort Order Not Exist!" }) },
            { 2, new Tuple<int, List<string>>(StatusCodes.Status500InternalServerError, new List<string>() { "Server Errors!" }) },
        };

        public userController(IUserService userInterface, IStateService stateInterface, ILogger<userController> logger)
        {
            _userInterface = userInterface;
            _stateInterface = stateInterface;
            _logger = logger;
        }

        private StateRequestDto copyRequest(StateRequestDto requestDto, bool firstLogin, bool sortOrder, string sortBy)
        {
            return new StateRequestDto()
            {
                FirstLogin = firstLogin,
                DESC = sortOrder,
                SortBy = (string.IsNullOrEmpty(sortBy))? requestDto.SortBy: sortBy,
                Page = requestDto.Page,
                Filter = requestDto.Filter
            };
        }

        [Route("")]
        [AttributeJwt]
        [HttpGet]
        public CustomResponse GetList([FromQuery] StateRequestDto requestDto)
        {
            try
            {
                var account = (UserModel)HttpContext.Items["Account"];

                string sortBy = "";
                if (string.IsNullOrEmpty(requestDto.SortBy))
                {
                    sortBy = _userInterface.GetListSortOrder()[0];
                }
                else
                {
                    if (!_userInterface.GetListSortOrder().Contains(requestDto.SortBy)) return new CustomResponse(_listErrors[1]);
                }

                bool firstLogin = requestDto.FirstLogin ?? false;
                bool sortOrder = requestDto.DESC ?? false;

                var processingRequestDto = copyRequest(requestDto, firstLogin, sortOrder, sortBy);
 
                var getCurrentState = this._stateInterface.GetState(processingRequestDto, account.UserId);

                var tupleInfo = this._userInterface.GetAllUsersWithFilters(getCurrentState);

                var responseListUser = new UserPagiListResponseDTO()
                {
                    Pages = tupleInfo.Item1,
                    CurrentPage = getCurrentState.Page + 1,
                    FilterParam = getCurrentState.FilterParam,
                    UserInfoDtos = tupleInfo.Item2.Select(item => new UserInfoDto()
                    {
                        UserId = item.UserId,
                        UserName = item.UserName,
                        Email = item.Email,
                        FullName = item.FullName
                    }).ToArray()
                };

                return new CustomResponse(content: responseListUser);
            }
            catch (Exception e)
            {
                _logger.LogInformation("Error {0}", e.Message);
                return new CustomResponse(_listErrors[2]);
            }
        }
    }
}
