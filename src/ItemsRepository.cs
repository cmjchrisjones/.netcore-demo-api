using System;
using System.Collections.Generic;
using System.Linq;
using DemoAPI.Controllers;
using DemoAPI.Models;

namespace DemoAPI
{
    public class ItemsRepository : IItemsRepository
    {
        public DemoApiDbContext Db;

        public ItemsRepository(DemoApiDbContext db)
        {
            Db = db;
        }

        public void AddItem(Guid sessionId, Item item)
        {
            var foundItem = Db.Requests.FirstOrDefault(x => x.Id == sessionId);
            if (foundItem != null)
            {
                if (foundItem.Items == null)
                {
                    foundItem.Items = new List<Item>();
                }
                foundItem.Items.Add(item);
                Db.SaveChanges();
            }
        }

        public void DeleteRequest(Guid id)
        {
            var sessionToDelete = Db.Requests.FirstOrDefault(x => x.Id == id);
            if (sessionToDelete != null)
            {
                Db.Remove(sessionToDelete);
                Db.SaveChanges();
            }
        }

        public List<Request> GetAllSessions()
        {
            return Db.Requests.ToList();
        }

        public Request RetrieveSession(Guid sessionId)
        {
            return Db.Requests.FirstOrDefault(x => x.Id == sessionId);
        }

        public void StartSession(Guid session)
        {
            Db.Add(new Request { Id = session });
            Db.SaveChanges();
        }

        public bool UpdateSession(RequesterInfo requesterInfo)
        {
            var session = Db.Requests.FirstOrDefault(x => x.Id == requesterInfo.Id);

            if (session != null)
            {
                session.RequesterName = requesterInfo.Name;
                session.RequestDate = requesterInfo.RequestDate;
                Db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
