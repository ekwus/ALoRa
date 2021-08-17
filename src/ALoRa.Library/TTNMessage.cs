﻿using System;
using System.Text;
using Newtonsoft.Json;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace ALoRa.Library
{
    public class TTNMessage
    {
        public static TTNMessage DeserialiseMessage(MqttMsgPublishEventArgs evt)
        {
            var text = Encoding.ASCII.GetString(evt.Message);

            var lora = JsonConvert.DeserializeObject<LoRaMessage>(text);

            var msg = new TTNMessage(lora, evt.Topic);

            return msg;
        }

        public DateTime? Timestamp { get; set; }
        public string DeviceID { get; set; }
        public LoRaMessage RawMessage { get; set; }
        public string Topic { get; set; }
        public byte[] Payload { get; set; }

        public TTNMessage(LoRaMessage msg, string topic)
        {
            Timestamp = msg.metadata.time;
            DeviceID = msg.dev_id;
            RawMessage = msg;
            Topic = topic;
            if (msg.payload_raw != null)
            {
                Payload = Convert.FromBase64String(msg.payload_raw);
            }
        }
    }
}
