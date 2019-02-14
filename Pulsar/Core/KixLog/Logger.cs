using System;
using System.IO;
using System.IO.Pipes;


/*
 *  .____                                      
 *  |    |    ____   ____   ____   ___________ 
 *  |    |   /  _ \ / ___\ / ___\_/ __ \_  __ \
 *  |    |__(  <_> ) /_/  > /_/  >  ___/|  | \/
 *  |_______ \____/\___  /\___  / \___  >__|   
 *          \/    /_____//_____/      \/       
 */
namespace Pulsar.Core.KixLog
{
    public class Logger
    {
        // Basically Console.WriteLine(), but it also writes to a file.
        // See LogPusher.cs for the real innards
        public static void Log(string Message = "") // Default to empty string for newline
        {
            /*
            try
            {
                LogPusher.PushLogs(Message); // Push message to logger
            }
            catch (Exception error)
            {
                Console.WriteLine($"[{DateTime.Now} at Logger] failed to push message to Logger: {error}");
            }
            */

            // Have Console.WriteLine() be outside of the try/catch block so that errors are always logged to console
            Console.WriteLine(Message); // Write message to console
        }
    }
}
