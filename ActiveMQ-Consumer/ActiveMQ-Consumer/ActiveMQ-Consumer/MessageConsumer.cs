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
        public ConnectionFactory ConnectionFactory { get; set; }
        public String MessageFromProducer { get; set; }
        public TopicMessageData MessageReceived { get; set; }

        public MessageConsumer()
        {
            this.MessageReceived = new TopicMessageData();
            this.ConnectionFactory = new ConnectionFactory(Globals.WIRE_LEVEL_ENDPOINT);
        }

        public TopicMessageData ReceiveMessageQueue()
        {
            using (IConnection connection = ConnectionFactory.CreateConnection(Globals.ACTIVE_MQ_USERNAME, Globals.ACTIVE_MQ_PASSWORD))
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
                        this.MessageFromProducer = txtMsg.Text;
                        
                        Console.WriteLine();
                        Console.WriteLine("{0}: {1} ", DateTime.Now.ToString(), this.MessageFromProducer);
                        Console.WriteLine();

                        this.MessageReceived.Message = this.MessageFromProducer;
                        this.MessageReceived.ReceivedTime = DateTime.Now;
                    }
                    else
                    {
                        Console.WriteLine("*** Unexpected message type: {0} ***", msg.GetType().Name);

                        this.MessageReceived.Message = msg.GetType().Name;
                        this.MessageReceived.ReceivedTime = DateTime.Now;
                    }
                }
                connection.Close();
            }

            return this.MessageReceived;
        }
    }
}
