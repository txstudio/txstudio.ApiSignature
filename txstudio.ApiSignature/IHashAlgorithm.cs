namespace txstudio.ApiSignature
{
    public interface IHashAlgorithm
    {
        string Hash(string content);
    }
}
