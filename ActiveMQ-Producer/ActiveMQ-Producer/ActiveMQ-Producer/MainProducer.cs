using Apache.NMS;
using Apache.NMS.ActiveMQ;
using System;
using Constants;

namespace ActiveMQ_Producer
{
    class MainProducer
    {        
        static void Main(string[] args)
        {
            var messageProduction = new MessageProduction();
            messageProduction.SendMessageQueue();
        }    
    }
}
