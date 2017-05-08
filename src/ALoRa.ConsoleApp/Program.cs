using ALoRa.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALoRa.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nALoRa ConsoleApp - A The Things Network C# Library\n");

            var app = new TTNApplication("therm-io", "ttn-account-v2._4qH38SGjH9glrlFITfokDMq4-csR65DrIRFryeZAGY", "eu");
            app.MessageReceived += App_MessageReceived;

            Console.WriteLine("Press return to exit!");
            Console.ReadLine();

            app.Dispose();

            Console.WriteLine("\nAloha, Goodbye, Vaarwel!");

            System.Threading.Thread.Sleep(1000);
        }

        private static void App_MessageReceived(TTNMessage obj)
        {
            var data = BitConverter.ToString(obj.Payload);
            Console.WriteLine($"Message Timestamp: {obj.Timestamp}, Device: {obj.DeviceID}, Topic: {obj.Topic}, Payload: {data}");
        }
    }
}
