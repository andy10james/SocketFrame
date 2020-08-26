using System;

namespace Kana.Ikimi.SocketFrame.Micro.Scaffolding.Interfaces {
    public interface IControllerInstanciator {

        ISocketController InstanciateFromType(Type type);

    }
}
