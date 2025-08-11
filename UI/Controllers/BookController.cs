using LibrarySystem.Domains.Dtos;
using LibrarySystem.Domains.Entities;
using LibrarySystem.Domains.Enums;
using LibrarySystem.Domains.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers;

public class BookController : Controller
{
    #region Fields

    public readonly IBook _bookService;

    #endregion

    #region Constructor

    public BookController(IBook bookService)
    {
        _bookService = bookService;
    }

    #endregion

    #region Action Methods

    [HttpGet]
    public IActionResult InsertBook()
    {
        var response = new ResponseDto();

        return View(response);
    }

    [HttpPost]
    public async Task<IActionResult> InsertBookData(InsertBookRequestDto request)
    {
        var response = new ResponseDto();
        response.OpStatus = await _bookService.AddAsync(request);
        response.Message = string.Empty;
        if (response.OpStatus == OpStatus.successfully)
        {
            response.Message = "Book inserted successfully.";
        }
        else if (response.OpStatus == OpStatus.Failed)
        {
            response.Message = $"Failed to insert book. Check if the author Id: {request.AuthorId} is already exist.";
        }
        return View("InsertBook", response);
    }

    [HttpGet]
    public async Task<IActionResult> ManageBooks()
    {
        var resopnse = await _bookService.GetAllAsync();
        return View(resopnse);
    }

    [HttpGet]
    public async Task<IActionResult> LogicalDelete(int id)
    {
        await _bookService.DeleteAsync(id);
        var resopnse = await _bookService.GetAllAsync();
        return View("ManageBooks", resopnse);
    }

    [HttpGet]
    public async Task<IActionResult> UpdateBookInfo(int id)
    {
        var updateBookResponse = new UpdateBookResponseDto();
        updateBookResponse.Book = await _bookService.GetBookByIdAsync(id);
        return View(updateBookResponse);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateBookInfoData(Book request)
    {
        var updateBookResponse = new UpdateBookResponseDto();
        updateBookResponse.BookResponse = new ResponseDto();
        updateBookResponse.BookResponse.OpStatus = await _bookService.UpdateAsync(request);

        if (updateBookResponse.BookResponse.OpStatus == OpStatus.successfully)
        {
            updateBookResponse.BookResponse.Message = "Book updated successfuly";
        }
        else if (updateBookResponse.BookResponse.OpStatus == OpStatus.Failed)
        {
            updateBookResponse.BookResponse.Message = "Failed to update book";
        }
        else if (updateBookResponse.BookResponse.OpStatus == OpStatus.NotFound)
        {
            updateBookResponse.BookResponse.Message = "Book not found";
        }

        updateBookResponse.Book = await _bookService.GetBookByIdAsync(request.Id);
        return View("UpdateBookInfo", updateBookResponse);
    }

    [HttpPost]
    public async Task<IActionResult> SearchByBookTitle(string title)
    {
        var books = await _bookService.GetBooksByTitleAsync(title);
        return View("ManageBooks", books);
    }
    #endregion
}
