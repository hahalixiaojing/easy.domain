using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apache.NMS;
using Easy.Domain.Application;
using Easy.Domain.Event;
using Newtonsoft.Json;

namespace Easy.Domain.ActiveMqDomainEvent
{
    /// <summary>
    /// 基于ActiveMq的事件处理
    /// </summary>
    public class ActiveMqDomainEventManager : IDomainEventManager
    {
        readonly ActiveMqManager manager;
        readonly IDictionary<string, IMessageProducer> producers = new Dictionary<string, IMessageProducer>();
        public ActiveMqDomainEventManager(ActiveMqManager manager)
        {
            this.manager = manager;
        }

        public void PublishEvent<T>(string name, T obj) where T : IDomainEvent
        {
            string fullname = obj.GetType().FullName;
            if (producers.ContainsKey(fullname))
            {
                var message = producers[fullname].CreateTextMessage();
                message.Text = JsonConvert.SerializeObject(obj);
                message.Properties.SetString("route", this.manager.ClientId);
                producers[fullname].Send(message, MsgDeliveryMode.Persistent, MsgPriority.Normal, new TimeSpan(0));
            }
        }
        /// <summary>
        /// 注册订阅者
        /// </summary>
        /// <param name="name"></param>
        /// <param name="item"></param>
        public void RegisterSubscriber(string name, ISubscriber item)
        {
            string fullName = item.SuscribedToEventType.FullName;

            manager.RegisterTopicConsumer(fullName, item.GetType().FullName, $"route='{this.manager.ClientId}'", (msg) =>
             {
                 ITextMessage textMsg = msg as ITextMessage;
                 var sub = item as IActiveMqDomainEventSubscriber;
                 sub.HandleEvent(textMsg.Text);

                 msg.Acknowledge();
             });
        }
        /// <summary>
        /// 注册领域事件
        /// </summary>
        /// <param name="domainEventTypes"></param>
        public void RegisterDomainEvent(IList<Type> domainEventTypes)
        {
            foreach (var item in domainEventTypes)
            {
               var producer = manager.CreateTopicPublisher(item.FullName);
                producers.Add(item.FullName, producer);
            }
        }
    }
}
