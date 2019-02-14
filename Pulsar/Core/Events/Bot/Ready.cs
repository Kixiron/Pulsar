using System;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;

using Pulsar.Core.Utils;
using Pulsar.Core.KixLog;

/*
 *  __________                   .___      
 *  \______   \ ____ _____     __| _/__.__.
 *   |       _// __ \\__  \   / __ <   |  |
 *   |    |   \  ___/ / __ \_/ /_/ |\___  |
 *   |____|_  /\___  >____  /\____ |/ ____|
 *          \/     \/     \/      \/\/     
 */
namespace Pulsar.Core.Events.Bot
{
    public class Ready
    {
        /*
         * On Shard Ready
         * 
         * Ready message to Logger
         * Update Status
         * Send message to Login Channel
         */
        public static async Task OnShardReady(DiscordSocketClient SocketClient, DiscordShardedClient Client)
        {
            Logger.Log($"[{DateTime.Now} at Shard #{SocketClient.ShardId + 1}] Shard #{SocketClient.ShardId + 1} Ready ({SocketClient.ShardId + 1}/{Client.Shards.Count})");

            int Servers, Members;
            Servers = Client.Guilds.Count;
            Members = 0;
            foreach (SocketGuild server in Client.Guilds)
            {
                Members += server.MemberCount;
            }

            await SocketClient.SetGameAsync($"!help | {Servers} Servers | {Members} users");
            Logger.Log($"[{DateTime.Now}] Set Status: [!help | {Servers} Servers | {Members} users]");

            if (SocketClient.ShardId == 0)
            {
                SocketSelfUser Pulse = Client.CurrentUser; // Bot user object
                SocketUser Owner = Client.GetUser(PulsarClient.Config.OwnerID); // Owner user object

                string Invite = $"https://discordapp.com/api/oauth2/authorize?client_id={Pulse.Id}&permissions=8&scope=bot";
                Tuple<int, int, int> StatNumbers = Stats.PulsarStats(Client);

                Logger.Log("==========================================");
                Logger.Log($"[Successful login at {DateTime.Now}]");
                Logger.Log();
                Logger.Log($"Logged in as {Pulse.Username}#{Pulse.Discriminator} ({Pulse.Id})");
                Logger.Log($"Owner: {Owner.Username}#{Owner.Discriminator} ({Owner.Id})");
                Logger.Log($"Total Shards: {Client.Shards.Count}");
                Logger.Log($"Total Guilds: {StatNumbers.Item1}");
                Logger.Log($"Total Channels: {StatNumbers.Item2}");
                Logger.Log($"Total Members: {StatNumbers.Item3}");
                Logger.Log($"Invite link: {Invite}");
                Logger.Log();
                Logger.Log("==========================================");
            }
        }
    }
}
