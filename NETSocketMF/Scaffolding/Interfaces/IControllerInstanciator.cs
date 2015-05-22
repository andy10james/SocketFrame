using System;

namespace SocketFrame.Micro.Scaffolding.Interfaces {
    public interface IControllerInstanciator {

        ISocketController InstanciateFromType(Type type);

    }
}
