namespace DemoAPI
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DemoAPI.Controllers;
    using DemoAPI.Models;
    using Microsoft.EntityFrameworkCore;

    public class ItemsRepository : IItemsRepository
    {
        public DemoApiDbContext Db;

        public ItemsRepository(DemoApiDbContext db)
        {
            Db = db;
        }

        public void AddItem(Item item)
        {
            Db.Items.Add(item);
            Db.SaveChanges();
        }

        public void DeleteRequest(Guid id)
        {
            var sessionToDelete = Db.Requests.FirstOrDefault(x => x.Id == id);
            if (sessionToDelete != null)
            {
                Db.Requests.Remove(sessionToDelete);
                foreach (var item in Db.Items.ToList())
                {
                    if (item.Request.Id == id)
                    {
                        Db.Remove(item);
                        Db.SaveChanges();
                    }
                }
                Db.SaveChanges();
            }
        }

        public List<Request> GetAllSessions()
        {
            var list = Db.Requests
                .Include(req => req.Items)
                .ToList();
            return list;
        }

        public Request RetrieveSession(Guid sessionId)
        {
            return Db.Requests.Include(c => c.Items).FirstOrDefault(x => x.Id == sessionId);
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

        public List<Item> GetItems()
        {
            return Db.Items.ToList();
        }

        public List<Item> GetItemsForSession(Guid sessionId)
        {
            return Db.Items.Where(x => x.Request.Id == sessionId).ToList();
        }
    }
}
