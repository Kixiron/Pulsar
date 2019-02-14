using Microsoft.EntityFrameworkCore;

using Pulsar.Resources.Datatypes;


/*
 *  ________ __________    _________       __    __  .__                      
 *  \______ \\______   \  /   _____/ _____/  |__/  |_|__| ____    ____  ______
 *   |    |  \|    |  _/  \_____  \_/ __ \   __\   __\  |/    \  / ___\/  ___/
 *   |    `   \    |   \  /        \  ___/|  |  |  | |  |   |  \/ /_/  >___ \ 
 *  /_______  /______  / /_______  /\___  >__|  |__| |__|___|  /\___  /____  >
 *          \/       \/          \/     \/                   \//_____/     \/ 
 */
namespace Pulsar.Resources.Database
{
    public class PulsarDbContext : DbContext
    {
        public DbSet<Server> Servers { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Inventory> Inventories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder Options)
        {
            Options.UseSqlite($"Data Source=G:/Local File Server/Programming/C#/Pulsar/Pulsar/Core/Data/Database.sqlite");
        }
    }
}
