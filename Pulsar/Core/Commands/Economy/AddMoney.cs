using System;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

using Pulsar.Core.Utils;
using Pulsar.Core.KixLog;


/*
 *  ___________                                         
 *  \_   _____/ ____  ____   ____   ____   _____ ___.__.
 *   |    __)__/ ___\/  _ \ /    \ /  _ \ /     <   |  |
 *   |        \  \__(  <_> )   |  (  <_> )  Y Y  \___  |
 *  /_______  /\___  >____/|___|  /\____/|__|_|  / ____|
 *          \/     \/           \/             \/\/     
 */
namespace Pulsar.Core.Commands.Economy
{
    public class AddMoney : ModuleBase<ShardedCommandContext>
    {
        /*
         * Admins can add money to a user's account
         * 
         * Check that channel is in server
         * Check for user perms (See TODO)
         * Check for valid mention
         * Check that user isn't a bot
         * Check for valid amount of money
         * Send message to channel
         * Save user's cash in db
         * 
         * TODO: Make the server-set Mod and Admin roles also have this ability
         */
        [Command("add-money"), Alias("add-cash"), Summary("Add money to a user's account")]
        public async Task AddCommand(IUser User = null, int Amount = 0)
        {
            // Set up variables
            string Prefix = PulsarClient.Config.Prefix[0]; // Prefix string
            ISocketMessageChannel Channel = Context.Channel; // Channel = Context.Channel

            // Check for Text channel in server
            if (!(Context.Channel is ITextChannel))
            {
                await Context.Channel.SendMessageAsync($"Sorry {Context.User.Username}, but that command only works in a server!");
                return;
            }
            SocketGuildUser PermCheck = Context.User as SocketGuildUser; // Make a temp SocketGuildUser to check perms for
            // Check for Manage Server perm
            if (!PermCheck.GuildPermissions.ManageGuild)
            {
                await Channel.SendMessageAsync($"Sorry {Context.User.Username}, but you don't have permission to do that!");
                return;
            }
            // Check for valid user
            if (User == null)
            {
                await Channel.SendMessageAsync($"Hey {Context.User.Username}, you need to mention a user to give money to!\n" +
                                               $"Try {Prefix}give <@user> <amount>");
                return;
            }
            // Cannot add money to bots
            if (User.IsBot)
            {
                await Channel.SendMessageAsync($"Sorry {Context.User.Username}, but you can't give money to a bot!");
                return;
            }
            // Check for valid amount on money
            if (Amount <= 0)
            {
                await Channel.SendMessageAsync($"Hey {Context.User.Username}, you need to specify a valid amount of money to give to {User.Username}");
                return;
            }

            // Send message to channel
            await Channel.SendMessageAsync($"{Context.User.Mention} has added ${Amount} to {User.Mention}'s account!");

            // Save the user's cash
            try
            {
                Data.SaveCurrency(Context.Guild.Id, User.Id, Amount);
            }
            // Catch db error
            catch (Exception error)
            {
                await Channel.SendMessageAsync($"Uh oh! Something went wrong!");
                Logger.Log($"[{DateTime.Now} at Economy] Error Saving Data: {error}");
                return;
            }
        }
    }
}
