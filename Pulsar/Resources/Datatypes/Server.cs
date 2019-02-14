using System.ComponentModel.DataAnnotations;


/*
 *    _________                                
 *   /   _____/ ______________  __ ___________ 
 *   \_____  \_/ __ \_  __ \  \/ // __ \_  __ \
 *   /        \  ___/|  | \/\   /\  ___/|  | \/
 *  /_______  /\___  >__|    \_/  \___  >__|   
 *          \/     \/                 \/       
 */
namespace Pulsar.Resources.Datatypes
{
    public sealed class Server
    {
        [Key]
        public ulong ServerID { get; set; }
        public bool EnableModlog { get; set; }
        public ulong LogChannelID { get; set; }

        public bool ModLogEmbed { get; set; }
        public bool LogBans { get; set; }
        public bool LogKicks { get; set; }
        public bool LogJoins { get; set; }
        public bool LogLeaves { get; set; }
        public bool LogUserChanges { get; set; }
        public bool LogChannels { get; set; }
        public bool LogMessageEdit { get; set; }
        public bool LogMessageDelete { get; set; }
        public bool LogRoles { get; set; }
        public bool LogServer { get; set; }

        public ulong ModRole { get; set; }
        public ulong AdminRole { get; set; }
    }
}
