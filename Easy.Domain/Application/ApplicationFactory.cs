using Easy.Domain.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Domain.Application
{
    /// <summary>
    /// 应用服务工厂
    /// </summary>
    public class ApplicationFactory
    {
        private static readonly IDictionary<string, IApplication> applicationService = new Dictionary<string, IApplication>();

        private static object lockObj = new object();
        private static ApplicationFactory factory;
        private static IReturnTransformerLoader returnTransfomerLoader;
        private static IDomainEventSubscriberLoader domainEventSubscriberLoader;
        private ApplicationFactory(IReturnTransformerLoader loader = null, IDomainEventSubscriberLoader loader2 = null)
        {
            ApplicationFactory.returnTransfomerLoader = loader ?? new DefaultReturnTransformerLoader();
            ApplicationFactory.domainEventSubscriberLoader = loader2 ?? new DefaultDomainEventSubscriberLoader();
        }

        public static ApplicationFactory Instance(IReturnTransformerLoader loader = null)
        {
            if (factory == null)
            {
                lock (lockObj)
                {
                    if (factory == null)
                    {
                        factory = new ApplicationFactory(loader);
                    }
                }
            }
            return factory;
        }
        /// <summary>
        /// 注册应用服务
        /// </summary>
        /// <param name="application"></param>
        public void Register(IApplication application)
        {
            BaseApplication baseApplication = application as BaseApplication;
            this.RegisterReturnTransformer(baseApplication);
            this.RegisterDomainEventSubscriber(baseApplication);
            applicationService.Add(application.GetType().FullName.ToUpper(), application);
        }

        internal void RegisterByInterface(IApplication application, string interfaceTypeFullName)
        {
            BaseApplication baseApplication = application as BaseApplication;
            this.RegisterReturnTransformer(baseApplication);
            this.RegisterDomainEventSubscriber(baseApplication);

            applicationService.Add(interfaceTypeFullName.ToUpper(), application);
        }

        private void RegisterDomainEventSubscriber(BaseApplication application)
        {
            var domainEvents = domainEventSubscriberLoader.Find(application);
            foreach (KeyValuePair<String, IEnumerable<ISubscriber>> keypair in domainEvents)
            {
                foreach (var item in keypair.Value)
                {
                    application.RegisterDomainEvent(keypair.Key, item);
                }
            }
        }
        private void RegisterReturnTransformer(BaseApplication baseApplication)
        {
            var transformers = returnTransfomerLoader.Find(baseApplication);
            foreach (KeyValuePair<string, IEnumerable<IReturnTransformer>> keypair in transformers)
            {
                foreach (var item in keypair.Value)
                {
                    baseApplication.RegisterReturnTransformer(keypair.Key, item);
                }
            }
        }
        /// <summary>
        /// 获得应用服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>() where T : class ,IApplication
        {
            string fullName = typeof(T).FullName.ToUpper();

            if (applicationService.ContainsKey(fullName))
            {
                return applicationService[fullName] as T;
            }
            return null;
        }
        /// <summary>
        /// 根据接口类型，获得应用程序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetByInterface<T>() where T : class
        {
            string fullName = typeof(T).FullName.ToUpper();
            if (applicationService.ContainsKey(fullName))
            {
                return applicationService[fullName] as T;
            }
            return null;
        }
    }
}
