using System;
using System.Net;

namespace Kana.Ikimi.SocketFrame.Micro.Scaffolding.Interfaces {

    internal interface ISocketsBroker {

        SocketEndpoint AddSocket(UInt16 port);

        void ConnectAll();

        void Connect(UInt16 port);

        void DisconnectAll();

        void Disconnect(UInt16 port);

        Int32 Kill(IPAddress ipaddress);

        Int32 KillAll();

        Boolean Exists(UInt16 port);

        Boolean IsConnected(UInt16 port);

        Boolean IsPortAvailable(UInt16 port);

    }

}