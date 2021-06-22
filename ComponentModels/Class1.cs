using System;
using System.Collections.Generic;
using System.Reflection;

namespace ComponentModels
{
    public class Class1<T> where T: class, new()
    {
        private object ObjectTypeOfT { get; set; }
        private List<object> ListOfObjectsTypeOfT { get; set; }
        private object this[string PropertyName]
        {
            get
            {
                Type myType = typeof(T);
                System.Reflection.PropertyInfo pi = myType.GetProperty(PropertyName);
                return pi.GetValue(ObjectTypeOfT, null);
            }
            set
            {
                Type myType = typeof(T);
                System.Reflection.PropertyInfo pi = myType.GetProperty(PropertyName);
                pi.SetValue(ObjectTypeOfT, value, null);
            }
        }

        public dynamic X(T o)
        {
            var r = new Class1<T>();
            r.ObjectTypeOfT = o;
            foreach (PropertyInfo pi in o.GetType().GetProperties())
            {
                var propName = pi.Name;
                var propValue = pi.GetValue(o, null);
                r[propName] = propValue;
            }

            return r.ObjectTypeOfT as T;
        }
    }
}
