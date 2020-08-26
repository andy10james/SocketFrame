using Kana.Ikimi.SocketFrame.Micro.Delegates;

namespace Kana.Ikimi.SocketFrame.Micro.Scaffolding.Interfaces {

    public interface ISocketFrame {

        SocketFrame Configure(ConfigureBindings config);
        SocketFrame Configure(ConfigureStructure config);
        SocketFrame Configure(Configure config);
        SocketFrame Configure(ConfigureAll config);
        SocketFrame Init(ConfigureBindings config);
        SocketFrame Init(ConfigureStructure config);
        SocketFrame Init(Configure config);
        SocketFrame Init(ConfigureAll config);
        SocketFrame Init();

    }

}