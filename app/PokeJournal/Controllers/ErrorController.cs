using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using PokeJournal.Exceptions;

namespace PokeJournal.Controllers;

[ApiController]
public class ErrorController : ControllerBase
{
    [HttpGet("/Error")]
    public ActionResult OnGetError()
    {
        return ShowError();
    }

    [HttpPost("/Error")]
    public ActionResult OnPostError()
    {
        return ShowError();
    }

    private ActionResult ShowError()
    {
        var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

        var error = exceptionHandlerPathFeature?.Error;

        if (error is NotFoundException)
        {
            var ex = (NotFoundException)error;
            return StatusCode(404, ex.Message);
        }

        if (error is DbUpdateException)
        {
            var ex = (DbUpdateException)error;
            return StatusCode(400, ex.Message);
        }

        if (error is Exception)
        {
            var ex = error;
            return StatusCode(500, ex.Message);
        }

        return Ok("Everything is okay");
    }

}
