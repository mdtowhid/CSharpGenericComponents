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
            //var pn = o1.GetType().GetProperty("Address");
            //var propName = pn.Name;
            //var pname = o1.GetType().Name;
            //var pnt1 = pn.PropertyType;
            //var val = pn.GetValue(o1, null);
            //ExpandoObject expando = new ExpandoObject();

            Class1<Order> co = new Class1<Order>();
            //co.ObjectTypeOfT = o1;
            ////co[propName] = val;
            ////var c = co[propName];
            //string data = String.Empty;
            //foreach (PropertyInfo pi in o1.GetType().GetProperties())
            //{
            //    var propName = pi.Name;
            //    var propValue = pi.GetValue(o1, null);
            //    co[propName] = propValue;
            //}

            //ViewBag.Data = co.ObjectTypeOfT;
            var mappedOrder = Class1<Order>.AutoObjectMapper(null, orders[0]);
            var mappedEmployees = Class1<Employee>.AutoObjectsMapper(null, employees);

            //GridComponent<Order>.AddObjectToGrid(o1, orders);
            //var cc = GridComponent<Order>.FindObjectFromGrid(2, orders);
            //GridComponent<Order>.UpdateObjectTOGrid(3, cc, orders);
            ///*GridComponent<Order>.RemoveObjectFromGrid(2, orders)*/;

            //ITableComponent orTable = TableComponent<Order>.MakeTable(GridComponent<Order>.UpdateObjectTOGrid(3, cc, orders), new ActionUrl() { Edit = "[controller]/{id}" });

            //ViewBag.OrTable = orTable.TableHeaderAsHtmlString + orTable.TableRowsAsHtmlString;

            //GridComponent<Order>.GridBuilder(orders, new ActionUrl() { Delete = "/delete/", Edit = "edit/" });
            //var orderTableHeader = ComponentHelpers.GetPropertyNames(orders[0]);
            //var orderRows = ComponentHelpers.GetPropertyValues(orders);

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
