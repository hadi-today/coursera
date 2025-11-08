// Controllers/TestController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("public")]
    public IActionResult GetPublicData()
    {
        return Ok("This is public data.");
    }

    [HttpGet("secure")]
    [Authorize] // This endpoint requires a valid token
    public IActionResult GetSecureData()
    {
        return Ok("This is secure data. You are authorized!");
    }

    [HttpGet("error")]
    public IActionResult TriggerError()
    {
        throw new InvalidOperationException("This is a simulated error.");
    }
}