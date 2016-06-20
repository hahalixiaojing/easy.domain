using Easy.Domain.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Domain.Application
{
    public abstract class BaseApplication : IApplication
    {
        private readonly IDictionary<String, IList<IReturnTransformer>> TRANSFORMER = new Dictionary<String, IList<IReturnTransformer>>();
        private readonly IDomainEventManager manager = null;

        public BaseApplication(IDomainEventManager manager)
        {
            this.manager = manager;
        }

        public BaseApplication()
        {
            manager = new TaskDomainEventManager();
        }

        public virtual void RegisterReturnTransformer(string name, IReturnTransformer transformer)
        {
            if (this.TRANSFORMER.ContainsKey(name))
            {
                this.TRANSFORMER[name].Add(transformer);
            }
            else
            {
                var list = new List<IReturnTransformer>();
                list.Add(transformer);
                this.TRANSFORMER.Add(name, list);
            }
        }
        /// <summary>
        /// 注册领域事件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="item"></param>
        public virtual void RegisterDomainEvent(string name, ISubscriber item)
        {
            this.manager.RegisterSubscriber(name, item);
        }
        private IList<IReturnTransformer> GetTransformer(string name)
        {
            return this.TRANSFORMER[name];
        }
        /// <summary>
        /// 写返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mName">方法名称</param>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected virtual IReturn Write<T>(String mName, T obj)
        {
            DefaultReturn<T> @return = new DefaultReturn<T>(obj, this.GetTransformer(mName));
            return @return;
        }
        /// <summary>
        /// 输出并发布领域事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="EventDATA"></typeparam>
        /// <param name="mName"></param>
        /// <param name="obj"></param>
        /// <param name="eventData"></param>
        /// <returns></returns>
        protected virtual IReturn WriteAndPublishDomainEvent<T, EventDATA>(string mName, T obj, EventDATA eventData) where EventDATA : IDomainEvent
        {
            try
            {
                this.manager.PublishEvent<EventDATA>(mName, eventData);
            }
            catch { }
            return this.Write<T>(mName, obj);
        }
        /// <summary>
        /// 发布领域事件
        /// </summary>
        /// <typeparam name="T">事件数据类型</typeparam>
        /// <param name="mName"></param>
        /// <param name="obj"></param>
        protected virtual void PublishEvent<T>(String mName, T obj) where T : IDomainEvent
        {
            this.manager.PublishEvent<T>(mName, obj);
        }
    }
}
