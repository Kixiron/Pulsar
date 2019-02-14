using System;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;

using Pulsar.Core.KixLog;
using Pulsar.Core.Utils;

namespace Pulsar.Core.Events.Bot
{
    public class LeftServer
    {
        /*
         * On Server Leave
         * 
         * Send Message to Server Log Channel
         * Report to Logger
         * Update Status
         */
        public static async Task OnServerLeave(SocketGuild Server, DiscordShardedClient Client)
        {
            SocketTextChannel ServerLogChannel = Client.GetChannel(PulsarClient.Config.ServerLogChannelID) as SocketTextChannel; // Fetch and Cast Server Log Channel

            // Build Embed
            EmbedBuilder Embed = new EmbedBuilder()
            {
                Title = $"Left {Server.Name} ({Server.Id})",
                Description = $"Owner: {Server.Owner.Username}#{Server.Owner.Discriminator}\n" +
                              $"Members: {Server.MemberCount}",
                ThumbnailUrl = Server.IconUrl,
                Timestamp = DateTime.Now
            };
            Embed.WithColor(142, 44, 208);

            // Send embed
            await ServerLogChannel.SendMessageAsync("", false, Embed.Build());

            Logger.Log($"[{DateTime.Now} at Server] Left Server: {Server.Name}");

            await Stats.UpdatePulsarStatus(Client); // Update Status
        }
    }
}
