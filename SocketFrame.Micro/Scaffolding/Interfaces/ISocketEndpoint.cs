using System;
using System.Net;

namespace Kana.Ikimi.SocketFrame.Micro.Scaffolding.Interfaces {

    public interface ISocketEndpoint {

        UInt16 Port { get; }
        Boolean IsAlive { get; }

        void Connect();
        void Disconnect();
        Int32 Kill(IPAddress ipaddress);
        Int32 KillAll();

    }

}