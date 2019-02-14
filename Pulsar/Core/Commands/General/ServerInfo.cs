using System;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;


/*
 *    _________                                 .___        _____       
 *   /   _____/ ______________  __ ___________  |   | _____/ ____\____  
 *   \_____  \_/ __ \_  __ \  \/ // __ \_  __ \ |   |/    \   __\/  _ \ 
 *   /        \  ___/|  | \/\   /\  ___/|  | \/ |   |   |  \  | (  <_> )
 *  /_______  /\___  >__|    \_/  \___  >__|    |___|___|  /__|  \____/ 
 *          \/     \/                 \/                 \/             
 */
namespace Pulsar.Core.Commands.General
{
    public class ServerInfo : ModuleBase<ShardedCommandContext>
    {
        /*
         * Information on the current server
         * 
         * Check that the message is in a server
         * Send message to channel with server info
         */
        [Command("server-info"), Alias("serverinfo", "sinfo"), Summary("Display Server info")]
        public async Task ServerInfoCommand()
        {
            // Check that channel is in a server
            if (!(Context.Channel is ITextChannel))
            {
                await Context.Channel.SendMessageAsync($"Sorry {Context.User.Username}, but that command only works in a server!");
                return;
            }

            SocketGuild Server = Context.Guild;

            // Build embed
            EmbedBuilder Embed = new EmbedBuilder
            {
                Title = Server.Name,
                Description = $"Since {String.Format("{0:M/d/yyyy}", Server.CreatedAt)}. That's over {(DateTime.Now - Server.CreatedAt).Days} days ago!",
                Timestamp = Context.Message.Timestamp,
                ThumbnailUrl = Server.IconUrl
            };
            Embed.WithColor(142, 44, 208);
            Embed.WithFooter($"Server ID: {Server.Id}");
            Embed.AddField("Owner", $"{Server.Owner.Username}#{Server.Owner.Discriminator}");
            Embed.AddField("Total Users", Server.Users.Count);
            Embed.AddField("Total Channels", $"{Server.Channels.Count} ({Server.TextChannels.Count} text, {Server.VoiceChannels.Count} voice)");
            Embed.AddField("Total Roles", Server.Roles.Count);
            Embed.AddField("Region", Server.VoiceRegionId);

            // Send embed
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }
    }
}
