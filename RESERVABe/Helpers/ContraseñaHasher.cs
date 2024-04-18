using System;
using System.Security.Cryptography;
using System.Text;

public static class ContraseñaHasher
{
    private const int SaltSize = 16;
    private const int HashSize = 32;

    public static string HashPassword(string password)
    {
        byte[] salt = GenerateSalt();
        byte[] hash = GenerateHash(password, salt);
        return Convert.ToBase64String(hash) + ":" + Convert.ToBase64String(salt);
    }

    public static bool VerifyPassword(string password, string hashedPassword)
    {
        string[] parts = hashedPassword.Split(':');
        if (parts.Length != 2)
        {
           
            return false;
        }

        byte[] hash = Convert.FromBase64String(parts[0]);
        byte[] salt = Convert.FromBase64String(parts[1]);
        byte[] expectedHash = GenerateHash(password, salt);
        return ByteArraysEqual(hash, expectedHash);
    }

    private static byte[] GenerateHash(string password, byte[] salt)
    {
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
        {
            return pbkdf2.GetBytes(HashSize);
        }
    }

    private static byte[] GenerateSalt()
    {
        byte[] salt = new byte[SaltSize];
        using (var rng = new RNGCryptoServiceProvider())
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
