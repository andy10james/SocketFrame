using Kana.Ikimi.SocketFrame.Micro.Scaffolding.Interfaces;

namespace Kana.Ikimi.SocketFrame.Micro.Scaffolding {

    public class SocketFrame : ISocketFrame {


        private static readonly SocketFrame _instance = new SocketFrame();

        public static SocketFrame Instance {
            get { return _instance; }
        }

        public SocketFrame Configure(Configure config) {
            config(new BindingBroker(), new ConfigurationBroker());
            return this;
        }

        public SocketFrame Init(Configure config) {
            this.Configure(config);
            return this.Init();
        }

        public SocketFrame Init() {
            // Start up
            return this;
        }

    }

}