using Microsoft.AspNetCore.Mvc;
using MyWebApp.Attributes;
using MyWebApp.DTO;
using MyWebApp.DTO.SleepEntry.Request;
using MyWebApp.DTO.SleepEntry.Response;
using MyWebApp.DTO.State.Request;
using MyWebApp.DTO.User.Request;
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
        private readonly ILogger<userController> _logger;
        private readonly ISleepEntryService _sleepEntryInterface;

        private readonly Dictionary<int, Tuple<int, List<string>>> _listErrors = new Dictionary<int, Tuple<int, List<string>>>()
        {
            { 1, new Tuple<int, List<string>>(StatusCodes.Status404NotFound, new List<string>() { "Sort Order Not Exist!" }) },
            { 2, new Tuple<int, List<string>>(StatusCodes.Status500InternalServerError, new List<string>() { "Server Errors!" }) },
            { 3, new Tuple<int, List<string>>(StatusCodes.Status409Conflict, new List<string>() { "User Not Exist!" }) },
            { 4, new Tuple<int, List<string>>(StatusCodes.Status403Forbidden, new List<string>() { "Not Allows!" }) },
            { 5, new Tuple<int, List<string>>(StatusCodes.Status403Forbidden, new List<string>() { "Cannot Remove Admin!" }) },
        };

        public userController(ISleepEntryService sleepEntryInterface, IUserService userInterface, ILogger<userController> logger)
        {
            _userInterface = userInterface;
            _sleepEntryInterface = sleepEntryInterface;
            _logger = logger;
        }

        [AttributeJwt]
        [HttpGet]
        public CustomResponse GetList([FromQuery] StateRequestDto requestDto)
        {
            try
            {
                var account = (UserModel)HttpContext.Items["Account"];

                var listUser = _userInterface.GetAllUsers();

                var responseListUser = new UserPagiListResponseDTO()
                {
                    UserInfoDtos = listUser.Select(item => new UserInfoDto()
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

        [AttributeJwt]
        [HttpDelete]
        public CustomResponse RemoveUser(RequestDeleteDto requestDeleteDto)
        {
            try
            {
                var account = (UserModel)HttpContext.Items["Account"];

                if(!account.IsAdmin) 
                    return new CustomResponse(_listErrors[4]);

                var user = this._userInterface.CheckUserByID(requestDeleteDto.UserId);

                if (user.IsAdmin) 
                    return new CustomResponse(_listErrors[5]);

                if (user == null)
                    return new CustomResponse(_listErrors[3]);

                this._userInterface.DeleteUser(user);

                return new CustomResponse();
            }
            catch (Exception e)
            {
                _logger.LogInformation("Error {0}", e.Message);
                return new CustomResponse(_listErrors[2]);
            }
        }

        [AttributeJwt]
        [HttpPut]
        public CustomResponse EditUser(RequestUpdateDto requestDeleteDto)
        {
            try
            {
                var account = (UserModel)HttpContext.Items["Account"];
                if (!account.IsAdmin) return new CustomResponse(_listErrors[4]);

                var user = this._userInterface.CheckUserByID(requestDeleteDto.UserId);

                if (user == null)
                    return new CustomResponse(_listErrors[3]);

                this._userInterface.UpdateUser(user, requestDeleteDto);

                return new CustomResponse();
            }
            catch (Exception e)
            {
                _logger.LogInformation("Error {0}", e.Message);
                return new CustomResponse(_listErrors[2]);
            }
        }

        [AttributeJwt]
        [Route("sleep")]
        [HttpGet]
        public CustomResponse GetListSleep()
        {
            try
            {
                var account = (UserModel)HttpContext.Items["Account"];

                List<SleepEntryModel> sleepEntries = this._sleepEntryInterface.GetAllSleepEntries(account.UserId);

                int averageSleepTime = 0;
                int averageWakeUpTime = 0;
                int averageDuration = 0;

                if (sleepEntries.Count > 0)
                {
                    averageSleepTime = sleepEntries.Sum(item => item.SleepTime) / sleepEntries.Count;
                    averageWakeUpTime = sleepEntries.Sum(item => item.SleepTime) / sleepEntries.Count;
                    averageDuration = sleepEntries.Sum(item => item.SleepDuration) / sleepEntries.Count;
                }

                var responseObject = new SleepDurationGetListResponse()
                {
                    AverageDuration = averageDuration,
                    AverageWakeUpTime = averageWakeUpTime,
                    AverageSleepTime = averageSleepTime,
                    SleepEntries = sleepEntries,
                };

                return new CustomResponse(content: responseObject);
            }
            catch (Exception e)
            {
                _logger.LogInformation("Error {0}", e.Message);
                return new CustomResponse(_listErrors[3]);
            }
        }


        [AttributeJwt]
        [Route("sleep")]
        [HttpDelete]
        public CustomResponse DeleteSleepEntry([FromBody] SleepEntryDeleteRequest requestUserDTO)
        {
            try
            {
                var account = (UserModel)HttpContext.Items["Account"];

                SleepEntryModel sleepEntry = this._sleepEntryInterface.GetSleepEntryById(requestUserDTO.SleepEntryId);

                if (sleepEntry == null)
                    return new CustomResponse(_listErrors[3]);

                if (sleepEntry.UserId != account.UserId)
                    return new CustomResponse(_listErrors[5]);

                return new CustomResponse();
            }
            catch (Exception e)
            {
                _logger.LogInformation("Error {0}", e.Message);
                return new CustomResponse(_listErrors[3]);
            }
        }

        [AttributeJwt]
        [Route("sleep")]
        [HttpPut]
        public CustomResponse UpdateSleepEntry([FromBody] SleepEntryDeleteRequest requestUserDTO)
        {
            try
            {
                var account = (UserModel)HttpContext.Items["Account"];

                SleepEntryModel sleepEntry = this._sleepEntryInterface.GetSleepEntryById(requestUserDTO.SleepEntryId);

                if (sleepEntry == null)
                    return new CustomResponse(_listErrors[3]);

                if (sleepEntry.UserId != account.UserId)
                    return new CustomResponse(_listErrors[5]);

                return new CustomResponse();
            }
            catch (Exception e)
            {
                _logger.LogInformation("Error {0}", e.Message);
                return new CustomResponse(_listErrors[3]);
            }
        }

        [AttributeJwt]
        [Route("sleep")]
        [HttpPost]
        public CustomResponse CreateNewSleepEntry([FromBody] SleepEntryCreateRequest requestUserDTO)
        {
            try
            {
                var account = (UserModel)HttpContext.Items["Account"];

                SleepEntryModel sleepEntry = this._sleepEntryInterface.CreateNewSleepEntry(requestUserDTO, account.UserId);

                if (sleepEntry == null)
                    return new CustomResponse(_listErrors[3]);

                return new CustomResponse(content: new SleepEntryCreateResponse()
                {
                    SleepEntryId = sleepEntry.SleepEntryId,
                    Date = (DateTime)sleepEntry.Date,
                    SleepTime = sleepEntry.SleepTime,
                    WakeUpTime =sleepEntry.WakeUpTime,
                    Duration = sleepEntry.SleepDuration
                }
             );
            }
            catch (Exception e)
            {
                _logger.LogInformation("Error {0}", e.Message);
                return new CustomResponse(_listErrors[3]);
            }
        }
    }
}
