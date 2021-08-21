using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Database;
using System.Data;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Hosting;

namespace GenericComponents.Controllers
{
    public class DbSchemaController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DbSchemaController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }


        public IActionResult Index()
        {
            ViewBag.Tables = ListTables();
            List<List<string>> schemas = new List<List<string>>();
            foreach (string tn in ViewBag.Tables)
            {
                schemas.Add(ListOfTableSchema(tn));
            }

            ViewBag.TablesSchemas = schemas;

            bool c = CreateDirectory("DynamicFilesFolderPath");
            if (c == false)
            {
                CreateAndWriteInFiles(ViewBag.Tables);
            }
            GetAllFilesNameFromFolder("DynamicFilesFolderPath");
            return View();
        }

        public List<string> ListTables()
        {
            SqlConnection _connection = DatabaseInfo.GetSqlConnection();
            List<string> tables = new List<string>();
            _connection.Open();
            DataTable dt = _connection.GetSchema("Tables");
            foreach (DataRow row in dt.Rows)
            {
                string tablename = (string)row[2];
                tables.Add(tablename);
            }
            _connection.Close();
            return tables;
        }

        public List<string> ListOfTableSchema(string tableName)
        {
            SqlConnection _connection = DatabaseInfo.GetSqlConnection();
            List<string> tableSchemas = new List<string>();
            _connection.Open();


            string q = @"SELECT COLUMN_NAME AS ColName, DATA_TYPE AS DataType
                FROM INFORMATION_SCHEMA.COLUMNS
                WHERE TABLE_NAME = '" + tableName + "' order by COLUMN_NAME ASC";

            SqlCommand cmd = DatabaseInfo.GetSqlCommand(q, _connection);
            using(SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    tableSchemas.Add(reader["ColName"].ToString() + "_" + reader["DataType"].ToString());
                }
            }

            _connection.Close();

            return tableSchemas;
        }

        public string[] GetAllFilesNameFromFolder(string folderPath)
        {
            string c =  _webHostEnvironment.WebRootPath;
            string combined = Path.Combine(c, folderPath);
            string[] filePaths = new string[] { };
            if (Directory.Exists(combined))
            {
                filePaths = Directory.GetFiles(c);
            }
            else
            {
                Directory.CreateDirectory(combined);
            }

            return filePaths;
        }

        public bool CreateDirectory(string folderPath)
        {
            string c = _webHostEnvironment.WebRootPath;
            string combined = Path.Combine(c, folderPath);
            DirectoryInfo di = null;
            if (!Directory.Exists(combined))
            {
                di = Directory.CreateDirectory(combined);
            }

            return di != null ? true : false;
        }

        public void CreateAndWriteInFiles(List<string> tableNames)
        {
            string c = _webHostEnvironment.WebRootPath + "\\DynamicFilesFolderPath";

            foreach (string fileName in tableNames)
            {
                string pathString = Path.Combine(c, fileName + ".cs");
                string contents = "";
                foreach (string colType in ListOfTableSchema(fileName))
                {
                    string[] colTypeArray = colType.Split('_');
                    string colName = colTypeArray[0];
                    string type = colTypeArray[1];
                    contents += "public " + GetActualType(type) + " " + colName + " { get; set; }\n";
                }
                System.IO.File.WriteAllText(pathString, contents);
            }
        }

        private string GetActualType(string type)
        {
            type = type.ToLower();
            switch (type)
            {
                case "nvarchar":
                    type = "string";
                    break;
                case "varchar":
                    type = "string";
                    break;
                case "bigint":
                    type = "long";
                    break;
                case "bit":
                    type = "bool";
                    break;
                case "datetime":
                    type = "DateTime";
                    break;
                default:
                    break;
            }

            return type;
        }
    }
}
