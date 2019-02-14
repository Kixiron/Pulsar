/* This file has been junkified by JunkyPy */
﻿using System.Reflection;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

using Pulsar.Core.Config;


/*
*  __________      .__
*  \______   \__ __|  |   ___________ _______
*   |     ___/  |  \  |  /  ___/\__  \\_  __ \
*   |    |   |  |  /  |__\___ \  / __ \|  | \/
*   |____|   |____/|____/____  >(____  /__|
*                            \/      \/
*/
namespace Pulsar
{
public class PulsarClient
{
private DiscordShardedClient Client; // Client
private CommandService Commands; // Commands

public static void Main(string[] args) => new PulsarClient().PulsarAsync().GetAwaiter().GetResult();

public static Configuration.Config Config = Configuration.LoadConfig(); // Config

public async Task PulsarAsync()
{
// Initialize Client
if (Config.ShardNumber != 0) // If not 0, TotalShards = Config.ShardNumber
{
Client = new DiscordShardedClient(new DiscordSocketConfig
{
MessageCacheSize = 100,
AlwaysDownloadUsers = true,
LogLevel = LogSeverity.Debug,
TotalShards = Config.ShardNumber
});
}
else // If Config.ShardNumber = 0, Shards = Automatic
{
Client = new DiscordShardedClient(new DiscordSocketConfig
{
MessageCacheSize = 100,
AlwaysDownloadUsers = true,
LogLevel = LogSeverity.Debug
});
}

// Initialize Commands
Commands = new CommandService(new CommandServiceConfig
{
LogLevel = LogSeverity.Debug,
CaseSensitiveCommands = false,
DefaultRunMode = RunMode.Async
});

// Event Handler
Core.Events.EventHandler.HandleEvents(Client, Commands);

await Commands.AddModulesAsync(Assembly.GetEntryAssembly(), null); // Add Command Modules
await Client.LoginAsync(TokenType.Bot, Config.Token);
await Client.StartAsync();

await Task.Delay(-1);
}
}
}