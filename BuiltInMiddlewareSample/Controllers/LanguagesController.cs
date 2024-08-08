using BuiltInMiddlewareSample.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace BuiltInMiddlewareSample.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class LanguagesController : ControllerBase
  {
    private readonly IStringLocalizer<LanguagesController> _localizer;

    public LanguagesController(IStringLocalizer<LanguagesController> localizer)
    {
      _localizer = localizer;
    }

    // api/languages?culture=tr-TR
    // api/languages?culture=en-US
    [HttpGet]
    public IActionResult Get()
    {
      var value = _localizer.GetString("Hello").Value;

      return Ok(value);
    }

    [HttpPost]
    public IActionResult Post([FromBody] PostDto dto)
    {
      if (ModelState.IsValid)
      {
        return Ok();
      }

      return BadRequest(ModelState);
    }
  }
}
