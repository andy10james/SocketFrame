using System.Threading;
using Kana.Ikimi.SocketFrame.Micro.Scaffolding;
using Netduino.Controllers;
using Netduino.Events;

namespace Netduino {

    public class Bootstrap {

        public static void Main() {
            Heartbeat.Start();
            Sw1Events.OnPush(Switch);
            new SocketFrame().Configure((c, b, s) => {
                b.Bind(typeof (QueryController)).To(2004, 2005);
                b.Bind(typeof (QueryController)).To(2001).Between(2009, 2011);
                b.Bind(typeof (QueryController)).To(2004, 2005);
            }).Init();
            Thread.Sleep(Timeout.Infinite);
        }

        public static void Switch() {
            if (Heartbeat.Beating) Heartbeat.Pause();
            else Heartbeat.Resume();
        }

    }

}