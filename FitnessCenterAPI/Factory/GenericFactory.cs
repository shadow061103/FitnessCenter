using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitnessCenterAPI.Factory
{
    public class GenericFactory//泛型工廠 不需要再建立工廠類別
    {
        public static T CreateInastance<T>(string assemblyname, string typename)
        {
            return CreateInastance<T>(assemblyname, typename, null);
        }

        public static T CreateInastance<T>(string assemblyname, string typename, object[] args)
        {
            object instance = Activator.CreateInstance(assemblyname, typename, args).Unwrap();
            return (T)instance;
        }

        public static T CreateInastance<T>(Type type)

        {
            return CreateInastance<T>(type, null);
        }

        public static T CreateInastance<T>(Type type, object[] args)
        {
            object instance = Activator.CreateInstance(type, args);
            return (T)instance;
        }
    }
}