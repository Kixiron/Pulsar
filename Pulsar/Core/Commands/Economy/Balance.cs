using System;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

using Pulsar.Core.Utils;
using Pulsar.Core.KixLog;


/*
 *  __________        .__                              
 *  \______   \_____  |  | _____    ____   ____  ____  
 *   |    |  _/\__  \ |  | \__  \  /    \_/ ___\/ __ \ 
 *   |    |   \ / __ \|  |__/ __ \|   |  \  \__\  ___/ 
 *   |______  /(____  /____(____  /___|  /\___  >___  >
 *          \/      \/          \/     \/     \/    \/ 
 */
namespace Pulsar.Core.Commands.Economy
{
    public class Balance : ModuleBase<ShardedCommandContext>
    {
        /*
         * Check a user's Bank Balance
         * 
         * Check that message is in server (Actually needed, cash in per-server. Maybe add a server select function later?)
         * Check for user mention, default to message sender
         * Check user's cash in db
         * Send message to channel
         */
        [Command("balance"), Alias("bal"), Summary("Shows your bank balance")]
        public async Task BalanceCommand(IUser User = null)
        {
            // Check for text channel
            if (!(Context.Channel is ITextChannel))
            {
                await Context.Channel.SendMessageAsync($"Sorry {Context.User.Username}, but that command only works in a server!");
                return;
            }
            
            // If no user mentioned, use the current user
            if (User == null)
            {
                User = Context.User;
                // Fetch user's balance
                int UserBal;
                try
                {
                    UserBal = Data.GetCurrency(Context.Guild.Id, User.Id);
                }
                // Catch db error
                catch (Exception error)
                {
                    await Context.Channel.SendMessageAsync($"Uh oh! Something went wrong!");
                    Logger.Log($"[{DateTime.Now} at Economy] Error Fetching Data: {error}");
                    return;
                }

                // Return balance
                await Context.Channel.SendMessageAsync($"{User.Username}, you have ${UserBal}!");
            }
            // Same thing, different end string
            else
            {
                User = Context.User;
                // Fetch user's balance
                int UserBal;
                try
                {
                    UserBal = Data.GetCurrency(Context.Guild.Id, User.Id);
                }
                // Catch db error
                catch (Exception error)
                {
                    await Context.Channel.SendMessageAsync($"Uh oh! Something went wrong!");
                    Logger.Log($"[{DateTime.Now} at Economy] Error Fetching Data: {error}");
                    return;
                }

                // Return balance
                await Context.Channel.SendMessageAsync($"{User.Username} has ${UserBal}!");
            }
        }
    }
}
