using System;
using System.Threading.Tasks;

using Discord.WebSocket;

using Pulsar.Core.KixLog;


/*
 *    _________.__                     .___ ________  .__                                                 __   
 *   /   _____/|  |__ _____ _______  __| _/ \______ \ |__| ______ ____  ____   ____   ____   ____   _____/  |_ 
 *   \_____  \ |  |  \\__  \\_  __ \/ __ |   |    |  \|  |/  ___// ___\/  _ \ /    \ /    \_/ __ \_/ ___\   __\
 *   /        \|   Y  \/ __ \|  | \/ /_/ |   |    `   \  |\___ \\  \__(  <_> )   |  \   |  \  ___/\  \___|  |  
 *  /_______  /|___|  (____  /__|  \____ |  /_______  /__/____  >\___  >____/|___|  /___|  /\___  >\___  >__|  
 *          \/      \/     \/           \/          \/        \/     \/           \/     \/     \/     \/      
 */
namespace Pulsar.Core.Events.Bot
{
    public class ShardDisconnect
    {
        public static async Task OnShardDisconnect(Exception Error, DiscordSocketClient SocketClient, DiscordShardedClient Client)
        {
            Logger.Log($"[{DateTime.Now} at Shard #{SocketClient.ShardId + 1}] Shard #{SocketClient.ShardId + 1} Disconnected | Error: {Error}");
        }
    }
}
