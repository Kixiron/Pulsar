using System;
using System.Linq;

using Pulsar.Resources.Database;
using Pulsar.Resources.Datatypes;

using Pulsar.Core.KixLog;


/*
 *  ________          __          
 *  \______ \ _____ _/  |______   
 *   |    |  \\__  \\   __\__  \  
 *   |    `   \/ __ \|  |  / __ \_
 *  /_______  (____  /__| (____  /
 *          \/     \/          \/ 
 */
namespace Pulsar.Core.Utils
{
    public static class Data
    {
        // Check/Create an entry for a server
        public static async void CreateServerDb(ulong ServerID)
        {
            using (PulsarDbContext DbContext = new PulsarDbContext())
            {
                IQueryable<Server> ServerDB = DbContext.Servers.Where(x => x.ServerID == ServerID); // Select Server from db

                // If server not found, create new server entry
                if (ServerDB.Count() < 1)
                {
                    try
                    {
                        DbContext.Servers.Add(new Server
                        {
                            ServerID = ServerID,
                            EnableModlog = false,
                            ModLogEmbed = false,
                            LogBans = false,
                            LogKicks = false,
                            LogJoins = false,
                            LogLeaves = false,
                            LogUserChanges = false,
                            LogChannels = false,
                            LogMessageEdit = false,
                            LogMessageDelete = false,
                            LogRoles = false,
                            LogServer = false,
                            ModRole = 0,
                            AdminRole = 0
                        });
                    }
                    // Catch db error
                    catch (Exception error)
                    {
                        Logger.Log($"[{DateTime.Now} at Data] Error Creating Server entry for {ServerID}: {error}");
                    }
                }

                // Save db changes
                try
                {
                    await DbContext.SaveChangesAsync();
                }
                // Catch db error
                catch (Exception error)
                {
                    Logger.Log($"[{DateTime.Now} at Data] Error Saving Server entry for {ServerID}: {error}");
                }
            }
        }

        // Fetch amount of money a user has
        public static int GetCurrency(ulong ServerID, ulong UserID)
        {
            using (PulsarDbContext DbContext = new PulsarDbContext())
            {
                IQueryable<Member> UserDB = DbContext.Members.Where(x => x.UserID == UserID); // Select user from member db
                // If user not found, return no money
                if (UserDB.Count() < 1)
                {
                    return 0;
                }

                IQueryable<Member> CompoundDB = DbContext.Members.Where(x => x.ServerID == ServerID && x.UserID == UserID);// Find entry in member db where Server ID and Member ID are correct
                // Else return the user's amount
                return CompoundDB.Select(x => x.Amount).FirstOrDefault();
            }
        }

        // Save currency for a user
        public static async void SaveCurrency(ulong ServerID, ulong UserID,int Amount)
        {
            using (PulsarDbContext DbContext = new PulsarDbContext())
            {
                // If user not found, make a db entry
                if (DbContext.Members.Where(x => x.UserID == UserID && x.ServerID == ServerID).Count() < 1)
                {
                    try
                    {
                        // Add entry to db
                        DbContext.Members.Add(new Member
                        {
                            UserID = UserID,
                            ServerID = ServerID,
                            Amount = Amount
                        });
                    }
                    // Catch db error
                    catch (Exception error)
                    {
                        Logger.Log($"[{DateTime.Now} at Data] Error Creating Data entry for {UserID} in {ServerID}: {error}");
                    }
                }
                // Else update currency
                else
                {
                    Member Current = DbContext.Members.Where(x => x.UserID == UserID && x.ServerID == ServerID).FirstOrDefault();
                    Current.Amount += Amount;
                    DbContext.Members.Update(Current);
                }

                // Save db changes
                try
                {
                    await DbContext.SaveChangesAsync();
                }
                // Catch db error
                catch (Exception error)
                {
                    Logger.Log($"[{DateTime.Now} at Data] Error Saving Server entry for {ServerID}: {error}");
                }
            }
        }

        // Create an item
        public static async void CreateItemDb(ulong ServerID, string ItemName, string ItemDescription, int ItemCost)
        {
            try
            {
                using (PulsarDbContext DbContext = new PulsarDbContext())
                {
                    // If item doesn't exist, create it
                    if (DbContext.Items.Where(x => x.ServerID == ServerID && x.ItemName == ItemName).Count() < 1)
                    {
                        try
                        {
                            // Add entry to db
                            DbContext.Items.Add(new Item
                            {
                                ServerID = ServerID,
                                ItemName = ItemName,
                                ItemDescription = ItemDescription
                            });
                        }
                        catch (Exception error)
                        {
                            Logger.Log($"[{DateTime.Now} at Data] Error creating Item entry: {error}");
                        }
                    }
                    // Else update entry
                    else
                    {
                        Item Current = DbContext.Items.Where(x => x.ServerID == ServerID && x.ItemName == ItemName).FirstOrDefault();
                        Current.ItemDescription = ItemDescription;
                        Current.ItemCost = ItemCost;
                        DbContext.Items.Update(Current);
                    }

                    // Save db changes
                    try
                    {
                        await DbContext.SaveChangesAsync();
                    }
                    catch (Exception error)
                    {
                        Logger.Log($"[{DateTime.Now} at Data] Error creating saving Item entry: {error}");
                    }
                }
            }
            catch (Exception error)
            {
                Logger.Log($"[{DateTime.Now} at Data] Error Saving Item entry for {ServerID}: {error}");
            }
        }
    }
}
