using System;
using DemoAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : Controller
    {
        public ItemsController(IItemsRepository repo)
        {
            Repo = repo;
        }

        public IItemsRepository Repo { get; }

        [HttpGet]
        [Route("/api/start")]
        public IActionResult StartSession()
        {
            var session = Guid.NewGuid();
            Repo.StartSession(session);
            return Ok(new { sessionId = session });
        }

        [HttpPost]
        [Route("/api/add-item")]
        public IActionResult AddItem([FromBody]Item addItemModel)
        {
            Repo.AddItem(addItemModel);
            return Ok("Item added");
        }

        [HttpGet]
        [Route("/api/retrieve-session/{sessionId}")]
        public IActionResult GetSession(Guid sessionId)
        {
            Request item = Repo.RetrieveSession(sessionId);
            return new JsonResult(item);
        }

        [HttpGet]
        [Route("/api/get-all-sessions")]
        public IActionResult GetAllSessions()
        {
            var allData = Repo.GetAllSessions();
            return Ok(allData);
        }

        [HttpPost]
        [Route("/api/update-requester-info")]
        public IActionResult UpdateRequestorInformation([FromBody]RequesterInfo requesterInfo)
        {
            if (Repo.UpdateSession(requesterInfo))
            {
                return Ok();
            }
            return BadRequest();

        }

        [HttpDelete]
        [Route("/api/delete/{sessionId}")]
        public void DeleteSession(Guid sessionId)
        {
            Repo.DeleteRequest(sessionId);
        }

        [HttpGet]
        [Route("/api/get-items")]
        public IActionResult GetItems()
        {
            return Ok(Repo.GetItems());
        }

        [HttpGet]
        [Route("/api/get-items/{sessionId}")]
        public IActionResult GetItemsForSession(Guid sessionId)
        {
            return Ok(Repo.GetItemsForSession(sessionId));
        }
    }

    public class AddItemModel
    {
        public Guid Id { get; set; }

        public Item Item { get; set; }
    }

    public class RequesterInfo
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime? RequestDate => DateTime.Now;

        public DateTime? HireDate { get; set; }
    }
}
