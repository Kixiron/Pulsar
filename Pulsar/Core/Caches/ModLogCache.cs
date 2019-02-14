using System.Collections.Generic;


namespace Pulsar.Core.Caches
{
    public class ModLogCache
    {
        // SERVER ID | CHANNEL ID | MESSAGE ID
        public static List<ulong[]> BanMsgCache;
        public static List<ulong[]> UnbanMsgCache;

        public static List<ulong[]> KickMsgCache;
    }
}
