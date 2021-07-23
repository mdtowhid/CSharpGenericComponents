using ComponentModels;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GenericComponents.Helpers
{
    public static class ExcelHelper
    {
        public static List<T> ExcelEngine<T>(IFormFile file, string commonColumnName) where T : class, new()
        {
            List<T> users = new List<T>();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            List<object> rowDataList = null;
            List<Excel> excels = new List<Excel>();

            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                stream.Position = 0;
                IExcelDataReader excelDataReader = ExcelReaderFactory.CreateReader(stream);

                var conf = new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = a => new ExcelDataTableConfiguration
                    {
                        UseHeaderRow = false
                    }
                };

                DataSet dataSet = excelDataReader.AsDataSet(conf);
                DataRowCollection row = dataSet.Tables["Sheet1"].Rows;
                //DataColumnCollection col = dataSet.Tables["Sheet1"].Columns;

                foreach (DataRow item in row)
                {
                    Excel obj = new Excel();
                    rowDataList = item.ItemArray.ToList();
                    int j = 0;
                    foreach (var i in rowDataList)
                    {
                        SetPropValue(obj, commonColumnName + j, i);
                        j++;
                    }
                    excels.Add(obj);
                }
            }

            return excels as List<T>;
        }

        //public static List<string> ExcelDataAsHTMLData<T>(int htmlTagMode, List<Excel> excels)
        //{
        //    string data = string.Empty;
        //    if(htmlTagMode == 1)
        //    {
        //        int j = 0; Excel obj = new Excel();
        //        foreach (var item in excels)
        //        {
        //            foreach (var pi in item.GetType().GetProperties())
        //            {
                        
        //            }
                    
        //        }
        //    }
        //}

        static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        static object SetPropValue(Excel src, string propName, object value)
        {
            src.GetType().GetProperty(propName).SetValue(src, value.ToString());
            return src;
        }
    }
}
