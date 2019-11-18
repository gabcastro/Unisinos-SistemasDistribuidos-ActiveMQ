using System;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Constants;

namespace ActiveMQ_Producer
{
    class MessageProduction
    {
        public ConnectionFactory connectionFactory { get; set; }

        public MessageProduction()
        {
            this.connectionFactory = new ConnectionFactory(Globals.WIRE_LEVEL_ENDPOINT);
        }

        public void SendMessageQueue()
        {
            using (IConnection connection = this.connectionFactory.CreateConnection(Globals.ACTIVE_MQ_USERNAME, Globals.ACTIVE_MQ_PASSWORD))
            {
                connection.Start();

                using (ISession session = connection.CreateSession(AcknowledgementMode.AutoAcknowledge))
                using (IDestination producerDestination = session.GetQueue(Globals.QUEUE_NAME))
                using (IMessageProducer producer = session.CreateProducer(producerDestination))
                {
                    producer.DeliveryMode = MsgDeliveryMode.NonPersistent;
                    
                    String msg = string.Format("Information sended to server MQ of Amazon!!!");
                    
                    ITextMessage message = session.CreateTextMessage(msg);

                    producer.Send(message);

                    Console.WriteLine("INFO: Successful in sending message to server");
                }
            }
        }
    }
}
