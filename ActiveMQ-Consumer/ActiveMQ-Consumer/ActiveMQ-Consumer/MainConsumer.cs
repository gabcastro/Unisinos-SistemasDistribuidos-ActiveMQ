using Apache.NMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveMQ_Consumer
{
    class MainConsumer
    {
        static void Main(string[] args)
        {
            ConsumerMessage cm = new ConsumerMessage();
            
            Console.WriteLine("Waiting for messages");
            while (cm.ReadQueue())
            {
                Console.WriteLine("Successfully read message");
            }
            Console.WriteLine("Finished");
        }        
    }
}
