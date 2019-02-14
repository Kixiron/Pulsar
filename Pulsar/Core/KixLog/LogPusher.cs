using System;
using System.IO;

using Discord.WebSocket;


/*
 *  .____                   __________             .__                  
 *  |    |    ____   ____   \______   \__ __  _____|  |__   ___________ 
 *  |    |   /  _ \ / ___\   |     ___/  |  \/  ___/  |  \_/ __ \_  __ \
 *  |    |__(  <_> ) /_/  >  |    |   |  |  /\___ \|   Y  \  ___/|  | \/
 *  |_______ \____/\___  /   |____|   |____//____  >___|  /\___  >__|   
 *          \/    /_____/                        \/     \/     \/       
 */
namespace Pulsar.Core.KixLog
{
    /* 
     * Lots of try/catches here, file handling scares me and if these dudes fail they like to errorlessly brick the bot.
     * 
     * For all errors here I decided to use Console.WriteLine() instead of Logger.Log() because I don't want to get into
     * a feedback loop of error logging.
     * 
     * The FilePath string should be different every day, and so should seamlessly rotate the logs on a daily basis.
     * 
     * Writer.Write() appends the file, so no reading/storage of the previous logs is needed.
     * 
     * WARINING: Never use Logger.Log() in this file. It can and will thrust the bot into a loop of errors at worst,
     * or render the whole "logging to file" part of things useless.
     * 
     * NOTE: Sharding makes this a whole lot more fun
     * NOTE: It might actually be fine, not really sure tbh
     * NOTE: It does appear to matter, so I made it make separate files for each Shard
     * NOTE: I have no way to reliably get the shard id from each log pushed
     */

    public class LogPusher
    {
        private static string Chunk = "\n\n==============================================\n" +
        /* Heads the first Chunk */   $"Started Logging Session for Pulsar\n" +
        /* with a fancy header */     "==============================================\n\n";

        // Log File path- Makes new log file every day
        private static readonly string FilePath = $"G:/Local File Server/Programming/C#/Pulsar/Pulsar/Core/Data/Logs/[{string.Format("{0:MMddyyyy}", DateTime.Now)}]-Log.txt";

        // Adds Chunk onto Logger.Chunk, if Logger.Chunk over 2000 char, Write it
        public static void PushLogs(string Chunk)
        {
            LogPusher.Chunk += Chunk + "\n";

            // If over 2000 char, write to Log file
            if (LogPusher.Chunk.Length >= 2000)
            {
                try
                {
                    WriteChunk(); // Write Logger.Chunk
                    LogPusher.Chunk = ""; // Reset Logger.Chunk
                }
                catch (Exception error)
                {
                    Console.WriteLine($"[{DateTime.Now} at LogPusher] Failed to write chunk: {error}");
                }
            }
        }

        // Creates Log file if needed, writes Logger.Chunk onto Log File
        private static void WriteChunk()
        {
            try
            {
                // Write Current Chunk to file
                using (StreamWriter Writer = new StreamWriter(FilePath, true))
                {
                    try
                    {
                        Writer.Write(Chunk); // Write Chunk
                        Console.WriteLine($"[{DateTime.Now} at Logging] Wrote Log Chunk"); // Feel safe writing a successful Write to the Logger
                    }
                    catch (Exception error)
                    {
                        Console.WriteLine($"[{DateTime.Now} at LogPusher] Failed to write chunk: {error}");
                    }
                }
            }
            catch (Exception error)
            {
                Console.WriteLine($"[{DateTime.Now} at LogPusher] Failed to push log: {error}");
            }
        }
    }
}

