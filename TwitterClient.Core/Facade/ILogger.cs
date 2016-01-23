using System;

namespace TwitterClient.Core.Facade
{
    public interface ILogger
    {
        void AddError(string error);
        void AddError(Exception exception);
        void AddWarning(string warning);
    }
}
