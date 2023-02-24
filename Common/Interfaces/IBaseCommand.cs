namespace BookstoreWebApp.Common.Interfaces
{
    public interface IBaseCommand<BookModel>
    {
        Task ReserveBook(Guid bookId);

        Task ReturnBook(Guid bookId, string bookingNumber);
    }
}
