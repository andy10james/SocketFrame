using System;
using System.Collections;
using System.Net;
using NETSocketMF.Controllers;

namespace NETSocketMF {
    internal static class SocketsDirector {

        private readonly static Hashtable Servers;

        static SocketsDirector() {
            Servers = new Hashtable();
        }

        public static SocketManager AddSocket(Int16 port, ArrayList types) {
            
        }

        public static SocketManager AddServer(Int16 port, SocketController controller) {
            return AddServer(port, new SocketManager(controller, port));
        }

        public static SocketManager AddServer(Int16 port, SocketManager socket) {
            Servers.Add(port, socket);
            return socket;
        }

        public static void RemoveServer(Int16 port) {
            var server = Servers[port] as SocketManager;
            if (server == null) return;
            if (server.IsAlive) server.Disconnect();
            Servers.Remove(port);
        }

        public static void ConnectAll() {
            foreach (Int16 port in Servers.Keys) {
                var server = Servers[port] as SocketManager;
                if (server != null && !server.IsAlive) {
                    server.Connect();
                }
            }
        }

        public static void Connect(Int16 port) {
            var server = Servers[port] as SocketManager;
            if (server != null) {
                server.Connect();
            }
        }

        public static void DisconnectAll() {
            foreach (Int16 port in Servers.Keys) {
                var server = Servers[port] as SocketManager;
                if (server.IsAlive) {
                    server.Disconnect();
                }
            }
        }

        public static void Disconnect(Int16 port) {
            var server = Servers[port] as SocketManager;
            if (server != null) {
                server.Disconnect();
            }
        }

        public static Int32 DisconnectIP(IPAddress ipaddress) {
            var disconnected = 0;
            foreach (SocketManager server in Servers) {
                if (server.IsAlive) {
                    disconnected += server.DisconnectIP(ipaddress);
                }
            }
            return disconnected;
        }

        public static Boolean Exists(Int16 port) {
           return Servers[port] != null;
        }

        public static Boolean IsConnected(Int16 port) {
            var server = Servers[port] as SocketManager;
            return server != null && server.IsAlive;
        }

        public static Boolean IsPortAvailable(Int16 port) {
            return !Exists(port);
        }

    }
}
