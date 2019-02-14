using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

using Pulsar.Core.KixLog;

namespace Pulsar.Core.Commands.General
{
    public class UserInfo : ModuleBase<ShardedCommandContext>
    {
        /*
         * Gives info on a user
         * 
         * Checks for command to be sent to a server (May not be needed)
         * Default to command user if no user mentioned
         * Build message
         * Send message to channel
         * 
         */
        [Command("user-info"), Alias("userinfo"), Summary("Information on a user")]
        public async Task UserInfoCommand(IUser User = null)
        {
            try
            {
                // Check that channel is in a server
                if (!(Context.Channel is ITextChannel))
                {
                    await Context.Channel.SendMessageAsync($"Sorry {Context.User.Username}, but that command only works in a server!");
                    return;
                }

                // If no user mentioned, default to the command user
                if (User == null)
                {
                    User = Context.User;
                }
                SocketGuildUser Member = Context.Guild.GetUser(User.Id);

                // Build embed
                EmbedBuilder Embed = new EmbedBuilder
                {
                    Timestamp = Context.Message.Timestamp,
                    ThumbnailUrl = Member.GetAvatarUrl()
                };
                Embed.WithColor(142, 44, 208); // Color
                // Joined Discord on
                Embed.AddField("Joined Discord On:", $"{User.CreatedAt.Date}\n({(DateTime.Now - User.CreatedAt.Date).Days} days ago)", true);
                // Joined Server on
                Embed.AddField("Joined This Server On:", $"{Member.JoinedAt.Value.Date}\n({(DateTimeOffset.UtcNow - Member.JoinedAt).Value.Days} days ago)", true);

                // Title with or without nickname
                string Title = "";
                if (Member.Nickname != null)
                {
                    Title = $"{User.Username}#{User.Discriminator} ({Member.Nickname})";
                }
                else
                {
                    Title = $"{User.Username}#{User.Discriminator}";
                }

                // Set activity
                if (Member.Activity != null)
                {
                    switch (User.Activity.Type)
                    {
                        case ActivityType.Listening:
                            Embed.WithDescription($"<:Music:528358185124495360> {User.Activity.Type} to **{User.Activity}**");
                            break;
                        case ActivityType.Playing:
                            Embed.WithDescription($"<:Playing:529762442348068885> {User.Activity.Type} **{User.Activity}**");
                            break;
                        case ActivityType.Streaming:
                            Embed.WithDescription($"<:Streaming:529761314747187200> {User.Activity.Type} **{User.Activity}**");
                            break;
                        case ActivityType.Watching:
                            Embed.WithDescription($"<:Watching:529762080421838858> {User.Activity.Type} **{User.Activity}**");
                            break;
                    }
                }

                // Set the user author by Status
                switch (User.Status)
                {
                    case UserStatus.Online:
                        Embed.WithTitle("<:online:529758896236134410> " + Title);
                        break;
                    case UserStatus.Idle:
                        Embed.WithTitle("<:idle:529758896269819914> " + Title);
                        break;
                    case UserStatus.DoNotDisturb:
                        Embed.WithTitle("<:dnd:529758906327629834>" + Title);
                        break;
                    case UserStatus.Offline:
                        Embed.WithTitle("<:offline:529758896248717321> " + Title);
                        break;
                    case UserStatus.Invisible:
                        Embed.WithTitle("<:offline:529758896248717321> " + Title);
                        break;
                }

                // Add role list
                List<string> RoleMsg = new List<string>(); // Make list of strings
                foreach (SocketRole Role in Member.Roles)
                {
                    RoleMsg.Add("**" + Role.Name + "**"); // Add bolded role name
                }
                Embed.AddField("Roles", string.Join(" | ", RoleMsg.ToArray()), false); // Join every role with " | "

                // If Bot, say its a bot + ID as footer, if not ID as footer
                if (Member.IsBot || Member.IsWebhook || User.IsBot || User.IsWebhook) // Cover all the bases
                {
                    Embed.WithFooter($"Beep Boop, {User.Username} is a bot! • ID: {User.Id}");
                }
                else
                {
                    Embed.WithFooter($"ID: {User.Id}");
                }

                // Send embed
                await Context.Channel.SendMessageAsync("", false, Embed.Build());
            }
            catch (Exception error)
            {
                Logger.Log($"[{DateTime.Now} at UserInfo] Error: {error}");
            }
        }
    }
}
