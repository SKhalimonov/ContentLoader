using System;

namespace ContentLoader.Core.Exceptions
{
    public class UnsupportedServiceException : Exception
    {
        public UnsupportedServiceException(string serviceHost): base($"{serviceHost} is unsupported.")
        {
        }
    }
}
