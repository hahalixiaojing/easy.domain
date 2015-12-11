using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Domain.Application
{
    public class DefaultReturnTransformerLoader : IReturnTransformerLoader
    {
        public IDictionary<string, IEnumerable<IReturnTransformer>> Find(IApplication application)
        {
            Type type = application.GetType();

            IEnumerable<MethodInfo> methods = type.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).Where(m => m.ReturnType == typeof(IReturn));

            String name = type.Name.Substring(0, type.Name.LastIndexOf("Application"));

            var returns = new Dictionary<String, IEnumerable<IReturnTransformer>>();
            foreach (var m in methods)
            {
                string mName = m.Name;
                string @namespace = type.Namespace + "." + name + "." + mName + ".";

                IEnumerable<IReturnTransformer> types = type.Assembly.GetTypes().Where(a => a.FullName.Contains(@namespace)).Select(t => Activator.CreateInstance(t) as IReturnTransformer).Where(o => o != null);

                returns.Add(mName, types);
            }
            return returns;
        }
    }
}
