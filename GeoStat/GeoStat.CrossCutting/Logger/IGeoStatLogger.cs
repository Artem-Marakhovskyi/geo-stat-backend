using Microsoft.Extensions.Logging;
using System;

namespace GeoStat.CrossCutting.Logger
{
    public interface IGeoStatLogger
    {
        void LogInfo(string input, params object[] args);
        void LogError(string input, Exception ex, params object[] args);
        void LogWarn(string input, params object[] args);
    }
}
