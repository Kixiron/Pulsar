using System;
using System.Threading.Tasks;

using Discord.Commands;
using Discord.WebSocket;

using Pulsar.Core.KixLog;


/*
 *     _____                                               
 *    /     \   ____   ______ ___________     ____   ____  
 *   /  \ /  \_/ __ \ /  ___//  ___/\__  \   / ___\_/ __ \ 
 *  /    Y    \  ___/ \___ \ \___ \  / __ \_/ /_/  >  ___/ 
 *  \____|__  /\___  >____  >____  >(____  /\___  / \___  >
 *          \/     \/     \/     \/      \//_____/      \/ 
 */
namespace Pulsar.Core.Events.Message
{
    public class Message
    {
        /*
         * On Message
         * 
         * Ignore Null/Blank/Empty messages
         * Ignore bots and webhooks
         * Check for prefix (and mention if AcceptMention is true)
         * Await command execution
         * Report to Logger
         */
        public static async Task OnMessage(SocketMessage SocketMsg, DiscordShardedClient Client, CommandService Commands)
        {
            // Cast Context
            SocketUserMessage Message = SocketMsg as SocketUserMessage;
            ShardedCommandContext Context = new ShardedCommandContext(Client, Message);

            if (Context.Message == null || Context.Message.Content == "") return; // Ignore Null/Blank Messages
            if (Context.User.IsBot || Context.User.IsWebhook) return; // Ignore Bots and Webhooks

            int PrefixPos = 0;
            // Check for prefix or Pulsar mention
            if (PulsarClient.Config.AcceptMention)
            {
                if (!(Message.HasStringPrefix(PulsarClient.Config.Prefix[0], ref PrefixPos) || (Message.HasStringPrefix(PulsarClient.Config.Prefix[1], ref PrefixPos) || (Message.HasMentionPrefix(Client.CurrentUser, ref PrefixPos))))) return; // Ignore messages without prefix or Pulsar mention
            }
            else
            {
                if (!(Message.HasStringPrefix(PulsarClient.Config.Prefix[0], ref PrefixPos) || (Message.HasStringPrefix(PulsarClient.Config.Prefix[1], ref PrefixPos)))) return; // Ignore messages without prefix
            }

            IResult Result = await Commands.ExecuteAsync(Context, PrefixPos, null);
            if (!Result.IsSuccess)
            {
                Logger.Log($"[{DateTime.Now} at Commands] Text: {Context.Message.Content} | Error: {Result.ErrorReason}");
            }
            else
            {
                Logger.Log($"[{DateTime.Now} at Commands] Executed Command: {Context.Message.Content}");
            }
        }
    }
}
