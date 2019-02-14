using System.ComponentModel.DataAnnotations;


/*
 *  .___                           __                       
 *  |   | _______  __ ____   _____/  |_  ___________ ___.__.
 *  |   |/    \  \/ // __ \ /    \   __\/  _ \_  __ <   |  |
 *  |   |   |  \   /\  ___/|   |  \  | (  <_> )  | \/\___  |
 *  |___|___|  /\_/  \___  >___|  /__|  \____/|__|   / ____|
 *           \/          \/     \/                   \/     
 */
namespace Pulsar.Resources.Datatypes
{
    public sealed class Inventory
    {
        [Key]
        public ulong UserID { get; set; }
        public ulong ServerID { get; set; }
        public ulong ItemName { get; set; }
        public int Amount { get; set; }
    }
}
