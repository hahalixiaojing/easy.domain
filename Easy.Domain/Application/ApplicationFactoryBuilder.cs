using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;

namespace Easy.Domain.Application
{
    public class ApplicationFactoryBuilder : BaseFactoryBuilder
    {
        public ApplicationFactory Build(FileInfo fileinfo)
        {
            if (!fileinfo.Exists)
            {
                throw new FileNotFoundException(fileinfo.FullName);
            }
            var navi = CreateXpathNavi(fileinfo);
            var applications = Load(navi);
            foreach (var item in applications)
            {
                ApplicationFactory.Instance().RegisterByInterface(item.Value, item.Key);
            }

            return ApplicationFactory.Instance();
        }
        public ApplicationFactory Build(Stream stream)
        {
            var navi = CreateXpathNavi(stream);
            var applications = Load(navi);
            foreach (var item in applications)
            {
                ApplicationFactory.Instance().RegisterByInterface(item.Value, item.Key);
            }
            return ApplicationFactory.Instance();
        }

        private Dictionary<string, IApplication> Load(XPathNavigator navigator)
        {
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(navigator.NameTable);
            namespaceManager.AddNamespace("abc", "http://www.39541240.com/applications");
            namespaceManager.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");

            Boolean global_enable_interceptor = Convert.ToBoolean(ConfigurationManager.AppSettings["global_enable_interceptor"] ?? "true");

            Dictionary<string, IApplication> applications = new Dictionary<string, IApplication>();

            XPathNodeIterator it = navigator.Select("abc:applications/abc:application", namespaceManager);
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
                IApplication application = null;
                Type interceptorType = Type.GetType(interceptor);
                if (global_enable_interceptor && enable_interceptor == "TRUE" && interceptorType != null)
                {
                    IInterceptor instance = Activator.CreateInstance(interceptorType) as IInterceptor;
                    application = new ProxyGenerator().CreateClassProxy(implementationType, instance) as IApplication;
                }
                else
                {
                    application = Activator.CreateInstance(implementationType) as IApplication;
                }
                if (application == null)
                {
                    throw new NullReferenceException("application is null");
                }
                applications.Add(interfaceType.FullName.ToUpper(), application);
            }
            return applications;
        }
    }
}
