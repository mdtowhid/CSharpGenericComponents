using ComponentInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComponentModels
{
    public class Order : IModelBase
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string Address { get; set; }

        public static List<Order> GetOrders()
        {
            return new List<Order>
            {
                new Order
                {
                    Address="A",
                    CustomerId="A",
                    Id="A",
                },
                new Order
                {
                    Address="B",
                    CustomerId="B",
                    Id="B",
                },
                new Order
                {
                    Address = "C",
                    CustomerId = "C",
                    Id = "C",
                }
            };
        }
    }
}
