using Apache.NMS;
using System;

namespace ActiveMQ_Producer
{
    class MainProducer
    {
        static void Main(string[] args)
        {
            //var weatherData = new WeatherData();
            //weatherData.FetchData();

            ProducerMessages("Enviando mensagem de teste");
            Console.ReadLine();
        }

        /// <summary>
        /// Structure to send messages to queue of ActiveMQ
        /// </summary>
        /// <param name="msg"></param>
        static public void ProducerMessages(string msg)
        {
            string queueDataMonitoring = "WeatherData";
            string queueDataLog = "WeatherLog";

            Console.WriteLine("*** Created queues: {0}, {1} ***", queueDataMonitoring, queueDataLog);

            string brokerUri = $"activemq:tcp://localhost:61616";
            //string brokerUri = $"stomp:tcp://b-11195e51-8e0a-4016-b040-442d538adeb6-1.mq.us-east-1.amazonaws.com:61614";
            NMSConnectionFactory factory = new NMSConnectionFactory(brokerUri);
            

            using (IConnection connection = factory.CreateConnection())
            {
                connection.Start();

                using (ISession session = connection.CreateSession(AcknowledgementMode.AutoAcknowledge))
                using (IDestination dest = session.GetQueue(1 == 1 ? queueDataMonitoring : queueDataLog))
                using (IMessageProducer producer = session.CreateProducer(dest))
                {
                    producer.DeliveryMode = MsgDeliveryMode.NonPersistent;

                    producer.Send(session.CreateTextMessage("Mensagem para enviar"));
                }
            }
        }
    }
}
