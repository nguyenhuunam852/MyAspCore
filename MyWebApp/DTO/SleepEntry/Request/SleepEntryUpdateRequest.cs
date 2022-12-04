namespace MyWebApp.DTO.SleepEntry.Request
{
    public class SleepEntryUpdateRequest
    {
        public int SleepEntryId { get; set; }

        public DateTime Date { get; set; }

        public int SleepTime { get; set; }
        public int WakeUpTime { get; set; }

        public int Duration { get; set; }
    }
}
