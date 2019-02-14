using System;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;


/*
 *  ___________                    __      ___ ___                    .___.__                
 *  \_   _____/__  __ ____   _____/  |_   /   |   \_____    ____    __| _/|  |   ___________ 
 *   |    __)_\  \/ // __ \ /    \   __\ /    ~    \__  \  /    \  / __ | |  | _/ __ \_  __ \
 *   |        \\   /\  ___/|   |  \  |   \    Y    // __ \|   |  \/ /_/ | |  |_\  ___/|  | \/
 *  /_______  / \_/  \___  >___|  /__|    \___|_  /(____  /___|  /\____ | |____/\___  >__|   
 *          \/           \/     \/              \/      \/     \/      \/           \/       
 */
namespace Pulsar.Core.Events
{
    public static class EventHandler
    {
        private static DiscordShardedClient Client; // Client
        private static CommandService Commands; // Commands

        public static void HandleEvents(DiscordShardedClient Client, CommandService Commands)
        {
            EventHandler.Client = Client;
            EventHandler.Commands = Commands;

            // Bot
            Client.ShardReady += Ready;
            Client.Log += Logging;
            Client.JoinedGuild += JoinServer;
            Client.LeftGuild += LeaveServer;
            Client.ShardDisconnected += ShardDisconnect;
            Client.ShardConnected += ShardConnect;

            // Message
            Client.MessageReceived += MessageRecived;
            Client.MessageDeleted += MessageDelete;
            Client.MessageUpdated += MessageUpdate;

            // Channel
            Client.ChannelCreated += ChannelCreate;
            Client.ChannelDestroyed += ChannelDelete;
            Client.ChannelUpdated += ChannelUpdate;

            // Role
            Client.RoleCreated += RoleCreate;
            Client.RoleDeleted += RoleDelete;
            Client.RoleUpdated += RoleUpdate;

            // Server
            Client.UserBanned += ServerBan;
            Client.UserUnbanned += ServerUnban;
            Client.GuildUpdated += ServerUpdate;

            // User
            Client.UserJoined += UserJoin;
            Client.UserLeft += UserLeave;
        }

        // Bot
        private static async Task Ready(DiscordSocketClient SocketClient) => await Bot.Ready.OnShardReady(SocketClient, Client);
        private static async Task Logging(LogMessage Message) => await Bot.Logging.OnLog(Message);
        private static async Task JoinServer(SocketGuild Server) => await Bot.JoinedServer.OnServerJoin(Server, Client);
        private static async Task LeaveServer(SocketGuild Server) => await Bot.LeftServer.OnServerLeave(Server, Client);
        private static async Task ShardDisconnect(Exception Error, DiscordSocketClient SocketClient) => await Bot.ShardDisconnect.OnShardDisconnect(Error, SocketClient, Client);
        private static async Task ShardConnect(DiscordSocketClient SocketClient) => await Bot.ShardConnect.OnShardConnect(SocketClient, Client);

        // Message
        private static async Task MessageRecived(SocketMessage SocketMsg) => await Message.Message.OnMessage(SocketMsg, Client, Commands);
        private static async Task MessageDelete(Cacheable<IMessage, ulong> Message, ISocketMessageChannel Channel) => await Events.Message.MessageDelete.OnMessageDelete(Message, Channel, Client);
        private static async Task MessageUpdate(Cacheable<IMessage, ulong> Before, SocketMessage After, ISocketMessageChannel Channel) => await Message.MessageUpdate.OnMessageUpdate(Before, After, Channel, Client);

        // Channel
        private static async Task ChannelCreate(SocketChannel Channel) => await Events.Channel.ChannelCreate.OnChannelCreate(Channel, Client);
        private static async Task ChannelDelete(SocketChannel Channel) => await Events.Channel.ChannelDelete.OnChannelDelete(Channel, Client);
        private static async Task ChannelUpdate(SocketChannel Before, SocketChannel After) => await Channel.ChannelUpdate.OnChannelUpdate(Before, After, Client);

        // Role
        private static async Task RoleCreate(SocketRole Role) => await Events.Role.RoleCreate.OnRoleCreate(Role, Client);
        private static async Task RoleDelete(SocketRole Role) => await Events.Role.RoleDelete.OnRoleDelete(Role, Client);
        private static async Task RoleUpdate(SocketRole Before, SocketRole After) => await Role.RoleUpdate.OnRoleUpdate(Before, After, Client);

        // Server
        private static async Task ServerBan(SocketUser User, SocketGuild Server) => await Events.Server.ServerBan.OnServerBan(User, Server, Client);
        private static async Task ServerUnban(SocketUser User, SocketGuild Server) => await Events.Server.ServerUnban.OnServerUnban(User, Server, Client);
        private static async Task ServerUpdate(SocketGuild Before, SocketGuild After) => await Server.ServerUpdate.OnServerUpdate(Before, After, Client);

        // User
        private static async Task UserJoin(SocketGuildUser User) => await Events.User.UserJoin.OnUserJoin(User, Client);
        private static async Task UserLeave(SocketGuildUser User) => await Events.User.UserLeave.OnUserLeave(User, Client);
    }
}
