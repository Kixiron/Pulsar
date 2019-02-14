using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Discord;
using Discord.Rest;
using Discord.Commands;
using Discord.WebSocket;

using Pulsar.Core.Utils;


/*
 *    _________                               .__        _____       
 *   /   _____/ ______________  __ ___________|__| _____/ ____\____  
 *   \_____  \_/ __ \_  __ \  \/ // __ \_  __ \  |/    \   __\/  _ \ 
 *   /        \  ___/|  | \/\   /\  ___/|  | \/  |   |  \  | (  <_> )
 *  /_______  /\___  >__|    \_/  \___  >__|  |__|___|  /__|  \____/ 
 *          \/     \/                 \/              \/             
 */
namespace Pulsar.Core.Commands.Owner
{
    public class OwnerServerInfo : ModuleBase<ShardedCommandContext>
    {
        /*
         * Gives detailed server info to owner (By ID)
         * 
         * Filters users other than owner
         * Checks for valid server id
         * Finds server
         * Sends embed to channel
         */
        [Command("ownerserverinfo"), Summary("Server info for the Owner")]
        public async Task OwnerServerInfoCommand(ulong ServerID = 0)
        {
            if (!Checks.IsOwner(Context.User.Id)) return; // Filter other users

            // Check for valid server id
            if (ServerID == 0)
            {
                await Context.Channel.SendMessageAsync("You need to give a valid Server ID.");
                return;
            }

            IEnumerable<SocketGuild> ServerFilter = Context.Client.Guilds.Where(x => x.Id == ServerID); // Find servers with the matching ServerID
            // If no matches, cannot find server
            if (ServerFilter.Count() < 1)
            {
                await Context.Channel.SendMessageAsync("I'm sorry, but I can't find that server!");
                return;
            }

            SocketGuild Server = ServerFilter.FirstOrDefault(); // Select the first valid server
            // Build embed
            /*
             * Owner: Username#Discriminator
             * Created At: Created date (Days old)
             * Users: User Count
             * Channels: Channel Count (X text, X voice)
             * Roles: Role Count
             * Verification Level: Verification level
             * MFA Level: MFA Level
             * Region: Voice Region
             * Emotes: Emote Count
             * Invites: Invite Count
             */
            IReadOnlyCollection<RestInviteMetadata> Invites = await Server.GetInvitesAsync();
            EmbedBuilder Embed = new EmbedBuilder
            {
                Title = $"{Server.Name} ({Server.Id})",
                Timestamp = Context.Message.Timestamp,
                ThumbnailUrl = Server.IconUrl,
                Description = $"Owner: {Server.Owner.Username}#{Server.Owner.Discriminator}\n" +
                              $"Created at: {Server.CreatedAt} ({(DateTime.Now - Server.CreatedAt).Days} Days ago)\n" +
                              $"Users: {Server.Users.Count}\n" +
                              $"Channels: {Server.Channels.Count} ({Server.TextChannels.Count} text, {Server.VoiceChannels.Count} voice)\n" +
                              $"Roles: {Server.Roles.Count}\n" +
                              $"Verification Level: {Server.VerificationLevel}\n" +
                              $"MFA Level: {Server.MfaLevel}\n" +
                              $"Region: {Server.VoiceRegionId}\n" +
                              $"Emotes: {Server.Emotes.Count}\n" +
                              $"Invites: {Invites.Count}"
            };
            Embed.WithColor(142, 44, 208);

            // Send embed
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }
    }
}
