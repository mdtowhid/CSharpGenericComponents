using ComponentInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComponentsBiz
{
    public class TableComponentBiz : ITableComponent
    {
        public string TableName { get; set; }
        public string TableClasses { get; set; }
        public string TableHeaderAsHtmlString { get; set; }
        public string TableRowsAsHtmlString { get; set; }
    }
}
