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
        [Route("/api/add-item/{item}")]
        public IActionResult AddItem([FromBody]AddItemModel addItemModel)
        {

            Repo.AddItem(addItemModel.Id, addItemModel.Item);
            return Ok("Item added");
        }

        [HttpPost]
        [Route("/api/retrieve-session/{sessionId}")]
        public IActionResult GetSession([FromBody] Guid sessionId)
        {
            var items = Repo.RetrieveSession(sessionId);
            return new JsonResult(items);
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
