using System.Security.Cryptography;
using System.Text;

public class AuthService
{
    private readonly AppDbContext _dbContext;

    public AuthService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public User Authenticate(string username, string password)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Username == username);
        if (user == null) return null;

        var passwordHash = GeneratePasswordHash(password);
        return user.PasswordHash == passwordHash ? user : null;
    }

    public void ResetPassword(int userId, string newPassword)
    {
        var user = _dbContext.Users.Find(userId);
        if (user != null)
        {
            user.PasswordHash = GeneratePasswordHash(newPassword);
            _dbContext.SaveChanges();
        }
    }

    public void UpdatePassword(int userId, string currentPassword, string newPassword)
    {
        var user = _dbContext.Users.Find(userId);
        if (user != null && user.PasswordHash == GeneratePasswordHash(currentPassword))
        {
            user.PasswordHash = GeneratePasswordHash(newPassword);
            _dbContext.SaveChanges();
        }
    }

    private string GeneratePasswordHash(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        var builder = new StringBuilder();
        foreach (var b in bytes)
        {
            builder.Append(b.ToString("x2"));
        }
        return builder.ToString();
    }
}
