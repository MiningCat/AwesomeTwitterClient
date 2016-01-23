using System;

namespace TwitterClient.Core.Exceptions
{
    [Serializable]
    public class InvalidParameterStringFormatException : Exception
    {
        public InvalidParameterStringFormatException(string parameterString) : base(
            $"Parameter string {parameterString} has invalid format")
        { }
    }
}
