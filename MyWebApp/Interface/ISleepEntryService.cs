using MyWebApp.DTO.SleepEntry.Request;
using MyWebApp.Models;

namespace MyWebApp.Interface
{
    public interface ISleepEntryService
    {
        List<SleepEntryModel> GetAllSleepEntries(int userId);

        SleepEntryModel? GetSleepEntryById(int sleepEntryId);

        SleepEntryModel? CreateNewSleepEntry(SleepEntryCreateRequest sleepEntryCreateRequest, int userId);

        bool DeleteSleepEntry(SleepEntryModel sleepEntryID);
        bool UpdateSleepEntry(SleepEntryModel sleepEntry, SleepEntryUpdateRequest sleepEntryUpdateRequest);
    }
}
