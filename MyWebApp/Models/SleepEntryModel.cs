namespace MyWebApp.Models
{
    #nullable disable
    public class SleepEntryModel
    {
        public int SleepEntryId;

        public DateTime? Date;
        public DateTime? SleepTime;
        public DateTime? WakeUpTime;

        public bool IsDeleted;

        public int UserId;
        public int SleepDuration;

        public virtual UserModel User { get; set; }
    }
}
