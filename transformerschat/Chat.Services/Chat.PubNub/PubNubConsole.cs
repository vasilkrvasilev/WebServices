using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.PubNub
{
    public static class PubNubConsole
    {
        private static PubnubAPI pubnub = new PubnubAPI(
            "pub-c-e32a4279-7190-4426-8ca8-e2771abeb561", // PUBLISH_KEY
                "sub-c-3a318502-059b-11e3-991c-02ee2ddab7fe",               // SUBSCRIBE_KEY
                "sec-c-ZDQzZjgzNWItOWJlYy00NTYzLWIzZGYtZWFkNjJmM2QzOTk0",   // SECRET_KEY
                true                                                        // SSL_ON?
            );

        private static string channel = "chat-channel";

        public static void Publish(string message)
        {
            string formattedMessage = string.Format("{0}: {1}", pubnub.Time().ToString(), message);
            pubnub.Publish(channel, formattedMessage);
        }

        //public static void Print()
        //{
        //    // Start the HTML5 Pubnub client
        //    Process.Start("..\\..\\PubNub-HTML5-Client.html");

        //    // Subscribe for receiving messages (in a background task to avoid blocking)
        //    Task task = new Task(
        //        () =>
        //        pubnub.Subscribe(
        //            channel,
        //            delegate(object message)
        //            {
        //                Console.WriteLine(message.ToString());
        //                return true;
        //            }
        //        )
        //    );

        //    task.Start();
        //}
    }
}
