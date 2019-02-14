using System;
using System.IO;
using System.Collections.Generic;

using Newtonsoft.Json;

using Pulsar.Core.KixLog;


/*
 *       ____. _________________    _______   
 *      |    |/   _____/\_____  \   \      \  
 *      |    |\_____  \  /   |   \  /   |   \ 
 *  /\__|    |/        \/    |    \/    |    \
 *  \________/_______  /\_______  /\____|__  /
 *                   \/         \/         \/ 
 */
namespace Pulsar.Core.Config
{
    // JSON is initialized/loaded in Pulsar.cs
    public class Configuration
    {
        // Config class where data is stored
        public class Config
        {
            public string Token; // Token
            public List<string> Prefix; // Prefixes, when displaying one to client, default to Prefix[0]
            public ulong OwnerID; // Owner ID
            public string Version; // Bot Version
            public ulong TicketChannelID; // Ticket Channel ID
            public ulong ServerLogChannelID; // Server Log ID
            public ulong LoginChannelID; // Login Channel ID
            public ulong AlertChannelID; // Alert Channel ID
            public ulong SupportServerID; // Support Server ID
            public string SupportServerInvite; // Support Server Invite
            public bool AcceptMention; // Accept mentions for a prefix boolean
            public int ShardNumber; // Number of shards
        }

        // Path to config file
        private static readonly string ConfigPath = "G:/Local File Server/Programming/C#/Pulsar/Pulsar/Core/Data/config.json";

        // Load Config.json into Config class
        public static Config LoadConfig()
        {
            string Raw = "";

            try
            {
                // Open config.json for reading
                using (StreamReader Reader = new StreamReader(ConfigPath))
                {
                    Raw = Reader.ReadToEnd(); // Read config.json and load it into Raw
                }
            }
            catch (Exception error)
            {
                Logger.Log($"[{DateTime.Now} at JSON] Failed to load Config file: {error}");
            }

            Config Config = JsonConvert.DeserializeObject<Config>(Raw); // Deserialize json into Config class
            Logger.Log($"[{DateTime.Now} at JSON] Loaded Config file"); // Report to log
            return Config; // Return the loaded config
        }
    }
}
