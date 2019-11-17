using Amazon;
using System;
using System.Collections.Generic;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Constants;
using Amazon.Runtime.CredentialManagement;

namespace ActiveMQ_Consumer
{
    class SendEmail
    {
        /// <summary>
        /// The subject line for the email
        /// </summary>
        public String Subject { get; set; }

        /// <summary>
        /// The subject line for the email
        /// </summary>
        public String TextBody { get; set; }

        /// <summary>
        /// The HTML body of the email
        /// </summary>
        public String HtmlBody { get; set; }

        public SendEmail()
        {
            this.Subject = "Amazon SES test (AWS SDK para .NET)";
            
            this.HtmlBody = @"<html>
                                <head></head>
                                <body>
                                  <h1>Amazon SES Test (AWS SDK para .NET)</h1>
                                  <p>This email was sent with
                                    <a href='https://aws.amazon.com/ses/'>Amazon SES</a> using the
                                    <a href='https://aws.amazon.com/sdk-for-net/'>AWS SDK para .NET</a>.</p>
                                </body>
                              </html>";

            this.TextBody = "Amazon SES Test (.NET)\r\n"
                          + "This email was sent through Amazon SES "
                          + "using the AWS SDK para .NET.";
        }

        public void SendEmailRequest(TopicMessage topicMessage = null)
        {
            if (topicMessage != null) BuildMessage(topicMessage);

            using (var client = new AmazonSimpleEmailServiceClient(RegionEndpoint.USEast1))
            {
                var sendRequest = new SendEmailRequest
                {
                    Source = Globals.SENDER_ADDRESS,
                    Destination = new Destination
                    {
                        ToAddresses =
                        new List<string> { Globals.RECEIVER_ADDRESS }
                    },
                    Message = new Message
                    {
                        Subject = new Content(this.Subject),
                        Body = new Body
                        {
                            Html = new Content
                            {
                                Charset = "UTF-8",
                                Data = this.HtmlBody
                            },
                            Text = new Content
                            {
                                Charset = "UTF-8",
                                Data = this.TextBody
                            }
                        }
                    },
                    // If you are not using a configuration set, comment
                    // or remove the following line 
                    //ConfigurationSetName = Globals.CONFIG_SET
                };
                try
                {
                    Console.WriteLine("Sending email using Amazon SES...");
                    var response = client.SendEmail(sendRequest);
                    Console.WriteLine("The email was sent successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("The email was not sent.");
                    Console.WriteLine("Error message: " + ex.Message);

                }
            }
        }

        public void BuildMessage(TopicMessage topicMessage)
        {
            this.Subject = "Alert from Producer!";
            string bodyMessage = "";

            foreach (var item in topicMessage.Data)
            {
                bodyMessage += string.Format(" <li> {0}: {1} </li> ", item.ReceivedTime.ToString(), item.Message);
            }
            
            this.HtmlBody = string.Format(@"<html>
                <head></head>
                <body>
                    <h2>Status update</h2>
                    <ul> 
                        {0} 
                    </ul>
                </body>
            </html>", bodyMessage);

            this.TextBody = "E-mail provider unsupported message body";
        }
    }
}
