namespace MyWebApp.DTO.SleepEntry.Response
{
    public class SleepEntryCreateResponse
    {
        public int SleepEntryId { get; set; }

        public DateTime Date { get; set; }

        public int SleepTime { get; set; }
        public int WakeUpTime { get; set; }

        public int Duration { get; set; }
    }
}
