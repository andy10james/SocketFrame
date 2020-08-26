using System.Threading;
using Kana.Ikimi.SocketFrame.Micro.Scaffolding;
using Kana.Ikimi.SocketFrame.Micro.Scaffolding.Interfaces;
using Netduino.Controllers;
using Netduino.Events;

namespace Netduino {

    public class Bootstrap {

        public static void Main() {
            Heartbeat.Start();
            Sw1Events.OnPush(Switch);
            new SocketFrame().Init((b) => {
                b.Bind(typeof (QueryController)).To(2004, 2005);
                b.Bind(typeof (QueryController)).To(2001).Between(2009, 2011);
                b.Bind(typeof (QueryController)).To(2004, 2005);
            }).Init(s => {
                s.For(typeof(IControllerInstanciator)).UseDefault();
            });
            Thread.Sleep(Timeout.Infinite);
        }

        public static void Switch() {
            if (Heartbeat.Beating) Heartbeat.Pause();
            else Heartbeat.Resume();
        }

    }

}