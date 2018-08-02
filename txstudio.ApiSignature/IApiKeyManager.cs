using System;
using System.Collections.Generic;
using System.Text;

namespace txstudio.ApiSignature
{
    public interface IApiKeyManager
    {
        string GetSecret(string apikey);
    }
}
