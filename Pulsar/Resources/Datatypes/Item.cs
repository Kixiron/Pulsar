using System.ComponentModel.DataAnnotations;


/*
 *  .___  __                  
 *  |   |/  |_  ____   _____  
 *  |   \   __\/ __ \ /     \ 
 *  |   ||  | \  ___/|  Y Y  \
 *  |___||__|  \___  >__|_|  /
 *                 \/      \/ 
 */
namespace Pulsar.Resources.Datatypes
{
    public sealed class Item
    {
        [Key]
        public ulong ServerID { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public int ItemCost { get; set; }
    }
}
