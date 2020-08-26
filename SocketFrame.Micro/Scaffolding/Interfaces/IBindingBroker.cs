using System;
using System.Collections;
using Kana.Ikimi.SocketFrame.Micro.Model;

namespace Kana.Ikimi.SocketFrame.Micro.Scaffolding.Interfaces {

    public interface IBindingBroker {

        SocketBinding Bind(Type controller);
        SocketBinding Find(Type controller);
        IEnumerable GetControllersFor(UInt16 port);
        IEnumerable GetPortsFor(Type controller);
        IEnumerable GetPortsFor(ISocketController controller);

    }

}