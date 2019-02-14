using System;
using System.Collections.Generic;
using System.Text;

using Discord;
using Discord.WebSocket;
using Discord.Commands;


/*
 *  _________ .__                   __            
 *  \_   ___ \|  |__   ____   ____ |  | __  ______
 *  /    \  \/|  |  \_/ __ \_/ ___\|  |/ / /  ___/
 *  \     \___|   Y  \  ___/\  \___|    <  \___ \ 
 *   \______  /___|  /\___  >\___  >__|_ \/____  >
 *          \/     \/     \/     \/     \/     \/ 
 */
namespace Pulsar.Core.Utils
{
    public class Checks
    {
        // Checks if the supplied userID is the owner
        public static bool IsOwner(ulong UserID)
        {
            if (UserID == PulsarClient.Config.OwnerID) return true;
            else return false;
        }
    }
}
