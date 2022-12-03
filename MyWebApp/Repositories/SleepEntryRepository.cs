using MyWebApp.DTO.SleepEntry.Request;
using MyWebApp.Interface;
using MyWebApp.Models;

namespace MyWebApp.Repositories
{
    public class SleepEntryRepository : ISleepEntryService
    {
        private readonly ILogger<SleepEntryRepository> _logger;

        private readonly DBContext _dbContext;

        public SleepEntryRepository(DBContext dbContext, ILogger<SleepEntryRepository> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public SleepEntryModel? CreateNewSleepEntry(SleepEntryCreateRequest sleepEntryCreateRequest, int userId)
        {
            try
            {
                SleepEntryModel sleepEntryModel = new SleepEntryModel()
                {
                    Date = sleepEntryCreateRequest.Date,
                    SleepTime = sleepEntryCreateRequest.SleepTime,
                    WakeUpTime = sleepEntryCreateRequest.WakeUpTime,
                    SleepDuration = sleepEntryCreateRequest.Duration,
                    UserId = userId
                };

                _dbContext.SleepEntries.Add(sleepEntryModel);

                _dbContext.SaveChanges();

                return sleepEntryModel;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception("RegisterNewUser");
            }
        }

        public bool DeleteSleepEntry(SleepEntryModel sleepEntryID)
        {
            try
            {
                sleepEntryID.IsDeleted = true;

                var getUser = _dbContext.SleepEntries.Update(sleepEntryID);

                _dbContext.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception("CheckUserByID");
            }
        }

        public List<SleepEntryModel> GetAllSleepEntries(int userId)
        {
            try
            {
                var getSleepEntry = _dbContext.SleepEntries.Where(item=>item.IsDeleted == false && item.UserId == userId).ToList();
                return getSleepEntry;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception("CheckUserByID");
            }
        }

        public SleepEntryModel? GetSleepEntryById(int sleepEntryId)
        {
            try
            {
                var getSleepEntry = _dbContext.SleepEntries.Find(sleepEntryId);
                return getSleepEntry;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception("CheckUserByID");
            }
        }

        public bool UpdateSleepEntry(SleepEntryModel sleepEntry, SleepEntryUpdateRequest sleepEntryUpdateRequest)
        {
            try
            {
                sleepEntry.Date = sleepEntryUpdateRequest.Date;
                sleepEntry.SleepTime = sleepEntryUpdateRequest.SleepTime;
                sleepEntry.WakeUpTime = sleepEntryUpdateRequest.WakeUpTime;
                sleepEntry.SleepDuration = sleepEntryUpdateRequest.Duration;

                var getUser = _dbContext.SleepEntries.Update(sleepEntry);

                _dbContext.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception("CheckUserByID");
            }
        }
    }
}
