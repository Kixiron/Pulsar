using System.Threading.Tasks;

using Discord.WebSocket;


/*
 *  _________ .__                                .__     ____ ___            .___       __          
 *  \_   ___ \|  |__ _____    ____   ____   ____ |  |   |    |   \______   __| _/____ _/  |_  ____  
 *  /    \  \/|  |  \\__  \  /    \ /    \_/ __ \|  |   |    |   /\____ \ / __ |\__  \\   __\/ __ \ 
 *  \     \___|   Y  \/ __ \|   |  \   |  \  ___/|  |__ |    |  / |  |_> > /_/ | / __ \|  | \  ___/ 
 *   \______  /___|  (____  /___|  /___|  /\___  >____/ |______/  |   __/\____ |(____  /__|  \___  >
 *          \/     \/     \/     \/     \/     \/                 |__|        \/     \/          \/ 
 */
namespace Pulsar.Core.Events.Channel
{
    public class ChannelUpdate
    {
        public static async Task OnChannelUpdate(SocketChannel Before, SocketChannel After, DiscordShardedClient Client)
        {

        }
    }
}
