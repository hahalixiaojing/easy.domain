using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Domain.Application
{
    public class DefaultDomainEventLoader : IDomainEventLoader
    {
        public readonly string[] excludeMethods = { "RegisterReturnTransformer", "RegisterDomainEvents", "RegisterReturnTransformer", "RegisterDomainEvent" };

        public IList<Type> Load(IApplication application)
        {
            Type type = application.GetType();

            IEnumerable<MethodInfo> methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(m => !excludeMethods.Contains(m.Name));

            String name = type.Name.Substring(0, type.Name.LastIndexOf("Application"));

            var returns = new List<Type>();
            foreach (var m in methods)
            {
                string mName = m.Name;
                string @namespace = type.Namespace + "." + name + "." + mName + "DomainEvents" + ".";

                var targetTypes = type.Assembly.GetTypes()
                    .Where(a => a.FullName.Contains(@namespace))
                    .Where(a => !a.IsAbstract)
                    .Where(a => a.GetInterface("IDomainEvent") != null);

                returns.AddRange(targetTypes);
            }
            return returns;
        }
    }
}
