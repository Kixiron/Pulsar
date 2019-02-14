using System;
using System.Threading.Tasks;

using Discord.WebSocket;

using Pulsar.Core.KixLog;


/*
 *    _________.__                     .___ _________                                     __   
 *   /   _____/|  |__ _____ _______  __| _/ \_   ___ \  ____   ____   ____   ____   _____/  |_ 
 *   \_____  \ |  |  \\__  \\_  __ \/ __ |  /    \  \/ /  _ \ /    \ /    \_/ __ \_/ ___\   __\
 *   /        \|   Y  \/ __ \|  | \/ /_/ |  \     \___(  <_> )   |  \   |  \  ___/\  \___|  |  
 *  /_______  /|___|  (____  /__|  \____ |   \______  /\____/|___|  /___|  /\___  >\___  >__|  
 *          \/      \/     \/           \/          \/            \/     \/     \/     \/      
 */
namespace Pulsar.Core.Events.Bot
{
    public class ShardConnect
    {
        public static async Task OnShardConnect(DiscordSocketClient SocketClient, DiscordShardedClient Client)
        {
            Logger.Log($"[{DateTime.Now} at Shard #{SocketClient.ShardId + 1}] Shard #{SocketClient.ShardId + 1} Connected");
        }
    }
}
