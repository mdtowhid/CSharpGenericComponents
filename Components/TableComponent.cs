using ComponentInterfaces;
using ComponentsBiz;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using ComponentModels;

namespace Components
{
    public class TableComponent<T>
    {
        private readonly static string thOpen = "<th>";
        private readonly static string thClose = "</th>";
        private readonly static string trOpen = "<tr>";
        private readonly static string trClose = "</tr>";
        private readonly static string tdOpen = "</td>";
        private readonly static string tdClose = "</td>";

        private static string GetTableHeader(T tableClass)
        {
            string tableHeader = "";
            PropertyInfo[] propertyInfos = tableClass.GetType().GetProperties();
            foreach (PropertyInfo pi in propertyInfos)
            {
                tableHeader += thOpen + pi.Name + thClose;
            }
            return trOpen + tableHeader + trClose;
        }
        private static string MakeTableRow(T model)
        {
            PropertyInfo[] propertyInfos = model.GetType().GetProperties();
            string rowData = "";
            foreach (PropertyInfo pi in propertyInfos)
            {
                rowData += tdOpen + (string)pi.GetValue(model, null) + tdClose;
            }
            return rowData;
        }
        public static ITableComponent MakeTable(List<T> models)
        {
            string tableRows = "";
            TableComponentBiz tableComponentBiz = new TableComponentBiz();
            if (models.Count > 0)
            {
                var model = models[0];
                foreach (var m in models)
                {
                    tableRows += "<tr>" + MakeTableRow(m) + "</tr>";
                }
                tableComponentBiz.TableHeaderAsHtmlString = GetTableHeader(model);
                tableComponentBiz.TableRowsAsHtmlString = tableRows;
                tableComponentBiz.TableName = "Default Table";
                tableComponentBiz.TableClasses = "table table-hover table-bordered table-striped";
            }
            return tableComponentBiz;
        }

        private static string MakeTableRow(T model, ActionUrl actionUrl)
        {
            PropertyInfo[] propertyInfos = model.GetType().GetProperties();
            string rowData = "";
            List<string> rows = null;
            string id = "";
            foreach (PropertyInfo pi in propertyInfos)
            {
                IModelBase modelBase = (IModelBase)model;
                rowData += tdOpen + (string)pi.GetValue(model, null) + tdClose;
                id = modelBase.Id;
            }
            rowData += tdOpen+"<a href=" + actionUrl.Edit + "?id=" + id + ">Edit</a>";
            return rowData;
        }

        private static string GetTableHeader(T tableClass, bool withActions)
        {
            string tableHeader = "";
            PropertyInfo[] propertyInfos = tableClass.GetType().GetProperties();
            foreach (PropertyInfo pi in propertyInfos)
            {
                tableHeader += "<th>" + pi.Name + "</th>";
            }
            return "<tr>" + tableHeader + "<th>Actions</th></tr>";
        }
        public static ITableComponent MakeTable(List<T> models, ActionUrl actionUrl)
        {
            string tableRows = "";
            TableComponentBiz tableComponentBiz = new TableComponentBiz();
            if (models.Count > 0)
            {
                var model = models[0];
                foreach (var m in models)
                {
                    tableRows += "<tr>" + MakeTableRow(m, actionUrl) + "</tr>";
                }
                tableComponentBiz.TableHeaderAsHtmlString = GetTableHeader(model, true);
                tableComponentBiz.TableRowsAsHtmlString = tableRows;
                tableComponentBiz.TableName = "Default Table";
                tableComponentBiz.TableClasses = "table table-hover table-bordered table-striped";
            }
            return tableComponentBiz;
        }
    }
}
