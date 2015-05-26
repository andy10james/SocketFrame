using System;
using System.Collections;
using System.Net;
using Kana.Ikimi.SocketFrame.Micro.Scaffolding.Interfaces;

namespace Kana.Ikimi.SocketFrame.Micro.Scaffolding {
    internal class SocketsBroker : ISocketsBroker {

        private static readonly SocketsBroker _instance = new SocketsBroker();
        private readonly Hashtable _endpoints;

        public static SocketsBroker Instance {
            get { return _instance; }
        }

        public SocketsBroker() {
            this._endpoints = new Hashtable();
        }

        public SocketEndpoint AddSocket(UInt16 port) {
            var server = this._endpoints[port] as SocketEndpoint;
            if (server != null) {
                return server;
            }
            server = new SocketEndpoint(port);
            this._endpoints.Add(port, server);
            return server;
        }
        
        public void ConnectAll() {
            foreach (Int16 port in this._endpoints.Keys) {
                var server = this._endpoints[port] as SocketEndpoint;
                if (server != null && !server.IsAlive) {
                    server.Connect();
                }
            }
        }

        public void Connect(UInt16 port) {
            var server = this._endpoints[port] as SocketEndpoint;
            if (server != null) {
                server.Connect();
            }
        }

        public void DisconnectAll() {
            foreach (Int16 port in this._endpoints.Keys) {
                var server = this._endpoints[port] as SocketEndpoint;
                if (server.IsAlive) {
                    server.Disconnect();
                }
            }
        }

        public void Disconnect(UInt16 port) {
            var server = this._endpoints[port] as SocketEndpoint;
            if (server != null) {
                server.Disconnect();
            }
        }

        public Int32 Kill(IPAddress ipaddress) {
            var disconnected = 0;
            foreach (SocketEndpoint server in this._endpoints) {
                if (server.IsAlive) {
                    disconnected += server.Kill(ipaddress);
                }
            }
            return disconnected;
        }
        public Int32 KillAll() {
            var disconnected = 0;
            foreach (SocketEndpoint server in this._endpoints) {
                if (server.IsAlive) {
                    disconnected += server.KillAll();
                }
            }
            return disconnected;
        }

        public Boolean Exists(UInt16 port) {
           return this._endpoints[port] != null;
        }

        public Boolean IsConnected(UInt16 port) {
            var server = this._endpoints[port] as SocketEndpoint;
            return server != null && server.IsAlive;
        }

        public Boolean IsPortAvailable(UInt16 port) {
            return !this.Exists(port);
        }

    }
}
