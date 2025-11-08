// Controllers/UsersController.cs
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private static List<User> _users = new List<User>
    {
        new User { Id = 1, Username = "user1", Email = "user1@example.com" },
        new User { Id = 2, Username = "user2", Email = "user2@example.com" }
    };

    // GET: api/users
    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUsers()
    {
        return Ok(_users);
    }

    // GET: api/users/1
    [HttpGet("{id}")]
    public ActionResult<User> GetUser(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    // POST: api/users
[HttpPost]
public ActionResult<User> CreateUser(CreateUserDto createUserDto) // از DTO استفاده کنید
{
    // ModelState.IsValid به صورت خودکار توسط [ApiController] چک می‌شود
    // اما برای شفافیت می‌توانید خودتان هم چک کنید
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    var user = new User
    {
        Id = _users.Any() ? _users.Max(u => u.Id) + 1 : 1,
        Username = createUserDto.Username,
        Email = createUserDto.Email
    };

    _users.Add(user);
    return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
}

    // PUT: api/users/1
    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, User updatedUser)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        user.Username = updatedUser.Username;
        user.Email = updatedUser.Email;
        return NoContent();
    }

    // DELETE: api/users/1
    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        _users.Remove(user);
        return NoContent();
    }
}