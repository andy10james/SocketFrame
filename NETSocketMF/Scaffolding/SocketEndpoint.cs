using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SocketFrame.Micro.Scaffolding {
    internal class SocketEndpoint {

        public UInt16 Port { get { return _port; } }
        public ArrayList Handles { get { return _handles; } }
        public Boolean IsAlive {
            get { return (_listenerThread != null && _listenerThread.IsAlive); }
        }

        private readonly UInt16 _port;
        private readonly ArrayList _handles = new ArrayList();
        private Socket _socket;
        private Thread _listenerThread;

        public SocketEndpoint(UInt16 port) {
            _port = port;
        }

        public void Connect() {
            if (!IsAlive) {
                _listenerThread = new Thread(Listen);
                _listenerThread.Start();
            }
        }

        public void Disconnect() {
            if (IsAlive) {
                foreach (SocketSession handle in _handles) {
                    handle.Die();
                }
                _socket.Close();
            }
        }


        public Int32 DisconnectIP(IPAddress ipaddress) {
            Int32 disconnected = 0;
            foreach (SocketSession handle in _handles) {
                if (handle.RemoteEndPoint.Address.Equals(ipaddress)) {
                    handle.Die();
                    disconnected++;
                }
            }
            return disconnected;
        }
        
        private void Listen() {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Bind(new IPEndPoint(IPAddress.Any, _port));
            _socket.Listen(3);
            while (true) {
                Socket client;
                try {
                    client = _socket.Accept();
                } catch (Exception e) {
                    continue;
                }
                var handle = new SocketSession(client);
                handle.OnDeath += e => _handles.Remove(e);
                handle.Start();
                _handles.Add(handle);
            }
        }

    }
}
