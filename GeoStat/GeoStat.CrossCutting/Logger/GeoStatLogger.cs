﻿using System;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace GeoStat.CrossCutting.Logger
{
    public class GeoStatLogger : IGeoStatLogger
    {
        private readonly ILogger _logger;
        
        public GeoStatLogger(ILogger logger)
        {
            _logger = logger;
        }

        static GeoStatLogger()
        {
            NLogBuilder.ConfigureNLog("nlog.config");
        }

        public void LogError(string input, Exception ex, params object[] args)
        {
            _logger.LogError(ex, input, args);
        }

        public void LogInfo(string input, params object[] args)
        {
            _logger.LogInformation(input, args);
        }

        public void LogWarn(string input, params object[] args)
        {
            _logger.LogWarning(input, args);
        }
    }
}
