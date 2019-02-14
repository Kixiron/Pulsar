using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;


/*
 *     _____                                                 ____ ___            .___       __          
 *    /     \   ____   ______ ___________     ____   ____   |    |   \______   __| _/____ _/  |_  ____  
 *   /  \ /  \_/ __ \ /  ___//  ___/\__  \   / ___\_/ __ \  |    |   /\____ \ / __ |\__  \\   __\/ __ \ 
 *  /    Y    \  ___/ \___ \ \___ \  / __ \_/ /_/  >  ___/  |    |  / |  |_> > /_/ | / __ \|  | \  ___/ 
 *  \____|__  /\___  >____  >____  >(____  /\___  / \___  > |______/  |   __/\____ |(____  /__|  \___  >
 *          \/     \/     \/     \/      \//_____/      \/            |__|        \/     \/          \/ 
 */
namespace Pulsar.Core.Events.Message
{
    public class MessageUpdate
    {
        public static async Task OnMessageUpdate(Cacheable<IMessage, ulong> Before, SocketMessage After, ISocketMessageChannel Channel, DiscordShardedClient Client)
        {

        }
    }
}
