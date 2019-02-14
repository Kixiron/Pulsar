using Discord.Commands;
using Discord.WebSocket;


/*
 *  ________                             
 *  \_____  \__ _  ______ ______________
 *   /   |   \ \/ \/ /    \_/ __ \_ __  \
 *  /    |    \     /   |  \  ___/|  | \/
 *  \_______  /\/\_/|___|  /\___  >__|   
 *          \/           \/     \/       
 */
namespace Pulsar.Core.Utils
{
    public class Owner
    {
        // Returns SocketUser of Owner
        public static SocketUser GetOwnerObj(ShardedCommandContext Context)
        {
            SocketUser OwnerObj = Context.Client.GetUser(PulsarClient.Config.OwnerID);
            return OwnerObj;
        }
    }
}
