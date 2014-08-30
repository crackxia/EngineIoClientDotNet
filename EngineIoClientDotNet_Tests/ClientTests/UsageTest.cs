﻿using log4net;
using Quobject.EngineIoClientDotNet.Client;
using System;

using Xunit;

namespace Quobject.EngineIoClientDotNet_Tests.ClientTests
{
    public class UsageTest : Connection
    {


        [Fact]
        public void Usage1()
        {
            Socket.SetupLog4Net();
            var log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            var options = CreateOptions();
            var socket = new Socket(options);

            //You can use `Socket` to connect:
            //var socket = new Socket("ws://localhost");
            socket.On(Socket.EVENT_OPEN, () =>
            {
                socket.Send("hi", () =>
                {
                    socket.Close();
                });
            });
            socket.Open();

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
        }

        [Fact]
        public void Usage2()
        {
            Socket.SetupLog4Net();
            var log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            //Receiving data
            var socket = new Socket("ws://localhost");
            socket.On(Socket.EVENT_OPEN, () =>
            {
                socket.On(Socket.EVENT_MESSAGE, (data) =>
                {
                    var dataString = (string)data;
                    Console.WriteLine(dataString);
                    socket.Close();
                });
            });
            socket.Open();

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
        }

   
    }
}