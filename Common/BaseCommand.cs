using BookstoreWebApp.Common.Interfaces;
using BookstoreWebApp.Common.Models;

namespace BookstoreWebApp.Common
{
    public class BaseCommand : IBaseCommand<BookModel>
    {
        protected readonly InMemoryDataStore _inMemDS;

        public BaseCommand(InMemoryDataStore inMemDS)
        {
            _inMemDS = inMemDS;
        }

        public async Task ReserveBook(Guid bookId)
        {
            _inMemDS.ReserveBook(bookId);
            await Task.CompletedTask;
        }

        public async Task ReturnBook(Guid bookId, string bookingNumber)
        {
            _inMemDS.ReturnBook(bookId, bookingNumber);
            await Task.CompletedTask;
        }
    }
}
