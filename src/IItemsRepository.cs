using System;
using System.Collections.Generic;
using DemoAPI.Controllers;
using DemoAPI.Models;

namespace DemoAPI
{
    public interface IItemsRepository
    {
        void StartSession(Guid session);
        void AddItem(Guid sessionId, Item item);
        Request RetrieveSession(Guid sessionId);
        List<Request> GetAllSessions();
        void DeleteRequest(Guid id);
        bool UpdateSession(RequesterInfo requesterInfo);
    }
}
