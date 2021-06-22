using Components;
using GenericComponents.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ComponentInterfaces;
using ComponentModels;
using System.Dynamic;
using System.Reflection;

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
            var orders = Order.GetOrders();
            var employees = Employee.GetEmployees();
            #region Components
            ITableComponent tableComponent = TableComponent<Employee>.MakeTable(employees, new ActionUrl { Edit= "home/EditEmployee" });
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
                Address = "dsdsdsdsds=dsds",
                CustomerId = "dskjdksjd",
                Id = "kdlsdls"
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
            var tr = co.X(o1);
            return View();
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
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
