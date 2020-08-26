using Kana.Ikimi.SocketFrame.Micro.Scaffolding;
using Kana.Ikimi.SocketFrame.Micro.Scaffolding.Interfaces;

namespace Kana.Ikimi.SocketFrame.Micro.Delegates {

    public delegate void ConfigureAll(IConfigurationBroker configurationBroker, IBindingBroker bindingBroker, IStructureBroker structureBroker);

}