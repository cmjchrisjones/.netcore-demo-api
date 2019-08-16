using System;
using System.Collections.Generic;

namespace DemoAPI.Models
{
    public class Request
    {
        public Guid Id { get; set; }

        public string RequesterName { get; set; }

        public ICollection<Item> Items { get; set; }

        public DateTime? RequestDate { get; set; }
    }
}
