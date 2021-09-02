using System.Linq;
using System.Security.Cryptography;

namespace Sample.Core.Encryption
{
    public class EncryptionManager : IEncryptionManager
    {
        #region Methods

        public byte[] GetKey(string password, byte[] salt)
        {
            if (string.IsNullOrWhiteSpace(password))
                return null;

            using (var deriveBytes = GetDeriveBytes(password, salt))
            {
                return deriveBytes.GetBytes(20);//Derive a 20-byte key
            }
        }

        public EncryptedData CreateSaltKey(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return null;

            var salt = GenerateSalt();
            var encryptedData = new EncryptedData();

            using (var deriveBytes = GetDeriveBytes(password, salt))
            {
                byte[] key = deriveBytes.GetBytes(20);

                encryptedData.Key = key;
                encryptedData.Salt = salt;
            }

            return encryptedData;
        }

        private byte[] GenerateSalt()
        {
            var salt = new byte[20];

            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return salt;
        }

        private Rfc2898DeriveBytes GetDeriveBytes(string password, byte[] salt)
        {
            return new Rfc2898DeriveBytes(password, HashWithDefault(salt));
        }

        private byte[] HashWithDefault(byte[] salt)
        {
            byte[] bytes = { 75, 0, 84, 0, 55, 0, 119, 0, 98, 0, 33, 0, 51, 0, 71, 0, 121, 0, 115 };
            SHA256Managed hashstring = new SHA256Managed();

            return hashstring.ComputeHash(bytes.Select(x => (byte)(x ^ 79)).Concat(salt).ToArray());
        }

        #endregion Methods
    }
}