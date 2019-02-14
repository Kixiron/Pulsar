using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;


/*
 *     _____                                                ________         .__          __          
 *    /     \   ____   ______ ___________     ____   ____   \______ \   ____ |  |   _____/  |_  ____  
 *   /  \ /  \_/ __ \ /  ___//  ___/\__  \   / ___\_/ __ \   |    |  \_/ __ \|  | _/ __ \   __\/ __ \ 
 *  /    Y    \  ___/ \___ \ \___ \  / __ \_/ /_/  >  ___/   |    `   \  ___/|  |_\  ___/|  | \  ___/ 
 *  \____|__  /\___  >____  >____  >(____  /\___  / \___  > /_______  /\___  >____/\___  >__|  \___  >
 *          \/     \/     \/     \/      \//_____/      \/          \/     \/          \/          \/ 
 */
namespace Pulsar.Core.Events.Message
{
    public class MessageDelete
    {
        public static async Task OnMessageDelete(Cacheable<IMessage, ulong> Message, ISocketMessageChannel Channel, DiscordShardedClient Client)
        {

        }
    }
}
