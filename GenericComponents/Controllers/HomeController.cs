using Components;
using GenericComponents.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ComponentInterfaces;
using ComponentModels;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace GenericComponents.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITableComponent _tableComponent;

        public HomeController(ILogger<HomeController> logger, ITableComponent tableComponent)
        {
            _logger = logger;
            _tableComponent = tableComponent;
        }

        public IActionResult Index()
        {
            List<Order> orders = HttpContext.Session.GetObjectFromJson<List<Order>>("Orders");

            if (orders == null)
            {
                orders = Order.GetOrders();
            }
            var employees = Employee.GetEmployees();
            #region Components
            ITableComponent tableComponent = TableComponent<Employee>.MakeTable(employees, new ActionUrl { EditLink= "home/EditEmployee" });
            ViewBag.EmployeeTable = tableComponent.TableHeaderAsHtmlString + tableComponent.TableRowsAsHtmlString;
            tableComponent.TableName = "Employee Table";
            ViewBag.EmployeeTableName = tableComponent.TableName;
            ViewBag.TableClasses = tableComponent.TableClasses;

            ITableComponent orderTable = TableComponent<Order>.MakeTable(orders);
            orderTable.TableName = "Orders";
            ViewBag.OrdersTable = orderTable;
            #endregion

            Order o1 = new Order()
            {
                Address = "D",
                CustomerId = "D",
                Id = "D"
            };

            Class1<Order> co = new Class1<Order>();
            var mappedOrder = Class1<Order>.AutoObjectMapper(null, orders[0], 0);
            var mappedEmployees = Class1<Employee>.AutoObjectsMapper(null, employees);

            ActionUrl url = new ActionUrl()
            {
                EditLink = "/home/edit",
                DeleteLink = "/home/RemoveOrderFromGrid"
            };

            HttpContext.Session.SetObjectAsJson("Orders", orders);
            var grid = GridComponent<Order>.GridBuilder(HttpContext.Session.GetObjectFromJson<List<Order>>("Orders"), url);
            ViewBag.OrderGrid = grid;

            return View();
        }

        public IActionResult AddOrderToGrid(IFormCollection keyValuePairs)
        {
            Order newOrder = new Order
            {
                Id = Guid.NewGuid().ToString(),
                CustomerId = Guid.NewGuid().ToString(),
                Address = "Hard Address__",
            };
            List<Order> orders = HttpContext.Session.GetObjectFromJson<List<Order>>("Orders");
            orders.Add(newOrder);
            HttpContext.Session.SetObjectAsJson("Orders", orders);
            return RedirectToAction("Index");
        }

        public IActionResult RemoveOrderFromGrid(int id)
        {
            List<Order> orders = HttpContext.Session.GetObjectFromJson<List<Order>>("Orders");
            orders.Remove(orders[id]);
            HttpContext.Session.SetObjectAsJson("Orders", orders);
            return RedirectToAction("Index");
        }

        public IActionResult EditEmployee(string id)
        {
            var employees = Employee.GetEmployees();
            var employee = ApiMethodsHelper<Employee>.Get(employees, id);
            return View(employee);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var c = HttpContext.Session.Get("jdksjd");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
