using Kana.Ikimi.SocketFrame.Micro.Scaffolding.Interfaces;
using Kana.Ikimi.SocketFrame.Micro.Delegates;

namespace Kana.Ikimi.SocketFrame.Micro.Scaffolding {
    public class SocketFrame : ISocketFrame {

        private static readonly SocketFrame _instance = new SocketFrame();
        private readonly IStructureBroker _structureBroker;

        public static SocketFrame Instance {
            get { return SocketFrame._instance; }
        }

        public SocketFrame() {
            this._structureBroker = new StructureBroker();
        }

        public SocketFrame(IStructureBroker structureBroker) {
            this._structureBroker = structureBroker;
        }

        public SocketFrame Configure(ConfigureBindings config) {
            config(new BindingBroker());
            return this;
        }
        
        public SocketFrame Configure(ConfigureStructure config) {
            config(this._structureBroker);
            return this;
        }
        
        public SocketFrame Configure(Configure config) {
            config(new ConfigurationBroker());
            return this;
        }

        public SocketFrame Configure(ConfigureAll config) {
            config(new ConfigurationBroker(), new BindingBroker(), this._structureBroker);
            return this;
        }

        public SocketFrame Init(ConfigureBindings config) {
            this.Configure(config);
            return this.Init();
        }
        
        public SocketFrame Init(ConfigureStructure config) {
            this.Configure(config);
            return this.Init();
        }
        
        public SocketFrame Init(Configure config) {
            this.Configure(config);
            return this.Init();
        }

        public SocketFrame Init(ConfigureAll config) {
            this.Configure(config);
            return this.Init();
        }

        public SocketFrame Init() {
            this._structureBroker.CheckForCircularDependency();
            this._structureBroker.CheckForUninstanciables();
            // Start up
            return this;
        }

    }
}