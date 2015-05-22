using System;
using System.Collections;
using SocketFrame.Micro.Exceptions;
using SocketFrame.Micro.Scaffolding.Interfaces;

namespace SocketFrame.Micro.Model {

    public class SocketBinding {

        private Type _controller;
        public Type Controller {
            get { return _controller; }
            set {
                if (value.IsSubclassOf(typeof(ISocketController))) {
                    _controller = value;
                } else {
                    throw new InvalidSocketControllerException();
                }
            }
        }

        public ArrayList Sockets;
        private Boolean _allSockets;
        public Boolean AllSockets {
            get { return _allSockets && Sockets.Count == 0; }
            set {
                if (value) {
                    Sockets = new ArrayList();
                    _allSockets = true;
                } else {
                    _allSockets = false;
                }
            }
        }

        public UInt32 Backlog { get; private set; }

        public SocketBinding(Type controller) {
            Controller = controller;
            Backlog = 3;
        }

        public SocketBinding To(params UInt16[] bindings) {
            for (var i = 0; i < bindings.Length; i++)
                Sockets.Add(bindings[i]);
            return this;
        }

        public SocketBinding Between(UInt16 minimum, UInt16 maximum) {
            for (var i = minimum; i <= maximum; i++)
                Sockets.Add(i);
            return this;
        }

        public SocketBinding ToAll() {
            AllSockets = true;
            return this;
        }

        public SocketBinding WithBacklogOf(UInt16 backlog) {
            if (backlog < 1) backlog = 1;
            Backlog = backlog;
            return this;
        }

    }

}