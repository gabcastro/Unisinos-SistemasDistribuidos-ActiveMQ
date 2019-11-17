using System;

namespace ActiveMQ_Consumer
{
    class MainConsumer
    {
        static void Main(string[] args)
        {
            var objConsumer = new MessageConsumer();
            var objEmail = new SendEmail();
            TopicMessage message = new TopicMessage();

            while (true)
            {
                Console.WriteLine("... Wait...\n");

                message.Data.Add(objConsumer.ReceiveMessageQueue());

                objEmail.SendEmailRequest(message);
            }

        }
    }
}
