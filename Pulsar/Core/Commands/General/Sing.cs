using System;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

using Pulsar.Core.KixLog;


/*
 *    _________.__                
 *   /   _____/|__| ____    ____  
 *   \_____  \ |  |/    \  / ___\ 
 *   /        \|  |   |  \/ /_/  >
 *  /_______  /|__|___|  /\___  / 
 *          \/         \//_____/  
 */
namespace Pulsar.Core.Commands.General
{
    /*
     * Sings Daisy Bell, first song sung by computer
     * Also a HAL reference
     * https://youtu.be/41U78QP8nBk
     * 
     * Sends message to channel
     * Logs that someone used the command
     * Sends message to alert channel
     */
    public class Sing : ModuleBase<ShardedCommandContext>
    {
        [Command("sing")]
        public async Task SingCommand()
        {
            // <:Music:528358185124495360> is the custom emoji :music: on the support server
            await Context.Channel.SendMessageAsync("<:Music:528358185124495360> Daisy, Daisy <:Music:528358185124495360>\n" +
                                                   "<:Music:528358185124495360> Give me your answer do. <:Music:528358185124495360>\n" +
                                                   "<:Music:528358185124495360> I'm half crazy all for the love of you. <:Music:528358185124495360>\n" +
                                                   "<:Music:528358185124495360> It won't be a stylish marriage. <:Music:528358185124495360>\n" +
                                                   "<:Music:528358185124495360> I can't afford a carriage. <:Music:528358185124495360>\n" +
                                                   "<:Music:528358185124495360> But you'll look sweet upon the seat <:Music:528358185124495360>\n" +
                                                   "<:Music:528358185124495360> Of a bicycle built for two. <:Music:528358185124495360>");

            Logger.Log($"[{DateTime.Now} at Sing] {Context.User.Username}#{Context.User.Discriminator} used the sing command");

            // Build Embed
            EmbedBuilder Embed = new EmbedBuilder()
            {
                Title = $"{Context.User.Username}#{Context.User.Discriminator} used the sing command",
                Timestamp = Context.Message.Timestamp
            };
            Embed.WithColor(142, 44, 208);

            ITextChannel AlertChannel = Context.Client.GetChannel(PulsarClient.Config.AlertChannelID) as ITextChannel; // Find and cast Alert Channel
            await AlertChannel.SendMessageAsync("", false, Embed.Build());
        }
    }
}
