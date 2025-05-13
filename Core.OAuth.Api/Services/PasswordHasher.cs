using System.Security.Cryptography;

namespace Core.OAuth.Api.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iteration = 1000;

        private readonly HashAlgorithmName _algorithmName = HashAlgorithmName.SHA256;
        public string Hash(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iteration, _algorithmName, HashSize);

            var hashWithSalt = new byte[SaltSize + HashSize];
            Buffer.BlockCopy(salt, 0, hashWithSalt, 0, SaltSize);
            Buffer.BlockCopy(hash, 0, hashWithSalt, SaltSize, HashSize);

            return Convert.ToBase64String(hashWithSalt);
        }

        public bool Verify(string password, string passwordHash)
        {
            var hashWithSaltByte = Convert.FromBase64String(passwordHash);

            if (hashWithSaltByte.Length != SaltSize + HashSize)
            {
                return false;
            }

            var saltByte = new byte[SaltSize];
            Buffer.BlockCopy(hashWithSaltByte, 0, saltByte, 0, SaltSize);

            var storedHashByte = new byte[HashSize];
            Buffer.BlockCopy(hashWithSaltByte, SaltSize, storedHashByte, 0, HashSize);

            var hash = Rfc2898DeriveBytes.Pbkdf2(password, saltByte, Iteration, _algorithmName, HashSize);

            return CryptographicOperations.FixedTimeEquals(hash, storedHashByte);
        }
    }
}
