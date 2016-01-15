using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace Easy.Domain.ServiceFramework
{
    public class ServiceFactory
    {
        private IDictionary<String, IService> services = new Dictionary<String, IService>();


        public ServiceFactory(XPathNavigator navigator)
        {
            this.Load(navigator);
        }

        private void Load(XPathNavigator navigator) 
        {
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(navigator.NameTable); //namespace   
            namespaceManager.AddNamespace("abc", "http://www.39541240.com/services");
            namespaceManager.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");

            XPathNodeIterator it = navigator.Select("abc:services/abc:service", namespaceManager);
            foreach (XPathNavigator navi in it)
            {
                String interfaceName = navi.GetAttribute("interface", "");
                String implementationName = navi.GetAttribute("implementation", "");
                String interceptor = navi.GetAttribute("interceptor", "");
                String enable_interceptor = navi.GetAttribute("enable_interceptor", "").ToUpper();

                Type interfaceType = Type.GetType(interfaceName);
                Type implementationType = Type.GetType(implementationName);
                if (implementationType == null || interfaceName == null)
                {
                    throw new NotImplementedException("没有实现接口" + interfaceName);
                }
                IService service = null;
                Type interceptorType = Type.GetType(interceptor);
                if (enable_interceptor == "TRUE" && interceptorType != null)
                {
                    IInterceptor instance = Activator.CreateInstance(interceptorType) as IInterceptor;
                    service = new ProxyGenerator().CreateClassProxy(implementationType, instance) as IService;
                }
                else
                {
                    service = Activator.CreateInstance(implementationType) as IService;
                }
                if (service == null)
                {
                    throw new NullReferenceException("service is null");
                }
                services.Add(interfaceType.FullName, service);
            }
        }
        public TService Get<TService>()
           where TService : class
        {

            Type @interface = typeof(TService);
            if (services.ContainsKey(@interface.FullName))
            {
                TService repository = services[@interface.FullName] as TService;
                return repository;
            }
            return null;
        }

    }
}
