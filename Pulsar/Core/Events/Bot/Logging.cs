using System;
using System.Threading.Tasks;

using Discord;

using Pulsar.Core.KixLog;


/*
 *  .____                        .__                
 *  |    |    ____   ____   ____ |__| ____    ____  
 *  |    |   /  _ \ / ___\ / ___\|  |/    \  / ___\ 
 *  |    |__(  <_> ) /_/  > /_/  >  |   |  \/ /_/  >
 *  |_______ \____/\___  /\___  /|__|___|  /\___  / 
 *          \/    /_____//_____/         \//_____/  
 */
namespace Pulsar.Core.Events.Bot
{
    class Logging
    {
        /*
         * Logging
         * 
         * Logs messages from Pulsar.cs
         * 
         * Set LogLevel in Pulsar.cs
         * Options: Critical | Debug | Error | Info | Verbose | Warning
         */
        public static async Task OnLog(LogMessage Message)
        {
            // Log dat shit
            Logger.Log($"[{DateTime.Now} at {Message.Source}] {Message.Message}");
        }
    }
}
