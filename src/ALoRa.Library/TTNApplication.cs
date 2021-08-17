using System;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace ALoRa.Library
{
    public class TTNApplication : BaseObject
    {
        private const string BROKER_URL_FILTER = "{0}.thethings.network";

        private string m_clientId;
        private string m_appId;
        private MqttClient m_client;
        private Action<TTNMessage> m_msgReceived;

        public event Action<TTNMessage> MessageReceived
        {
            add { m_msgReceived += value; }
            remove { m_msgReceived -= value; }
        }

        public string AppID
        {
            get { return m_appId; }
        }

        public bool IsConnected
        {
            get { return m_client.IsConnected; }
        }

        public TTNApplication(string appId, string accessKey, string region)
        {
            m_appId = appId;
            m_clientId = Guid.NewGuid().ToString();

            m_client = new MqttClient(string.Format(BROKER_URL_FILTER, region));
            m_client.MqttMsgPublishReceived += M_client_MqttMsgPublishReceived;
            m_client.ConnectionClosed += M_client_ConnectionClosed;
            m_client.MqttMsgSubscribed += M_client_MqttMsgSubscribed;
            m_client.MqttMsgUnsubscribed += M_client_MqttMsgUnsubscribed;

            m_client.Connect(m_clientId, m_appId, accessKey);

            m_client.Subscribe( new string[] { "+/devices/+/up" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
        }

        protected override void Dispose(bool disposing)
        {
            m_client.Disconnect();
        }

        public void Publish()
        {
            //m_client.Publish()
        }

        private void M_client_MqttMsgPublishReceived(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs e)
        {
            try
            {
                var msg = TTNMessage.DeserialiseMessage(e);

                if (m_msgReceived != null)
                {
                    m_msgReceived(msg);
                }
            }
            catch
            {
                // Swallow any exceptions during message receive
            }
        }

        private void M_client_MqttMsgUnsubscribed(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgUnsubscribedEventArgs e)
        {
        }

        private void M_client_MqttMsgSubscribed(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgSubscribedEventArgs e)
        {
        }

        private void M_client_ConnectionClosed(object sender, EventArgs e)
        {
        }
    }
}
