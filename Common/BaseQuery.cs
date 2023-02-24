using BookstoreWebApp.Common.Interfaces;
using BookstoreWebApp.Common.Models;

namespace BookstoreWebApp.Common
{
    public class BaseQuery : IBaseQuery<BookModel>
    {
        protected readonly InMemoryDataStore _inMemDS;

        public BaseQuery(InMemoryDataStore inMemDS)
        {
            _inMemDS = inMemDS;
        }

        public async Task<List<BookModel>> GetAll()
        {
            return await Task.FromResult(_inMemDS.GetAllBooks());
        }

        public async Task<BookModel> GetById(Guid Id)
        {
            return await Task.FromResult(_inMemDS.GetById(Id));
        }

        public async Task<List<BookModel>> QueryByKeyword(string keyword)
        {
            return await Task.FromResult(_inMemDS.QueryByKeyword(keyword));
        }
    }
}
