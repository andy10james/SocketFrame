using Kana.Ikimi.SocketFrame.Micro.Scaffolding.Interfaces;
using Kana.Ikimi.SocketFrame.Micro.Delegates;

namespace Kana.Ikimi.SocketFrame.Micro.Scaffolding {
    public class SocketFrame : ISocketFrame {

        private static readonly SocketFrame _instance = new SocketFrame();

        public static SocketFrame Instance {
            get { return _instance; }
        }

        public SocketFrame Binding(ConfigureBindings config) {
            config(new BindingBroker());
            return this;
        }
        
        public SocketFrame Structure(ConfigureStructure config) {
            config(new StructureBroker())
            return this;
        }
        
        public SocketFrame Configure(Configure config) {
            config(new ConfigurationBroker());
            return this;
        }

        public SocketFrame Configure(ConfigureAll config) {
            config(new ConfigurationBroker(), new BindingBroker(), new StructureBroker());
        }

        public SocketFrame Init(ConfigureBindings config) {
            this.Binding(config);
            return this.Init();
        }
        
        public SocketFrame Init(ConfigureStructure config) {
            this.Structure(config);
            return this.Init()
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