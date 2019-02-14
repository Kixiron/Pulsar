using System;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

using Pulsar.Core.Utils;
using Pulsar.Core.KixLog;


/*
 *    ________.__               
 *   /  _____/|__|__  __ ____   
 *  /   \  ___|  \  \/ // __ \  
 *  \    \_\  \  |\   /\  ___/  
 *   \______  /__| \_/  \___  > 
 *          \/              \/  
 */
namespace Pulsar.Core.Commands.Economy
{
    public class GiveMoney : ModuleBase<ShardedCommandContext>
    {
        /*
         * Transfer money from one user to another
         * 
         * Check that message is in server
         * Check for user mention
         * Check that user is bot
         * Check for valid amount
         * Check both user's balances
         * Check that giver has enough money
         * Send message to channel
         * Transfer money in db
         */
        [Command("give-money"), Alias("transfer-money"), Summary("Give money to another user")]
        public async Task GiveCommand(IUser User = null, int Amount = 0)
        {
            string Prefix = PulsarClient.Config.Prefix[0];
            ISocketMessageChannel Channel = Context.Channel;

            // Check for text channel
            if (!(Context.Channel is ITextChannel))
            {
                await Context.Channel.SendMessageAsync($"Sorry {Context.User.Username}, but that command only works in a server!");
                return;
            }
            // Check for user mention
            if (User == null)
            {
                await Context.Channel.SendMessageAsync($"Hey {Context.User.Username}, you need to mention a user to give money to!\n" +
                                                       $"Try {Prefix}give <@user> <amount>");
                return;
            }
            // Cannot transfer to bots
            if (User.IsBot)
            {
                await Context.Channel.SendMessageAsync($"Sorry {Context.User.Username}, but you can't give money to a bot!");
                return;
            }
            // Check for valid amount
            if (Amount <= 0)
            {
                await Context.Channel.SendMessageAsync($"Hey {Context.User.Username}, you need to specify a valid amount of money to give to {User.Username}");
                return;
            }

            // Check both user's balance
            int GiverBal, ReceiverBal; GiverBal = ReceiverBal = 0;
            try
            {
                ReceiverBal = Data.GetCurrency(Context.Guild.Id, User.Id);
                GiverBal = Data.GetCurrency(Context.Guild.Id, Context.User.Id);
            }
            // Catch db error
            catch (Exception error)
            {
                await Context.Channel.SendMessageAsync($"Uh oh! Something went wrong!");
                Logger.Log($"[{DateTime.Now} at Economy] Error Fetching Data: {error}");
                return;
            }
            // Check if the giver has enough money
            if (GiverBal < Amount)
            {
                await Context.Channel.SendMessageAsync($"Sorry {Context.User.Username}, but you don't have enough money to do that!");
                return;
            }

            // Send message
            await Channel.SendMessageAsync($"{Context.User.Username} has given {User.Username} ${Amount}!");

            // Transfer money
            try
            {
                Data.SaveCurrency(Context.Guild.Id, User.Id, Amount); // Mentioned User given amount
                Data.SaveCurrency(Context.Guild.Id, Context.User.Id, -Amount); // Command user given negative amount
            }
            // Catch db error
            catch (Exception error)
            {
                await Channel.SendMessageAsync($"Uh oh! Something went wrong!");
                Logger.Log($"[{DateTime.Now} at Economy] Error Saving Data: {error}");
            }
        }
    }
}
