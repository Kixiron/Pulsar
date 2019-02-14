using System.Threading.Tasks;

using Discord.Commands;
using Discord.WebSocket;

using Pulsar.Core.Utils;


/*
 *    _________                               .__  .__          __   
 *   /   _____/ ______________  __ ___________|  | |__| _______/  |_ 
 *   \_____  \_/ __ \_  __ \  \/ // __ \_  __ \  | |  |/  ___/\   __\
 *   /        \  ___/|  | \/\   /\  ___/|  | \/  |_|  |\___ \  |  |  
 *  /_______  /\___  >__|    \_/  \___  >__|  |____/__/____  > |__|  
 *          \/     \/                 \/                   \/        
 */
namespace Pulsar.Core.Commands.Owner
{
    public class ServerList : ModuleBase<ShardedCommandContext>
    {
        /* 
         * Lists servers that Pulsar is in
         * 
         * Filters users other than Owner
         * Collects Servers
         * Adds entry to message for each server with server name and server ID
         * Sends message to channel
         */
        [Command("serverlist"), Alias("listservers"), Summary("List all servers that Pulsar is in")]
        public async Task ServerListCommand()
        {
            if (!Checks.IsOwner(Context.User.Id)) return; // Filter other users

            System.Collections.Generic.IReadOnlyCollection<SocketGuild> Servers = Context.Client.Guilds;

            // Start content string with header
            string Content = $"**{Context.Client.CurrentUser.Username} Servers**\n" +
                             $"**Total Servers:** {Stats.PulsarServerCount(Context.Client)}\n";

            // For each server, add a line of info
            int ServerCount = 0;
            foreach (SocketGuild Server in Servers)
            {
                ServerCount += 1;
                Content += $"[{ServerCount}] {Server.Name} ({Server.Id})\n";
            }

            // Send message
            await Context.Channel.SendMessageAsync(Content);
        }
    }
}
