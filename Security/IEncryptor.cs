namespace Security
{
    public interface IEncryptor
    {
        string Encrypt(string value);

        string Decrypt(string value);
    }
}
