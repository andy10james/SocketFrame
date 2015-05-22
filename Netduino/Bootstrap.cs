using System.Threading;
using Netduino.Controllers;
using Netduino.Events;
using NETSocketMF;

namespace Netduino {
    public class Bootstrap {

        public static void Main() {
            Heartbeat.Start();
            Sw1Events.OnPush(Switch);
            var s = new NETSocket();
            s.Bind(typeof(QueryController)).To(2004, 2005);
            s.Bind(typeof(QueryController)).To(2001).Between(2009, 2011);
            s.Bind(typeof(QueryController)).To(2004, 2005);
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
