namespace MyWebApp.DTO.SleepEntry.Request
{
    public class SleepEntryUpdateRequest
    {
        public int SleepEntryId { get; set; }

        public DateTime Date { get; set; }

        public DateTime SleepTime { get; set; }
        public DateTime WakeUpTime { get; set; }

        public int Duration { get; set; }
    }
}
