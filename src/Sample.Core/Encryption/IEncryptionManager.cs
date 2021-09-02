namespace Sample.Core.Encryption
{
    public interface IEncryptionManager
    {
        byte[] GetKey(string password, byte[] salt);

        EncryptedData CreateSaltKey(string password);
    }
}