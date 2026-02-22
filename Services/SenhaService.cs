using System.Security.Cryptography;
using System.Text;

public static class SenhaService
{
    public static string Hash(string senha)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(senha));
        return Convert.ToBase64String(bytes);
    }
}
