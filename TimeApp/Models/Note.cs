

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


        public bool IsCompleted { 
            
            get
            {
                return Status == TaskStatus.Completed;
            }

        }

        public Color DateStatus
        {
            get
            {
                if (Deadline < DateTime.Today)
                {
                    return new Color(0.721f, 0.000f, 0.000f);
                }
                if (Deadline == DateTime.Today)
                {
                    return new Color(0.118f, 0.565f, 1.000f);
                }
                return new Color(0.827f, 0.827f, 0.827f);
            }
        }

    }
}
