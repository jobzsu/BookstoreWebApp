using BookstoreWebApp.Common;
using BookstoreWebApp.Common.Interfaces;
using BookstoreWebApp.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreWebApp.Controllers
{
    public class BookController : Controller
    {
        protected readonly IBaseCommand<BookModel> _baseCmd;
        protected readonly IBaseQuery<BookModel> _baseQry;

        public BookController(IBaseCommand<BookModel> baseCmd, IBaseQuery<BookModel> baseQry)
        {
            _baseCmd = baseCmd;
            _baseQry = baseQry;
        }

        [HttpGet]
        public IActionResult Index(string keyword = "")
        {
            if (string.IsNullOrEmpty(keyword))
                return View(_baseQry.GetAll().Result);
            else
            {
                var books = _baseQry.QueryByKeyword(keyword).Result;

                if (books.Any())
                    return View(books);
                else
                    return View("NoDataList");
            }
        }

        [Route("~/Book/Reserve")]
        [HttpPost]
        public async Task<JsonResult> Reserve(Guid bookId)
        {
            try
            {
                await _baseCmd.ReserveBook(bookId);

                return await Task.FromResult(new JsonResult(new { IsSuccess = true }));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new JsonResult(new { IsSuccess = false, Message = ex.Message }));
            }
        }

        [Route("Return")]
        [HttpPost]
        public async Task<JsonResult> Return(Guid bookId, string bookingNumber)
        {
            try
            {
                await _baseCmd.ReturnBook(bookId, bookingNumber);

                return await Task.FromResult(new JsonResult(new { IsSuccess = true }));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new JsonResult(new { IsSuccess = false, Message = ex.Message }));
            }
        }
    }
}
