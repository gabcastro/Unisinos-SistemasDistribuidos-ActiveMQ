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
            NMSConnectionFactory factory = new NMSConnectionFactory(brokerUri);           

            using (IConnection connection = factory.CreateConnection())
            {
                connection.Start();

                using (ISession session = connection.CreateSession(AcknowledgementMode.AutoAcknowledge))
                using (IDestination dest = session.GetQueue(1 == 1 ? queueDataMonitoring : queueDataLog))
                using (IMessageProducer producer = session.CreateProducer(dest))
                {
                    producer.DeliveryMode = MsgDeliveryMode.NonPersistent;
                    ITextMessage message = session.CreateTextMessage(msg);

                    long time = 30 * 1000;
                    long period = 10 * 1000;
                    int repeat = 9;

                    message.Properties["AMQ_SCHEDULE_DELAY"] = time;
                    message.Properties["AMQ_SCHEDULE_PERIOD"] = period;
                    message.Properties["AMQ_SCHEDULE_REPEAT"] = repeat;

                    producer.Send(message);
                }
            }
        }
    }
}
