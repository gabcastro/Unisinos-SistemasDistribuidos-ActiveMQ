using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveMQ_Consumer
{
    class MessageConsumer
    {
        public ConnectionFactory connectionFactory { get; set; }

        public MessageConsumer()
        {
            this.connectionFactory = new ConnectionFactory(Globals.WIRE_LEVEL_ENDPOINT);
        }

        public void ReceiveMessageQueue()
        {
            using (IConnection connection = connectionFactory.CreateConnection(Globals.ACTIVE_MQ_USERNAME, Globals.ACTIVE_MQ_PASSWORD))
            {
                connection.Start();

                using (ISession session = connection.CreateSession(AcknowledgementMode.AutoAcknowledge))
                using (IDestination consumerDestination = session.GetQueue(Globals.QUEUE_NAME))
                using (IMessageConsumer consumer = session.CreateConsumer(consumerDestination))
                {
                    IMessage msg = consumer.Receive();

                    if (msg is ITextMessage)
                    {
                        ITextMessage txtMsg = msg as ITextMessage;
                        string body = txtMsg.Text;
                        Console.WriteLine("{0}: {1} ", DateTime.Now.ToString(), txtMsg.Text);
                    }
                    else
                    {
                        Console.WriteLine("*** Unexpected message type: {0} ***", msg.GetType().Name);
                    }
                }
                connection.Close();
            }
        }
    }
}
