namespace BookstoreWebApp.Common.Models
{
    public class EventModel
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public BookModel BookState { get; set; }
        public DateTime DateRecorded { get; set; }
    }
}
