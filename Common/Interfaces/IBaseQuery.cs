namespace BookstoreWebApp.Common.Interfaces
{
    public interface IBaseQuery<BookModel>
    {
        Task<List<BookModel>> GetAll();

        Task<BookModel> GetById(Guid Id);

        Task<List<BookModel>> QueryByKeyword(string keyword);
    }
}
