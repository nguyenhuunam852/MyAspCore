namespace MyWebApp.DTO.SleepEntry.Request
{
    #nullable disable
    public class SleepEntryCreateRequest
    {
        public DateTime Date { get; set; }

        public int SleepTime { get; set; }
        public int WakeUpTime { get; set; }

        public int Duration { get; set; }
    }
}
