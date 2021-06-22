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
                    Address="djskdjskd",
                    CustomerId="sdsdxs-dsdsd",
                    Id="--dsd-dsdsndns",
                },
                new Order
                {
                    Address="djskdjskd",
                    CustomerId="sdsds-dsdsd",
                    Id="00e0343c",
                },
                new Order
                {
                    Address = "djskdjskd",
                    CustomerId = "sdsds-dsdsd",
                    Id = "00e0dosksd343",
                }
            };
        }
    }
}
