using System;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

using Pulsar.Core.Utils;


/*
 *  .___        _____       
 *  |   | _____/ ____\____  
 *  |   |/    \   __\/  _ \ 
 *  |   |   |  \  | (  <_> )
 *  |___|___|  /__|  \____/ 
 *          \/             
 */
namespace Pulsar.Core.Commands.Bot
{
    public class Info : ModuleBase<ShardedCommandContext>
    {
        /*
         * Some info on Pulsar
         * 
         * Get Owner
         * Get Bot Stats
         * Send message to channel
         * 
         * TODO: Make the message more comprehensive, along with adding
         * some more bot stats
         */
        [Command("info"), Alias("botinfo", "pulsarinfo"), Summary("Gives information on Pulsar")]
        public async Task InfoCommand()
        {
            SocketUser Owner = Utils.Owner.GetOwnerObj(Context); // Fetch Owner
            Tuple<int, int, int> BotStats = Stats.PulsarStats(Context.Client); // fetch Pulsar Stats

            EmbedBuilder Embed = new EmbedBuilder()
            {
                /*
                 * Info Embed
                 * 
                 * Pulsar Info (Thumbnail of avatar)
                 * 
                 * Hello! I'm Pulsar!
                 * I was programmed in 100% locally sourced C# (C# Emote)
                 * Join the support server here (Link)
                 * Shard Number: Shard Number (Shard X out of X)
                 * Total Servers: X
                 * Total Users: X
                 * Total Channels: X
                 * I'm built on the Discord.net API by Kixiron
                 * 
                 * Made with ❤️ by Kixiron
                 */
                Title = $"{Context.Client.CurrentUser.Username} Info",
                Description = "Hello! I'm Pulsar!\n" +
                              "I was programmed in 100% locally sourced C# <:CSharp:528359235919151116>\n" +
                              $"Join the support server [here]({PulsarClient.Config.SupportServerInvite})\n" +
                              $"Shard Number: {Context.Client.GetShardIdFor(Context.Guild) + 1} (Shard {Context.Client.GetShardIdFor(Context.Guild) + 1} out of {Context.Client.Shards.Count})\n" +
                              $"Total Servers: {BotStats.Item1}\n" +
                              $"Total Users: {BotStats.Item3}\n" +
                              $"Total Channels: {BotStats.Item2}\n" +
                              $"I'm built on the [Discord.net API](https://github.com/RogueException/Discord.Net) by {Owner.Username}\n",
                ThumbnailUrl = Context.Client.CurrentUser.GetAvatarUrl(),
            };
            Embed.WithFooter($"Made with ❤️ by {Owner.Username}");
            Embed.WithColor(142, 44, 208);

            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }
    }
}
