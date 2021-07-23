using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ComponentModels
{
    public class Class1<T> where T: class, new()
    {
        private object ObjectTypeOfT { get; set; }
        private List<T> ListOfObjectsTypeOfT { get; set; }
        private object this[string PropertyName]
        {
            get
            {
                Type myType = typeof(T);
                PropertyInfo pi = myType.GetProperty(PropertyName);
                return pi.GetValue(ObjectTypeOfT, null);
            }
            set
            {
                Type myType = typeof(T);
                PropertyInfo pi = myType.GetProperty(PropertyName);
                pi.SetValue(ObjectTypeOfT, value, null);
            }
        }
        
        public static T AutoObjectMapper(IDataReader dr, T o, [Optional] int drIndex)
        {
            var r = new Class1<T>();
            r.ObjectTypeOfT = o;
            foreach (PropertyInfo pi in o.GetType().GetProperties())
            {
                var propName = pi.Name;
                var propValue = pi.GetValue(o, null);
                var type = pi.GetType();
                r[propName] = propValue;
                if (dr != null) 
                {
                    var drIndexName = dr.GetName(drIndex);
                    if (!string.IsNullOrEmpty(drIndexName))
                    {
                        r[propName] = dr[propName].ToString();
                    }
                }
            }

            return r.ObjectTypeOfT as T;
        }

        public static List<T> AutoObjectsMapper(IDataReader dr, List<T> models)
        {
            List<T> modelsToReturn = new List<T>();
            int drIndex = 0;
            foreach (T t in models)
            {
                modelsToReturn.Add(AutoObjectMapper(dr, t, drIndex));
                drIndex++;
            }
            return modelsToReturn;
        }
    }
}
