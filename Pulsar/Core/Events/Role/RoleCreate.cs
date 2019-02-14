using System.Threading.Tasks;

using Discord.WebSocket;


/*
 *  __________       .__           _________                        __          
 *  \______   \ ____ |  |   ____   \_   ___ \_______   ____ _____ _/  |_  ____  
 *   |       _//  _ \|  | _/ __ \  /    \  \/\_  __ \_/ __ \\__  \\   __\/ __ \ 
 *   |    |   (  <_> )  |_\  ___/  \     \____|  | \/\  ___/ / __ \|  | \  ___/ 
 *   |____|_  /\____/|____/\___  >  \______  /|__|    \___  >____  /__|  \___  >
 *          \/                 \/          \/             \/     \/          \/ 
 */
namespace Pulsar.Core.Events.Role
{
    public class RoleCreate
    {
        public static async Task OnRoleCreate(SocketRole Role, DiscordShardedClient Client)
        {

        }
    }
}
