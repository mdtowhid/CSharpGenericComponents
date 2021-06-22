using ComponentInterfaces;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ComponentModels
{
    public class ApiMethodsHelper<T>
    {
        public static T Get(List<T> models, string id)
        {
            object result = null;
            foreach (var item in models)
            {
                IModelBase gId = (IModelBase)item;
                if(gId.Id == id)
                    result = item;
                if (result != null)
                    break;
            }
            return (T)result;
        }
    }
}
