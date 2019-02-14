using System.Threading.Tasks;

using Discord.WebSocket;


/*
 *  _________ .__                                .__    _________                        __          
 *  \_   ___ \|  |__ _____    ____   ____   ____ |  |   \_   ___ \_______   ____ _____ _/  |_  ____  
 *  /    \  \/|  |  \\__  \  /    \ /    \_/ __ \|  |   /    \  \/\_  __ \_/ __ \\__  \\   __\/ __ \ 
 *  \     \___|   Y  \/ __ \|   |  \   |  \  ___/|  |__ \     \____|  | \/\  ___/ / __ \|  | \  ___/ 
 *   \______  /___|  (____  /___|  /___|  /\___  >____/  \______  /|__|    \___  >____  /__|  \___  >
 *          \/     \/     \/     \/     \/     \/               \/             \/     \/          \/ 
 */
namespace Pulsar.Core.Events.Channel
{
    public class ChannelCreate
    {
        public static async Task OnChannelCreate(SocketChannel Channel, DiscordShardedClient Client)
        {

        }
    }
}
