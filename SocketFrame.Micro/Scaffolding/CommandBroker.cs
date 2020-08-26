using Kana.Ikimi.SocketFrame.Micro.Model;
using Kana.Ikimi.SocketFrame.Micro.Scaffolding.Interfaces;

namespace Kana.Ikimi.SocketFrame.Micro.Scaffolding {
    internal class CommandBroker : ICommandBroker {

        public void Handle(ISocketSession session, string command) {
            SocketCommand.Create(command);
        }

    }
}
