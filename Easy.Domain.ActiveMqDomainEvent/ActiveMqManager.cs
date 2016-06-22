using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apache.NMS;
using Apache.NMS.Util;

namespace Easy.Domain.ActiveMqDomainEvent
{
    public class ActiveMqManager
    {
        readonly IConnection connection;
        readonly ISession session;
        readonly IList<IMessageConsumer> queueConsumers = new List<IMessageConsumer>();
        readonly Dictionary<string, IMessageConsumer> topicConsumers = new Dictionary<string, IMessageConsumer>();
        public ActiveMqManager(string url, string clientid, string usrname, string password)
        {
            this.ClientId = clientid;
            IConnectionFactory factory = new NMSConnectionFactory(url);
            if (!string.IsNullOrEmpty(usrname) && !string.IsNullOrEmpty(password))
            {
                connection = factory.CreateConnection(usrname, password);
            }
            else
            {
                connection = factory.CreateConnection();
            }

            if (!string.IsNullOrEmpty(clientid))
            {
                connection.ClientId = clientid;
            }
            session = connection.CreateSession(AcknowledgementMode.ClientAcknowledge);
            connection.Start();
        }

        public ActiveMqManager(string url) : this(url, string.Empty, string.Empty, string.Empty) { }
        public ActiveMqManager(string url, string clientId) : this(url, clientId, string.Empty, string.Empty) { }

        public string ClientId
        {
            get;private set;
        }
        public IMessageProducer CreateTopicPublisher(string topicName)
        {
            ITopic topic = SessionUtil.GetTopic(session, topicName);
            var producer = session.CreateProducer(topic);
            return producer;
        }

        public IMessageProducer CreateQueueProducer(string queueName)
        {
            IDestination destination = SessionUtil.GetQueue(session, queueName);
            var producer = session.CreateProducer(destination);
            return producer;
        }


        public void RegisterTopicConsumer(string topicName, string subscriberName, MessageListener listener)
        {
            this.RegisterTopicConsumer(topicName, subscriberName, null, listener);
        }
        public void RegisterTopicConsumer(string topicName, string subscriberName, string selector, MessageListener listener)
        {
            ITopic topic = SessionUtil.GetTopic(session, topicName);
            var consumer = session.CreateDurableConsumer(topic, subscriberName, selector, false);
            consumer.Listener += listener;
            topicConsumers.Add(subscriberName, consumer);
        }
        public void RegisterQueueConsumer(string name, int consumerCount, MessageListener listener)
        {
            IDestination destination = session.GetDestination($"queue://{name}");

            int actualcount = consumerCount == 0 ? 1 : consumerCount;
            for (var i = 0; i < actualcount; i++)
            {
                IMessageConsumer consumer = session.CreateConsumer(destination);
                consumer.Listener += listener;
                queueConsumers.Add(consumer);
            }
        }

        ~ActiveMqManager()
        {
            if (connection != null)
            {
                connection.Stop();
                connection.Close();
            }
        }
    }
}
