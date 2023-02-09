using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models
{
    public static class LoggerModel
    {
        private static ILogger _logger;

        public static ILogger Logger => _logger;

        public static void Configure(ILogger logger)
        {
            _logger = logger;
        }
    }
}
