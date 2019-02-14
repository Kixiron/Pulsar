using System.Threading.Tasks;

using Discord.WebSocket;


/*
 *  _________ .__                                .__    ________         .__          __          
 *  \_   ___ \|  |__ _____    ____   ____   ____ |  |   \______ \   ____ |  |   _____/  |_  ____  
 *  /    \  \/|  |  \\__  \  /    \ /    \_/ __ \|  |    |    |  \_/ __ \|  | _/ __ \   __\/ __ \ 
 *  \     \___|   Y  \/ __ \|   |  \   |  \  ___/|  |__  |    `   \  ___/|  |_\  ___/|  | \  ___/ 
 *   \______  /___|  (____  /___|  /___|  /\___  >____/ /_______  /\___  >____/\___  >__|  \___  >
 *          \/     \/     \/     \/     \/     \/               \/     \/          \/          \/ 
 */
namespace Pulsar.Core.Events.Channel
{
    public class ChannelDelete
    {
        public static async Task OnChannelDelete(SocketChannel Channel, DiscordShardedClient Client)
        {

        }
    }
}
