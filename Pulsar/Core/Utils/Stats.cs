using System;
using System.Threading.Tasks;

using Discord.WebSocket;

using Pulsar.Core.KixLog;


/*
 *    _________ __          __          
 *   /   _____//  |______ _/  |_  ______
 *   \_____  \\   __\__  \\   __\/  ___/
 *   /        \|  |  / __ \|  |  \___ \ 
 *  /_______  /|__| (____  /__| /____  >
 *          \/           \/          \/ 
 */
namespace Pulsar.Core.Utils
{
    public class Stats
    {
        // Updates Pulsar's status from anywhere
        public static async Task UpdatePulsarStatus(DiscordShardedClient Client)
        {
            Tuple<int, int, int> BotStats = PulsarStats(Client);
            await Client.SetGameAsync($"!help | {BotStats.Item1} Servers | {BotStats.Item3} users");
            Logger.Log($"[{DateTime.Now}] Set Status: [!help | {BotStats.Item1} Servers | {BotStats.Item3} users]");
        }

        // Tuple that returns Server Count, Channel Count, and Member Count
        public static Tuple<int, int, int> PulsarStats(DiscordShardedClient Client)
        {
            int Servers, Channels, Members;
            Servers = Client.Guilds.Count;
            Channels = Members = 0;
            foreach (SocketGuild server in Client.Guilds)
            {
                Members += server.MemberCount;
                Channels += server.Channels.Count;
            }
            return new Tuple<int, int, int>(Servers, Channels, Members);
        }

        // Returns only Server Count
        public static int PulsarServerCount(DiscordShardedClient Client)
        {
            return Client.Guilds.Count;
        }

        // Returns only Channel Count
        public static int PulsarChannelCount(DiscordShardedClient Client)
        {
            int Channels = 0;
            foreach (SocketGuild server in Client.Guilds)
            {
                Channels += server.Channels.Count;
            }
            return Channels;
        }

        // Returns only User Count
        public static int PulsarUserCount(DiscordShardedClient Client)
        {
            int Members = 0;
            foreach (SocketGuild server in Client.Guilds)
            {
                Members += server.MemberCount;
            }
            return Members;
        }
    }
}
