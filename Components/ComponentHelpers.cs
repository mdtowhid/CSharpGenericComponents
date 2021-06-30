using ComponentModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Components
{
    public class ComponentHelpers
    {
        static string SplitChar { get; set; } = "_";
        static List<string> HeaderList { get; set; }
        static List<string> Rows { get; set; }

        static string GetTypeName<T>() where T : class, new() => typeof(T).GetType().Name;

        public static List<string> GetPropertyNames<T>(T model) where T : class
        {
            List<string> names = new List<string>();
            foreach (var pi in model.GetType().GetProperties()) names.Add(pi.Name);
            return names;
        }

        public static List<string> GetPropertyValues<T>(List<T> models) where T : class
        {
            List<string> values = new List<string>();
            foreach (var item in models)
            {
                values.Add(GetObjectValue(item));
            }

            return values;
        }

        public static string GetObjectValue<T>(T model) where T: class
        {
            string values = "";
            foreach (var pi in model.GetType().GetProperties())
            {
                values += pi.GetValue(model, null) + SplitChar;
            }
            return values;
        }

        


        public string Splitter { get; set; }
        public List<string> PropNames { get; set; }
        public List<string> ActionPropNames { get; set; }
        public string ActionPropValues { get; set; }
        public List<string> GridRows { get; set; }
        public static ComponentHelpers GetGridHeadersWithSingleRows<T>(List<T> models, ActionUrl urls) where T : class
        {
            Rows = new List<string>();
            foreach (T model in models)
            {
                Rows.Add(GetObjectValue(model));
            }

            return new ComponentHelpers()
            {
                Splitter = SplitChar,
                ActionPropNames = GetPropertyNames(urls),
                GridRows = Rows,
                PropNames = GetPropertyNames(models[0]),
                ActionPropValues = GetObjectValue(urls)
            };
        }
    }
}
