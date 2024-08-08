using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuiltInMiddlewareSample.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TestsController : ControllerBase
  {

    private readonly IHttpContextAccessor _httpContextAccessor;

    public TestsController(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    [HttpPost("cookie")]
    public IActionResult Set()
    {

      var options = new CookieOptions { Expires = DateTimeOffset.Now.AddHours(5) };

      Response.Cookies.Append("x-csrf-token", Guid.NewGuid().ToString(), options);


      return Ok();
    }

    [HttpPost("session")]
    public IActionResult SetSession()
    {
      HttpContext.Session.SetString("SessionKey", "SessionValue");

      //_httpContextAccessor.HttpContext.Session

      return Ok();
    }

    [HttpGet("compression")]
    [ResponseCache(Duration = 300,VaryByHeader = "clientName", VaryByQueryKeys = new[] {"pageSize"})]
    // 64 MB en fazla Response Body Cachelenbilir
    // Get istekleri bekler status code 200 bekler
    // Authorization Header için Cache yapılmıyor.
    public async Task<IActionResult> ResponseCompression([FromHeader] string? clientName, [FromQuery] int pageSize = 1)
    {
      using var client = new HttpClient();
      var task1 =  client.GetStringAsync("https://google.com");


      var response = await task1;

      return Ok(response);
    }

 



  
  }
}
