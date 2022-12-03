namespace MyWebApp.DTO.SleepEntry.Request
{
    #nullable disable
    public class SleepEntryCreateRequest
    {
        public DateTime Date { get; set; }

        public DateTime SleepTime { get; set; }
        public DateTime WakeUpTime { get; set; }

        public int Duration { get; set; }
    }
}
