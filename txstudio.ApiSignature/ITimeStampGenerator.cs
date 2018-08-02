using System;

namespace txstudio.ApiSignature
{
    public interface ITimeStampGenerator
    {
        long GetTimeStamp(Nullable<DateTime> datetime = null);
    }
}
