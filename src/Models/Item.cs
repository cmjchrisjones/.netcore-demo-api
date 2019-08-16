namespace DemoAPI.Models
{
    using System;
    public class Item
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime RequestDate { get; set; }

        public DateTime HireDate { get; set; }
    }
}
