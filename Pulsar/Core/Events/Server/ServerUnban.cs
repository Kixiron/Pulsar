using System.Threading.Tasks;

using Discord.WebSocket;


/*
 *   ____ ___     ___.                  
 *  |    |   \____\_ |__ _____    ____  
 *  |    |   /    \| __ \\__  \  /    \ 
 *  |    |  /   |  \ \_\ \/ __ \|   |  \
 *  |______/|___|  /___  (____  /___|  /
 *               \/    \/     \/     \/ 
 */
namespace Pulsar.Core.Events.Server
{
    public class ServerUnban
    {
        public static async Task OnServerUnban(SocketUser User, SocketGuild Server, DiscordShardedClient Client)
        {

        }
    }
}
