namespace MyWebApp.DTO.SleepEntry.Response
{
    public class SleepEntryCreateResponse
    {
        public int SleepEntryId { get; set; }

        public DateTime Date { get; set; }

        public DateTime SleepTime { get; set; }
        public DateTime WakeUpTime { get; set; }

        public int Duration { get; set; }
    }
}
