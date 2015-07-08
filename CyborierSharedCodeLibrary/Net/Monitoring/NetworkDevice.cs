using System;
using System.Net;
using System.Net.NetworkInformation;
using log4net;
using LogManager = Cyborier.Shared.ThirdParty.Log4Net.LogManager;

namespace Cyborier.Shared.Net.Monitoring
{
    public class NetworkDevice
    {
        #region Logging Stuffs

        private readonly ILog logger = LogManager.Getlogger();

        #endregion

        #region variables

        #endregion

        #region Constructor

        /// <summary>
        ///     Create new Instance of Network Device.
        /// </summary>
        /// <param name="ipaddress">IP address of Network Device. E.g. 172.21.110.221</param>
        /// <param name="deviceName">name of device e.g. Plaza1Server</param>
        /// <param name="pingTimeout">Time out for the Ping Comman</param>
        public NetworkDevice(string ipaddress, string deviceName, TimeSpan deveiceThreashold, int placeID,
            int pingTimeout = 5000)
        {
            IPAddress = IPAddress.Parse(ipaddress);
            DeviceName = deviceName;
            DeviceThreshold = deveiceThreashold;
            PingTimeOut = pingTimeout;
            Place_ID = placeID;
            DeviceStatus = IPStatus.Unknown;
        }

        #endregion

        #region Properties

        public IPAddress IPAddress { get; set; }

        public string DeviceName { get; set; }

        public IPStatus DeviceStatus { get; set; }

        public DateTime LastStatusChangeDateTime { get; set; }

        public IPStatus DeviceLastStatus { get; set; }

        public TimeSpan DeviceThreshold { get; set; }

        public int Place_ID { get; set; }

        public int PingTimeOut { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Ping the Device to Get the Device Status.
        /// </summary>
        /// <returns>
        ///     returns true if status is changed.
        /// </returns>
        public bool CheckForStatusChange()
        {
            var p = new Ping();
            PingReply reply = p.Send(IPAddress, PingTimeOut);

            if (DeviceStatus != reply.Status)
            {
                DeviceLastStatus = DeviceStatus;
                DeviceStatus = reply.Status;
                LastStatusChangeDateTime = DateTime.Now;
                return true;
            }
            return false;
        }

        #endregion
    }
}