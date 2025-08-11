using LibrarySystem.Domains.Dtos;
using LibrarySystem.Domains.Entities;
using LibrarySystem.Domains.Enums;
using LibrarySystem.Domains.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers;

public class AuthorController : Controller
{

    #region Fields

    public readonly IAuthor _authorService;

    #endregion

    #region Constructor

    public AuthorController(IAuthor authorService)
    {
        _authorService = authorService;
    }

    #endregion

    #region Action Methods

    [HttpGet]
    public IActionResult InsertAuthor()
    {
        var status = new ResponseDto();
        status.OpStatus = new OpStatus();

        return View(status);
    }

    [HttpPost]
    public async Task<IActionResult> AddAuthorData(InsertAuthorRequestDto request)
    {
        var author = new Author
        {
            FullName = request.FullName,
            Email = request.Email,
            Address = request.Address,

        };
        var status = new ResponseDto();
        status.OpStatus = new OpStatus();
        status.OpStatus = await _authorService.AddAsync(author);

        if (status.OpStatus == OpStatus.successfully)
        {
            status.Message = "Author added seuccessfully";
        }
        else if (status.OpStatus == OpStatus.Failed)
        {
            status.Message = "Failed to add author";
        }
        else if (status.OpStatus == OpStatus.AlreadyExists)
        {
            status.Message = "Author already exists";
        }
        return View("InsertAuthor", status);
    }

    [HttpGet]
    public async Task<IActionResult> ManageAllAuthors()
    {
        var authors = await _authorService.GetAllAsync();
        return View(authors);
    }

    [HttpPost]
    public async Task<IActionResult> SearchByAuthorName(string name)
    {

        var authors = await _authorService.GetAuthorsByNameAsync(name);
        return View("ManageAllAuthors", authors);
    }

    [HttpGet]
    public async Task<IActionResult> UpdateAuthorInfo(int id)
    {

        var status = new UpdateAuthorResponseDto();
        status.AuthorResponse = new ResponseDto();
        status.Author = new Author();
        status.AuthorResponse.OpStatus = new OpStatus();

        status.Author = await _authorService.GetAuthorByIdAsync(id);
        return View(status);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateAuthorDataAsync(Author request)
    {
        var status = new UpdateAuthorResponseDto();
        status.Author = new Author();
        status.AuthorResponse = new ResponseDto();
        status.AuthorResponse.Message = string.Empty;
        status.AuthorResponse.OpStatus = new OpStatus();

        status.AuthorResponse.OpStatus = await _authorService.UpdateAsync(request);
        if (status.AuthorResponse.OpStatus == OpStatus.successfully)
        {
            status.AuthorResponse.Message = "Author updated successfully";
        }
        else if (status.AuthorResponse.OpStatus == OpStatus.Failed)
        {
            status.AuthorResponse.Message = "Failed to update author";
        }
        else if (status.AuthorResponse.OpStatus == OpStatus.NotFound)
        {
            status.AuthorResponse.Message = "Author not found";
        }
        status.Author = await _authorService.GetAuthorByIdAsync(request.Id);

        return View("UpdateAuthorInfo", status);
    }

    [HttpGet]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        await _authorService.DeleteAsync(id);
        var authors = await _authorService.GetAllAsync();
        return View("ManageAllAuthors", authors);
    }

#endregion

}
