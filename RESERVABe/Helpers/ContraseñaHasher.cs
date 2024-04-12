
using System;
using System.Security.Cryptography;
using System.Text;

public static class ContraseñaHasher
{
    private static readonly int _iterations = 10000; 
    private static readonly int _saltSize = 16; 

    public static string HashPassword(string password)
    {
        byte[] salt = GenerateSalt();
        byte[] hash = Pbkdf2(password, salt, _iterations);

        return Convert.ToBase64String(hash) + ":" + Convert.ToBase64String(salt);
    }

    public static bool VerifyPassword(string password, string hashedPassword)
    {
        string[] parts = hashedPassword.Split(':');
        byte[] hash = Convert.FromBase64String(parts[0]);
        byte[] salt = Convert.FromBase64String(parts[1]);

        byte[] computedHash = Pbkdf2(password, salt, _iterations);

        return ByteArraysEqual(hash, computedHash);
    }

    private static byte[] Pbkdf2(string password, byte[] salt, int iterations)
    {
        Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
        return pbkdf2.GetBytes(_saltSize);
    }

    private static byte[] GenerateSalt()
    {
        byte[] salt = new byte[_saltSize];
        using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(salt);
        }
        return salt;
    }

    private static bool ByteArraysEqual(byte[] a, byte[] b)
    {
        if (a == null && b == null)
            return true;

        if (a == null || b == null || a.Length != b.Length)
            return false;

        int result = 0;
        for (int i = 0; i < a.Length; i++)
            result |= a[i] ^ b[i];
        return result == 0;
    }
}