namespace Kana.Ikimi.SocketFrame.Micro.Scaffolding.Interfaces {
    public interface ICommandBroker {

        void Handle(ISocketSession session, string command);

    }
}
