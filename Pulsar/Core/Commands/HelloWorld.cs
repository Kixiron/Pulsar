using System.Threading.Tasks;

using Discord;
using Discord.Commands;

namespace Pulsar.Core.Commands
{
    public class HelloWorld : ModuleBase<ShardedCommandContext>
    {
        [Command("hello"), Alias("helloworld"), Summary("Hello world!")]
        public async Task CommandHello()
        {
            await Context.Channel.SendMessageAsync($"Hello {Context.User.Mention}!");
        }
        [Command("embed")]
        public async Task CommandEmbed()
        {
            EmbedBuilder Embed = new EmbedBuilder
            {
                Title = $"{Context.Client.CurrentUser.Username}",
                Timestamp = Context.Message.Timestamp,
                Description = "[Test](https://www.kixiron.com)"
            };
            Embed.WithColor(142, 44, 208);
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }
    }
}
