using ComponentModels;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Components
{
    public class GridComponent<T> where T : class 
    {
        public static readonly string SplitChar = "_";
        private readonly static string thOpen = "<th>";
        private readonly static string thClose = "</th>";
        private readonly static string trOpen = "<tr>";
        private readonly static string trClose = "</tr>";
        private readonly static string tdOpen = "<td>";
        private readonly static string tdClose = "</td>";
        private readonly static string tableOpen = "<table class=\"table table-striped table-hover\">";
        private readonly static string tableClose = "</table>";
        private readonly static string theaderOpen = "<theader>";
        private readonly static string theaderClose = "</theader>";
        private readonly static string tbodyOpen = "<tbody>";
        private readonly static string tbodyClose = "</tbody>";
        private readonly static string actionHeader = "<th>Actions</th>";
        private readonly static string anchorOpen = "<a>";
        private readonly static string anchorClose = "</a>";

        public static string GridBuilder([Optional] List<T> models, [Optional] ActionUrl url)
        {
            string grid = "";
            if(models == null)
                return "T Data is not provided. Session is also null for given class " + typeof(T).GetType().Name + ". Sorry for bulding grid.";

            if (models.Count > 0)
            {
                string header = "";
                foreach (var item in ComponentHelpers.GetPropertyNames(models[0])) header += thOpen + item + thClose;
                if (url != null) header += actionHeader;
                grid += trOpen + header + trClose;
                
                var rows = ComponentHelpers.GetPropertyValues(models);
                grid += ShaveGrids(rows, tdOpen, tdClose, url);
            }
            return tableOpen + grid + tableClose;
        }

        static string ShaveGrids(List<string> gridsData, string openingTag, string closingTag, [Optional] ActionUrl url)
        {
            string grid = string.Empty;
            int gridIndex = 0;
            foreach (string gridData in gridsData)
            {
                gridIndex++;
                grid += ShaveGridRow(gridData, gridIndex, openingTag, closingTag, url);
            }

            return grid;
        }

        static string ShaveGridRow(string gridData, int gridIndex, string openingTag, string closingTag, [Optional] ActionUrl url)
        {
            gridIndex = gridIndex - 1;
            string grid = string.Empty;
            bool isUrlExist = url != null ? true : false;
            string editLink = string.Empty;
            string deleteLink = string.Empty;

            if (isUrlExist)
            {
                var urls = ComponentHelpers.GetObjectValue(url).Split(SplitChar);
                editLink = urls[0];
                deleteLink = urls[1];
            }
            var links = "";
            foreach (var item in gridData.Split(SplitChar))
                if (item.Length > 0)
                    grid += openingTag + item + closingTag;
            if (editLink.Length > 0)
                links += "<a href=" + editLink + "?id=" + gridIndex + " class=\"btn btn-sm btn-outline-primary\">Edit" + anchorClose;
            if (deleteLink.Length > 0)
                links += @"<a href=" + deleteLink + "?id=" + gridIndex + " class=\"btn btn-sm btn-outline-danger\">Delete" + anchorClose;
            return trOpen + grid + tdOpen + links + tdClose + trClose;
        }
    }
}
