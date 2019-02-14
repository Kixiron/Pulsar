using System.Threading.Tasks;

using Discord.WebSocket;


/*
 *    _________                                  ____ ___            .___       __          
 *   /   _____/ ______________  __ ___________  |    |   \______   __| _/____ _/  |_  ____  
 *   \_____  \_/ __ \_  __ \  \/ // __ \_  __ \ |    |   /\____ \ / __ |\__  \\   __\/ __ \ 
 *   /        \  ___/|  | \/\   /\  ___/|  | \/ |    |  / |  |_> > /_/ | / __ \|  | \  ___/ 
 *  /_______  /\___  >__|    \_/  \___  >__|    |______/  |   __/\____ |(____  /__|  \___  >
 *          \/     \/                 \/                  |__|        \/     \/          \/ 
 */
namespace Pulsar.Core.Events.Server
{
    public class ServerUpdate
    {
        public static async Task OnServerUpdate(SocketGuild Before, SocketGuild After, DiscordShardedClient Client)
        {

        }
    }
}
