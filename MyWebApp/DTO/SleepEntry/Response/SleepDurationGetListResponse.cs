using MyWebApp.Models;

namespace MyWebApp.DTO.SleepEntry.Response
{
    public class SleepDurationGetListResponse
    {
        public int AverageSleepTime;
        public int AverageWakeUpTime;
        public int AverageDuration;

        public List<SleepEntryModel>? SleepEntries;
    }
}
