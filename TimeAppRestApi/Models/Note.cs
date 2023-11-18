namespace TimeAppRestApi.Models
{
    public enum TaskStatus
    {
        NotStarted,
        InProgress,
        Completed,
        Canceled,

    }

    public class Note
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Deadline { get; set; }
        public TaskStatus Status { get; set; }

    }
}
