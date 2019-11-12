using Apache.NMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveMQ_Consumer
{
    class ConsumerMessage
    {
        public ConsumerMessage() { }

        public bool ReadQueue()
        {
            string queueName = Consts.QUEUEDATA;
            string brokerUri = Consts.BROKEN_URI;

            NMSConnectionFactory factory = new NMSConnectionFactory(brokerUri);

            using (IConnection connection = factory.CreateConnection())
            {
                connection.Start();
                using (ISession session = connection.CreateSession(AcknowledgementMode.AutoAcknowledge))
                using (IDestination dest = session.GetQueue(queueName))
                using (IMessageConsumer consumer = session.CreateConsumer(dest))
                {
                    IMessage msg = consumer.Receive();
                    if (msg is ITextMessage)
                    {
                        ITextMessage txtMsg = msg as ITextMessage;
                        string body = txtMsg.Text;
                        Console.WriteLine("*** Received message: {0} ***", txtMsg.Text);
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("*** Unexpected message type: {0} ***", msg.GetType().Name);
                    }
                }
            }
            return false;
        }
    }


}
