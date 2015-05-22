using System;
using System.Collections;
using SocketFrame.Micro.Model;
using SocketFrame.Micro.Scaffolding.Interfaces;

namespace SocketFrame.Micro.Scaffolding {
    public class SocketFrame : ISocketFrame {

        private readonly static ArrayList Bindings;

        static SocketFrame() {
            Bindings = new ArrayList();
        }

        public SocketBinding Bind(Type controller) {
            var binding = new SocketBinding(controller);
            Bindings.Add(binding);
            return binding;
        }

        public IEnumerable GetControllersForSocket(UInt16 port) {
            foreach (var bindobject in Bindings) {
                var binding = bindobject as SocketBinding;
                if (binding == null) continue;
                if (binding.AllSockets || binding.Sockets.Contains(port))
                    yield return binding.Controller;
            }
        }

        public static void Init(UInt16 socket) {
            SocketsBroker.AddSocket(socket).Connect();
        }

    }
}
