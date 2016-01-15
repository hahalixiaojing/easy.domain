using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Domain.Base;
using System.Xml.XPath;
using System.IO;
using System.Xml;
using Castle.DynamicProxy;
using System.Configuration;

namespace Easy.Domain.RepositoryFramework
{
    public class RepositoryFactory
    {
        private IDictionary<String, IDao> repositories = new SortedDictionary<String, IDao>();
        public RepositoryFactory(FileInfo fileInfo)
        {
            var navi = this.CreateXpathNavi(fileInfo);
            this.Load(navi);
        }
        public RepositoryFactory(Stream stream)
        {
            var navi = this.CreateXpathNavi(stream);
            this.Load(navi);
        }
        private XPathNavigator CreateXpathNavi(FileInfo fileInfo)
        {
            if (!fileInfo.Exists)
            {
                throw new FileNotFoundException(fileInfo.FullName);
            }
            XPathDocument doc = new XPathDocument(fileInfo.FullName);
            XPathNavigator navigator = doc.CreateNavigator();
            return navigator;
        }
        private XPathNavigator CreateXpathNavi(Stream stream)
        {
            XPathDocument doc = new XPathDocument(stream);
            XPathNavigator navigator = doc.CreateNavigator();
            return navigator;
        }
        private void Load(XPathNavigator navigator) 
        {
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(navigator.NameTable);  
            namespaceManager.AddNamespace("abc", "http://www.39541240.com/repository");
            namespaceManager.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");

            Boolean global_enable_interceptor = Convert.ToBoolean(ConfigurationManager.AppSettings["global_enable_interceptor"] ?? "true");

            XPathNodeIterator it = navigator.Select("abc:repositories/abc:repository", namespaceManager);
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
                IDao dao = null;
                Type interceptorType = Type.GetType(interceptor);
                if (global_enable_interceptor && enable_interceptor == "TRUE" && interceptorType != null)
                {
                    IInterceptor instance = Activator.CreateInstance(interceptorType) as IInterceptor;
                    dao = new ProxyGenerator().CreateClassProxy(implementationType, instance) as IDao;
                }
                else
                {
                    dao = Activator.CreateInstance(implementationType) as IDao;
                }
                if (dao == null)
                {
                    throw new NullReferenceException("dao is null");
                }

                repositories.Add(interfaceType.FullName, dao);
            }
        }
        public  TRepository Get<TRepository, TEntity>()
            where TRepository : class
            where TEntity : IAggregateRoot
        {
            return this.Get<TRepository>();
        }
        public TRepository Get<TRepository>()
            where TRepository : class
        {

            Type @interface = typeof(TRepository);
            if (repositories.ContainsKey(@interface.FullName))
            {
                TRepository repository = repositories[@interface.FullName] as TRepository;
                return repository;
            }
            return null;
        }
    }
}
