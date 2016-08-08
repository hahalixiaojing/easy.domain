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
        readonly IList<ISession> sessions = new List<ISession>();
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
            for (var i = 0; i < 10; i++)
            {
                var session = connection.CreateSession(AcknowledgementMode.ClientAcknowledge);
                sessions.Add(session);
            }
            connection.Start();
        }

        public ActiveMqManager(string url) : this(url, string.Empty, string.Empty, string.Empty) { }
        public ActiveMqManager(string url, string clientId) : this(url, clientId, string.Empty, string.Empty) { }

        public string ClientId
        {
            get;private set;
        }

        private ISession GetSession()
        {
            return this.sessions[(int)(DateTime.Now.Ticks % 10)];
        }

        public IMessageProducer CreateTopicPublisher(string topicName)
        {
            ITopic topic = SessionUtil.GetTopic(GetSession(), topicName);
            var producer = GetSession().CreateProducer(topic);
            return producer;
        }

        public IMessageProducer CreateQueueProducer(string queueName)
        {
            IDestination destination = SessionUtil.GetQueue(GetSession(), queueName);
            var producer = GetSession().CreateProducer(destination);
            return producer;
        }


        public void RegisterTopicConsumer(string topicName, string subscriberName, MessageListener listener)
        {
            this.RegisterTopicConsumer(topicName, subscriberName, null, listener);
        }
        public void RegisterTopicConsumer(string topicName, string subscriberName, string selector, MessageListener listener)
        {
            ISession consumserSession = connection.CreateSession(AcknowledgementMode.ClientAcknowledge);
            ITopic topic = SessionUtil.GetTopic(consumserSession, topicName);
            var consumer = consumserSession.CreateDurableConsumer(topic, subscriberName, selector, false);
            consumer.Listener += listener;
            topicConsumers.Add(subscriberName, consumer);
        }
        public void RegisterQueueConsumer(string name, int consumerCount, MessageListener listener)
        {
            ISession consumserSession = connection.CreateSession(AcknowledgementMode.ClientAcknowledge);
            IDestination destination = consumserSession.GetDestination($"queue://{name}");

            int actualcount = consumerCount == 0 ? 1 : consumerCount;
            for (var i = 0; i < actualcount; i++)
            {
                IMessageConsumer consumer = consumserSession.CreateConsumer(destination);
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
