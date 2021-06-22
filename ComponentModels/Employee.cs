using ComponentInterfaces;
using System;
using System.Collections.Generic;

namespace ComponentModels
{
    public class Employee : IModelBase
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string DepartmentName { get; set; }

        public static List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>
            {
                new Employee
                {
                    DepartmentName="Dept1",
                    Email="j1@gmail.com",
                    Id="12121wdsd-sdsdsd",
                    Name="Jane1",
                },
                new Employee
                {
                    DepartmentName="Dept2",
                    Email="j2@gmail.com",
                    Id="12121wedsd-sdsdsd",
                    Name="Jane12",
                },
                new Employee
                {
                    DepartmentName="Dept3",
                    Email="j23@gmail.com",
                    Id="121321wedsd-sdsdsd",
                    Name="Jane123",
                }
            };

            return employees;
        }
    }
}
