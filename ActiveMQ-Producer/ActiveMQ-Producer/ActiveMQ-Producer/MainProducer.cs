using System.Threading;

namespace ActiveMQ_Producer
{
    class MainProducer
    {        
        public static MessageProduction messageProduction = new MessageProduction();

        static void Main(string[] args)
        {
            int count = 0;
            while (count < 50)
            {
                Thread thread = new Thread(new ThreadStart(SendMessageToServer));
                thread.Start();
                //Thread.Sleep(1000);

                count++;
            }
        }   
        
        public static void SendMessageToServer()
        {
            messageProduction.SendMessageQueue();
        }
    }
}
