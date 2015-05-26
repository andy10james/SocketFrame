using System;
using System.Collections;
using Kana.Ikimi.SocketFrame.Micro.Model;
using Kana.Ikimi.SocketFrame.Micro.Scaffolding.Interfaces;

namespace Kana.Ikimi.SocketFrame.Micro.Scaffolding {

    public class BindingBroker : IBindingBroker {

        private static readonly BindingBroker _instance = new BindingBroker();
        private readonly ArrayList _bindings;

        public static BindingBroker Instance {
            get { return _instance; }
        }

        public SocketBinding Bind(Type controller) {
            SocketBinding found = this.Find(controller);
            if (found != null) return found;
            SocketBinding binding = new SocketBinding(controller);
            this._bindings.Add(binding);
            return binding;
        }
        
        public SocketBinding Find(Type controller) {
            foreach (object bindobject in _bindings) {
                SocketBinding binding = bindobject as SocketBinding;
                if (binding == null) continue;
                if (binding.Controller == controller) return binding;
            }
            return null;
        }

        public IEnumerable GetControllersFor(UInt16 port) {
            foreach (object bindobject in this._bindings) {
                SocketBinding binding = bindobject as SocketBinding;
                if (binding == null) continue;
                if (binding.AllSockets || binding.Sockets.Contains(port))
                    yield return binding.Controller;
            }
        }

        public IEnumerable GetPortsFor(Type controller) {
            ArrayList portsYielded = new ArrayList();
            foreach (object bindobject in this._bindings) {
                SocketBinding binding = bindobject as SocketBinding;
                if (binding == null) continue;
                if (binding.Controller == controller) {
                    if (binding.AllSockets) {
                        yield return -1;
                        break;
                    }
                    foreach (object portobject in binding.Sockets) {
                        if (!(portobject is UInt16)) continue;
                        UInt16 port = (UInt16) portobject;
                        if (portsYielded.Contains(port)) continue;
                        portsYielded.Add(port);
                        yield return port;
                    }
                }
            }
        }

        public IEnumerable GetPortsFor(ISocketController controller) {
            return this.GetPortsFor(controller.GetType());
        }

    }

}