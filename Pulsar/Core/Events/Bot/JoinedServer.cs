using System;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;

using Pulsar.Core.Utils;
using Pulsar.Core.KixLog;


/*
 *    _________                                      ____.      .__        
 *   /   _____/ ______________  __ ___________      |    | ____ |__| ____  
 *   \_____  \_/ __ \_  __ \  \/ // __ \_  __ \     |    |/  _ \|  |/    \ 
 *   /        \  ___/|  | \/\   /\  ___/|  | \/ /\__|    (  <_> )  |   |  \
 *  /_______  /\___  >__|    \_/  \___  >__|    \________|\____/|__|___|  /
 *          \/     \/                 \/                                \/ 
 */
namespace Pulsar.Core.Events.Bot
{
    public class JoinedServer
    {
        /*
         * On Server Join
         * 
         * Create entry in DB for server
         * Send Message to Server Log Channel
         * Report to Logger
         * Update Status
         */
        public static async Task OnServerJoin(SocketGuild Server, DiscordShardedClient Client)
        {
            Data.CreateServerDb(Server.Id); // Make Server entry in db

            SocketTextChannel ServerLogChannel = Client.GetChannel(PulsarClient.Config.ServerLogChannelID) as SocketTextChannel; // Fetch and Cast Server Log Channel

            // Build Embed
            EmbedBuilder Embed = new EmbedBuilder()
            {
                Title = $"Joined {Server.Name} ({Server.Id})",
                Description = $"Owner: {Server.Owner.Username}#{Server.Owner.Discriminator}\n" +
                              $"Members: {Server.MemberCount}",
                ThumbnailUrl = Server.IconUrl,
                Timestamp = DateTime.Now
            };
            Embed.WithColor(142, 44, 208);

            // Send embed
            await ServerLogChannel.SendMessageAsync("", false, Embed.Build());

            Logger.Log($"[{DateTime.Now} at Server] Joined Server: {Server.Name}");

            await Stats.UpdatePulsarStatus(Client); // Update Status
        }
    }
}
