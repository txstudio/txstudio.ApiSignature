namespace txstudio.ApiSignature
{
    public interface ITimeStampValidator
    {
        bool IsAvailable(long hostTimeStamp, long clientTimeStamp);
    }
}
