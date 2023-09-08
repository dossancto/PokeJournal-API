using System.Security.Claims;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PokeJournal.Controllers;

[ApiController]
public class ErrorController : ControllerBase
{
  public ErrorController(){ }

  [HttpGet("/Error")]
  public ActionResult OnGetError(){
    return ShowError();
  }

  [HttpPost("/Error")]
  public ActionResult OnPostError(){
    return ShowError();
  }

  private ActionResult ShowError(){
     var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

     var error = exceptionHandlerPathFeature?.Error;

      if (error is DbUpdateException dbEx)
      {
          return StatusCode(400, dbEx.Message);
      }

      if (error is Exception ex)
      {
          return StatusCode(500, "Internal server error.");
      }

      return Ok("Everything is okay");
  }

}
