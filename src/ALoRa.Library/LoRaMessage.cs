using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALoRa.Library
{
    public class LoRaMessage
    {
        public string app_id { get; set; }
        public string dev_id { get; set; }
        public string hardware_serial { get; set; }
        public string payload_raw { get; set; }
        public int port { get; set; }
        public int counter { get; set; }
        public LoRaMetadata metadata { get; set; }

        public class LoRaMetadata
        {
            public DateTime time { get; set; }
            public decimal frequency { get; set; }
            public string modulation { get; set; }
            public string data_rate { get; set; }
            public string coding_rate { get; set; }

            public LoRaGateway[] gateways { get; set; }
        }

        public class LoRaGateway
        {
            public string gtw_id { get; set; }
            public Int64 timestamp { get; set; }
            public DateTime time { get; set; }
            public int channel { get; set; }
            public int rssi { get; set; }
            public decimal snr { get; set; }
            public decimal altitude { get; set; }
            public decimal longitude { get; set; }
            public decimal latitude { get; set; }
        }
    }

}
