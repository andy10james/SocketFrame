using System;
using SocketFrame.Micro.Model;

namespace SocketFrame.Micro.Scaffolding.Interfaces {
    public interface ISocketFrame {

        SocketBinding Bind(Type controller);



    }
}
