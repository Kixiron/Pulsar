using System.Threading.Tasks;

using Discord.WebSocket;


/*
 *  __________       .__            ____ ___            .___       __          
 *  \______   \ ____ |  |   ____   |    |   \______   __| _/____ _/  |_  ____  
 *   |       _//  _ \|  | _/ __ \  |    |   /\____ \ / __ |\__  \\   __\/ __ \ 
 *   |    |   (  <_> )  |_\  ___/  |    |  / |  |_> > /_/ | / __ \|  | \  ___/ 
 *   |____|_  /\____/|____/\___  > |______/  |   __/\____ |(____  /__|  \___  >
 *          \/                 \/            |__|        \/     \/          \/ 
 */
namespace Pulsar.Core.Events.Role
{
    public class RoleUpdate
    {
        public static async Task OnRoleUpdate(SocketRole Before, SocketRole After, DiscordShardedClient Client)
        {

        }
    }
}
