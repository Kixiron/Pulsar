using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pulsar.Core.Commands.Economy
{
    public class CreateItem : ModuleBase<ShardedCommandContext>
    {
        [Command("create-item"), Summary("Create an item for the server")]
        public async Task CreateItemCommand()
        {

        }
    }
}
