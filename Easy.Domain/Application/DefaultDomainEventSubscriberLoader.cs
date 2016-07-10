﻿using Easy.Domain.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Domain.Application
{
    public class DefaultDomainEventSubscriberLoader : IDomainEventSubscriberLoader
    {
        public readonly string[] excludeMethods = { "RegisterReturnTransformer", "RegisterDomainEvents", "RegisterReturnTransformer", "RegisterDomainEvent" };


        public IDictionary<string, IEnumerable<ISubscriber>> Find(IApplication application)
        {
            Type type = application.GetType();

            IEnumerable<MethodInfo> methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(m => !excludeMethods.Contains(m.Name));
            
            String name = type.Name.Substring(0, type.Name.LastIndexOf("Application"));

            var returns = new Dictionary<String, IEnumerable<ISubscriber>>();
            foreach (var m in methods)
            {
                string mName = m.Name;
                string @namespace = type.Namespace + "." + name + "." + mName + "DomainEvents" + ".";

                var targetTypes = type.Assembly.GetTypes()
                    .Where(a => a.FullName.ToUpperInvariant().Contains(@namespace.ToUpperInvariant()))
                    .Where(a => !a.IsAbstract)
                    .Where(a => a.GetInterface("ISubscriber") != null);

                IEnumerable<ISubscriber> types = targetTypes.Select(t => Activator.CreateInstance(t) as ISubscriber).Where(o => o != null);
                if (types.Count() > 0)
                {
                    returns.Add(mName, types);
                }
            }
            return returns;
        }


    }
}
