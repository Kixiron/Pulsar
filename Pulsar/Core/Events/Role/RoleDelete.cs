using System.Threading.Tasks;

using Discord.WebSocket;


/*
 *  __________       .__           ________         .__          __          
 *  \______   \ ____ |  |   ____   \______ \   ____ |  |   _____/  |_  ____  
 *   |       _//  _ \|  | _/ __ \   |    |  \_/ __ \|  | _/ __ \   __\/ __ \ 
 *   |    |   (  <_> )  |_\  ___/   |    `   \  ___/|  |_\  ___/|  | \  ___/ 
 *   |____|_  /\____/|____/\___  > /_______  /\___  >____/\___  >__|  \___  >
 *          \/                 \/          \/     \/          \/          \/ 
 */
namespace Pulsar.Core.Events.Role
{
    public class RoleDelete
    {
        public static async Task OnRoleDelete(SocketRole Role, DiscordShardedClient Client)
        {

        }
    }
}
