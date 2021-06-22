using System;
using System.Collections.Generic;
using System.Text;

namespace ComponentInterfaces
{
    public interface ITableComponent
    {
        string TableName { get; set; }
        string TableClasses { get; set; }
        string TableHeaderAsHtmlString { get; set; }
        string TableRowsAsHtmlString { get; set; }
    }
}
