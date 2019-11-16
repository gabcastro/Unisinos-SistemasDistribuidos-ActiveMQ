using Apache.NMS;
using Apache.NMS.ActiveMQ;
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
            var objConsumer = new MessageConsumer();
            string key;

            while (true)
            {
                Console.WriteLine("Wait...");

                objConsumer.ReceiveMessageQueue();
            }
        }
    }
}
