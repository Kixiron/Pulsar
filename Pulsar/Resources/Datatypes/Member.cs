using System.ComponentModel.DataAnnotations;


/*
 *     _____                ___.                 
 *    /     \   ____   _____\_ |__   ___________ 
 *   /  \ /  \_/ __ \ /     \| __ \_/ __ \_  __ \
 *  /    Y    \  ___/|  Y Y  \ \_\ \  ___/|  | \/
 *  \____|__  /\___  >__|_|  /___  /\___  >__|   
 *          \/     \/      \/    \/     \/       
 */
namespace Pulsar.Resources.Datatypes
{
    public sealed class Member
    {
        [Key]
        public ulong UserID { get; set; }
        public ulong ServerID { get; set; }
        public int Amount { get; set; }
    }
}
