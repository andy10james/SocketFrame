using System;
using System.Collections;
using System.Net;
using SocketFrame.Micro.Scaffolding.Interfaces;

namespace SocketFrame.Micro.Scaffolding {
    internal class SocketsBroker : ISocketsBroker {

        private readonly static Hashtable Servers;

        static SocketsBroker() {
            Servers = new Hashtable();
        }

        public static SocketEndpoint AddSocket(UInt16 port) {
            var server = Servers[port] as SocketEndpoint;
            if (server != null) {
                return server;
            }
            server = new SocketEndpoint(port);
            Servers.Add(port, server);
            return server;
        }
        
        public static void ConnectAll() {
            foreach (Int16 port in Servers.Keys) {
                var server = Servers[port] as SocketEndpoint;
                if (server != null && !server.IsAlive) {
                    server.Connect();
                }
            }
        }

        public static void Connect(UInt16 port) {
            var server = Servers[port] as SocketEndpoint;
            if (server != null) {
                server.Connect();
            }
        }

        public static void DisconnectAll() {
            foreach (Int16 port in Servers.Keys) {
                var server = Servers[port] as SocketEndpoint;
                if (server.IsAlive) {
                    server.Disconnect();
                }
            }
        }

        public static void Disconnect(UInt16 port) {
            var server = Servers[port] as SocketEndpoint;
            if (server != null) {
                server.Disconnect();
            }
        }

        public static Int32 DisconnectIP(IPAddress ipaddress) {
            var disconnected = 0;
            foreach (SocketEndpoint server in Servers) {
                if (server.IsAlive) {
                    disconnected += server.DisconnectIP(ipaddress);
                }
            }
            return disconnected;
        }

        public static Boolean Exists(UInt16 port) {
           return Servers[port] != null;
        }

        public static Boolean IsConnected(UInt16 port) {
            var server = Servers[port] as SocketEndpoint;
            return server != null && server.IsAlive;
        }

        public static Boolean IsPortAvailable(UInt16 port) {
            return !Exists(port);
        }

    }
}
