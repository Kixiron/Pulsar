using System.Threading.Tasks;
using System.Collections.Generic;

using Discord;
using Discord.Rest;
using Discord.WebSocket;

using Pulsar.Core.Events.Server;


/*
 *   ____ ___                     .____                              
 *  |    |   \______ ___________  |    |    ____ _____ ___  __ ____  
 *  |    |   /  ___// __ \_  __ \ |    |  _/ __ \\__  \\  \/ // __ \ 
 *  |    |  /\___ \\  ___/|  | \/ |    |__\  ___/ / __ \\   /\  ___/ 
 *  |______//____  >\___  >__|    |_______ \___  >____  /\_/  \___  >
 *               \/     \/                \/   \/     \/          \/ 
 */
namespace Pulsar.Core.Events.User
{
    public class UserLeave
    {
        public static async Task OnUserLeave(SocketGuildUser User, DiscordShardedClient Client)
        {
            SocketGuild Server = Client.GetGuild(User.Guild.Id);
            IEnumerable<RestAuditLogEntry> AuditLogs = await Server.GetAuditLogsAsync(50).FlattenAsync();

            foreach (RestAuditLogEntry Entry in AuditLogs)
            {
                switch (Entry.Action)
                {
                    case ActionType.Ban:
                        await ServerBan.UpdateServerBan(Server.Id, Client, Entry);
                        break;
                    case ActionType.Kick:
                        await ServerKick.OnServerKick(Server.Id, Client);
                        break;
                }
            }
        }
    }
}
