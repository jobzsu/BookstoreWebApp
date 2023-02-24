using BookstoreWebApp.Common.Models;

namespace BookstoreWebApp.Common
{
    public class InMemoryDataStore
    {
        List<BookModel> books = new List<BookModel>();
        List<EventModel> events = new List<EventModel>();

        public InMemoryDataStore()
        {
            books.AddRange(new List<BookModel>()
            {
                new BookModel()
                {
                    Id = Guid.Parse("9b0896fa-3880-4c2e-bfd6-925c87f22878"),
                    Title = "CQRS for Dummies",
                    IsReserved = false
                },
                new BookModel()
                {
                    Id = Guid.Parse("0550818d-36ad-4a8d-9c3a-a715bf15de76"),
                    Title = "Visual Studio Tips",
                    IsReserved = false
                },new BookModel()
                {
                    Id = Guid.Parse("8e0f11f1-be5c-4dbc-8012-c19ce8cbe8e1"),
                    Title = "NHibernate Cookbook",
                    IsReserved = false
                }
            });

            books.ForEach(b =>
            {
                events.Add(new EventModel()
                {
                    Id = new Guid(),
                    Type = "Insert",
                    BookState = b,
                    DateRecorded = DateTime.UtcNow
                });
            });
        }

        public List<BookModel> GetAllBooks()
        {
            return books;
        }

        public BookModel GetById(Guid Id)
        {
            return books.FirstOrDefault(b => b.Id == Id);
        }

        public List<BookModel> QueryByKeyword(string keyword)
        {
            keyword = keyword.ToLower().Trim();
            return books.Where(b => b.Title.ToLower().Contains(keyword)).ToList();
        }

        public void ReserveBook(Guid bookId)
        {
            var book = GetById(bookId);
            if (book is null) throw new NullReferenceException($"Book with Id:{bookId} not found");
            else
            {
                if (book.IsReserved) 
                    throw new Exception($"Book: {book.Title} is already reserved.");
                else
                {
                    book.IsReserved = true; // booking number automatically generated

                    // Record event
                    events.Add(new EventModel()
                    {
                        Id = new Guid(),
                        BookState = book,
                        Type = "Reserve",
                        DateRecorded = DateTime.UtcNow
                    });
                }
            }
        }

        public void ReturnBook(Guid bookId, string bookingNumber)
        {
            var book = GetById(bookId);
            if (book is null) throw new NullReferenceException($"Book with Id:{bookId} not found");
            else
            {
                if (book.BookingNumber != bookingNumber)
                    throw new Exception($"Incorrect booking number");
                else
                {
                    book.IsReserved = false; // booking number automatically set to null

                    // Record event
                    events.Add(new EventModel()
                    {
                        Id = new Guid(),
                        BookState = book,
                        Type = "Return",
                        DateRecorded = DateTime.UtcNow
                    });
                }   
            }
        }
    }
}
