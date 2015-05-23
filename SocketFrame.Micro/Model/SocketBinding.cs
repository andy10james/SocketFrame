using System;
using System.Collections;
using Kana.Ikimi.SocketFrame.Micro.Exceptions;
using Kana.Ikimi.SocketFrame.Micro.Scaffolding.Interfaces;

namespace Kana.Ikimi.SocketFrame.Micro.Model {

    public class SocketBinding {

        private Type _controller;
        public Type Controller {
            get { return this._controller; }
            set {
                if (value.IsSubclassOf(typeof(ISocketController))) {
                    this._controller = value;
                } else {
                    throw new InvalidSocketControllerException();
                }
            }
        }

        public ArrayList Sockets;
        private Boolean _allSockets;
        public Boolean AllSockets {
            get { return this._allSockets && this.Sockets.Count == 0; }
            set {
                if (value) {
                    this.Sockets = new ArrayList();
                    this._allSockets = true;
                } else {
                    this._allSockets = false;
                }
            }
        }

        public UInt32 Backlog { get; private set; }

        public SocketBinding(Type controller) {
            this.Controller = controller;
            this.Backlog = 3;
        }

        public SocketBinding To(params UInt16[] bindings) {
            for (var i = 0; i < bindings.Length; i++)
                this.Sockets.Add(bindings[i]);
            return this;
        }

        public SocketBinding Between(UInt16 minimum, UInt16 maximum) {
            for (var i = minimum; i <= maximum; i++)
                this.Sockets.Add(i);
            return this;
        }

        public SocketBinding ToAll() {
            this.AllSockets = true;
            return this;
        }

        public SocketBinding WithBacklogOf(UInt16 backlog) {
            if (backlog < 1) backlog = 1;
            this.Backlog = backlog;
            return this;
        }

    }

}