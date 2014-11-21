using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using Netduino.Controllers;
using Netduino.Events;
using Netduino.Output;
using Netduino.Servers;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace Netduino {
    public class Bootstrap {

        public static void Main() {
            Heartbeat.Start();
            Sw1Events.OnPush(Switch);
            ServersDirector.AddServer(2004, new QueryController()).Connect();
            Thread.Sleep(Timeout.Infinite);
        }

        public static void Switch() {
            if (Heartbeat.Beating) {
                Heartbeat.Pause();
            } else {
                Heartbeat.Resume();
            }
        }

    }
}
