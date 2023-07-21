using Microsoft.AspNetCore.Mvc;

public class UserLogin
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class ResetPasswordModel
{
    public int UserId { get; set; }
    public string NewPassword { get; set; }
}

public class ForgotPasswordModel
{
    
}

public class UpdatePasswordModel
{
    public int UserId { get; set; }
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("signup")]
    public IActionResult SignUp([FromBody] User newUser)
    {
        
        
        return Ok("User signed up successfully!");
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLogin userLogin)
    {
        var user = _authService.Authenticate(userLogin.Username, userLogin.Password);
        if (user == null)
        {
            return BadRequest("Invalid username or password");
        }

        
        return Ok("Login successful!");
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        
        return Ok("Logged out successfully!");
    }

    [HttpPost("reset-password")]
    public IActionResult ResetPassword([FromBody] ResetPasswordModel model)
    {
        _authService.ResetPassword(model.UserId, model.NewPassword);
        return Ok("Password reset successful!");
    }

    [HttpPost("forgot-password")]
    public IActionResult ForgotPassword([FromBody] ForgotPasswordModel model)
    {
        
        return Ok("Forgot password request processed successfully!");
    }

    [HttpPost("update-password")]
    public IActionResult UpdatePassword([FromBody] UpdatePasswordModel model)
    {
        _authService.UpdatePassword(model.UserId, model.CurrentPassword, model.NewPassword);
        return Ok("Password update successful!");
    }
}
