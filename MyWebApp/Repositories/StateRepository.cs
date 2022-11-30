using MyWebApp.DTO.State.Request;
using MyWebApp.Interface;
using MyWebApp.Models;

#nullable disable

namespace MyWebApp.Repositories
{
    public class StateRepository : IStateService
    {
        private readonly IUserService _userInterface;
        private readonly DBContext _dbContext;
        private readonly ILogger<UserRepository> _logger;

        public StateRepository(DBContext dbContext, IUserService userService, ILogger<UserRepository> logger)
        {
            _userInterface = userService;
            _dbContext = dbContext;
        }

        public StateModel GetState(StateRequestDto stateRequestDto, int userId)
        {
            try
            {
                StateModel returnState;
  
                var getState = from states in _dbContext.States
                             where states.UserId == userId
                             select states;

                if (getState.Any())
                {
                    var updateState = getState.First();
                
                    if((bool)stateRequestDto.FirstLogin) return updateState;

                    updateState.FilterParam = stateRequestDto.Filter;
                    updateState.Page = (int)((stateRequestDto.Page == null) ? 0 : stateRequestDto.Page - 1);
                    updateState.IsDesc = (bool)stateRequestDto.DESC;
                    updateState.SortBy = stateRequestDto.SortBy;

                    _dbContext.States.Update(updateState);
                    returnState = updateState;
                }
                else
                {
                    var newState = new StateModel()
                    {
                        UserId = userId,
                        Page = (int)((stateRequestDto.Page == null) ? 0 : stateRequestDto.Page-1),
                        FilterParam = stateRequestDto.Filter,
                        IsDesc = (bool)stateRequestDto.DESC,
                        SortBy = stateRequestDto.SortBy
                    };

                    _dbContext.States.Add(newState);
                
                    returnState = newState;
                }

                _dbContext.SaveChanges();

                return returnState;
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception("CreateNewState");
            }
        }
    }
}
