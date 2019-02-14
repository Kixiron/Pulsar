using System;
using System.Linq;
using System.Threading.Tasks;

using Discord;
using Discord.Rest;
using Discord.WebSocket;

using Pulsar.Resources.Database;


/*
 *  __________                
 *  \______   \_____    ____  
 *   |    |  _/\__  \  /    \ 
 *   |    |   \ / __ \|   |  \
 *   |______  /(____  /___|  /
 *          \/      \/     \/ 
 */
namespace Pulsar.Core.Events.Server
{
    public class ServerBan
    {
        public static async Task OnServerBan(SocketUser User, SocketGuild TargetServer, DiscordShardedClient Client)
        {
            using (PulsarDbContext DbContext = new PulsarDbContext())
            {
                IQueryable<Resources.Datatypes.Server> ServerDB = DbContext.Servers.Where(x => x.ServerID == TargetServer.Id);

                if (ServerDB.Count() >= 1)
                {
                    Resources.Datatypes.Server Server = ServerDB.FirstOrDefault();

                    if (Server.EnableModlog == true && Server.LogBans == true && Server.LogChannelID != 0)
                    {
                        ISocketMessageChannel LogChannel = Client.GetChannel(Server.LogChannelID) as ISocketMessageChannel;

                        EmbedBuilder Embed = new EmbedBuilder()
                        {
                            Title = "User Banned",
                            Description = $"{User.Username}#{User.Discriminator} was banned",
                            Timestamp = DateTime.Now
                        };
                        Embed.WithColor(255, 0, 0);

                        RestUserMessage BanMsg = await LogChannel.SendMessageAsync("", false, Embed.Build());

                        ulong[] CacheArray = new ulong[3] { TargetServer.Id, LogChannel.Id, BanMsg.Id };
                        Caches.ModLogCache.BanMsgCache.Add(CacheArray);
                    }
                }
            }
        }

        public static async Task UpdateServerBan(ulong ServerID, DiscordShardedClient Client, RestAuditLogEntry Entry)
        {
            ulong[] ServerCache = Caches.ModLogCache.BanMsgCache.Where(x => x[0] == ServerID).FirstOrDefault();
            ISocketMessageChannel Channel = Client.GetChannel(ServerCache[1]) as ISocketMessageChannel;
            SocketUserMessage Message = Channel.GetCachedMessage(ServerCache[2]) as SocketUserMessage;
            
            // Janky, but what the hell. It removes everything from the original message but Username#Discriminator so that it can be used later
            string Username = Message.Embeds.FirstOrDefault().Description;
            int index = Username.IndexOf("was banned");
            if (index >= 0)
                Username = Username.Remove(index);
            

            EmbedBuilder Embed = new EmbedBuilder()
            {
                Title = "User Banned",
                Description = $"{Username} was banned by {Entry.User}.\n" +
                              $"Reason:\n" +
                              $"```{Entry.Reason}```",
                Timestamp = Entry.CreatedAt.Date
            };
            Embed.WithColor(255, 0, 0);

            await Message.ModifyAsync(x => x.Embed = Embed.Build());
        }
    }
}
