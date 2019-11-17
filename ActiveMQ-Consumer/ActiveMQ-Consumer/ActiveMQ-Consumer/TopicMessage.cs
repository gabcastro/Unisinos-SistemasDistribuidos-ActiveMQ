using System;
using System.Collections.Generic;

namespace ActiveMQ_Consumer
{
    public class TopicMessage
    {
        public IList<TopicMessageData> Data { get; set; }

        public TopicMessage()
        {
            this.Data = new List<TopicMessageData>();
        }
    }

    public class TopicMessageData
    {
        public String Message { get; set; }
        public DateTime ReceivedTime { get; set; }
    }
}
