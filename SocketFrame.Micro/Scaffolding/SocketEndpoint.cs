using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Kana.Ikimi.SocketFrame.Micro.Scaffolding.Interfaces;

namespace Kana.Ikimi.SocketFrame.Micro.Scaffolding {

    internal class SocketEndpoint : ISocketEndpoint {

        private readonly ArrayList _handles = new ArrayList();
        private readonly UInt16 _port;
        private Thread _listenerThread;
        private Socket _socket;

        public SocketEndpoint(UInt16 port) {
            this._port = port;
        }

        public UInt16 Port {
            get { return this._port; }
        }

        public Boolean IsAlive {
            get { return (this._listenerThread != null && this._listenerThread.IsAlive); }
        }

        public void Connect() {
            if (!this.IsAlive) {
                this._listenerThread = new Thread(this.Listen);
                this._listenerThread.Start();
            }
        }

        public void Disconnect() {
            if (this.IsAlive) {
                foreach (SocketSession handle in this._handles) handle.Die();
                this._socket.Close();
            }
        }

        public Int32 Kill(IPAddress ipaddress) {
            Int32 disconnected = 0;
            foreach (SocketSession handle in this._handles)
                if (handle.RemoteEndPoint.Address.Equals(ipaddress)) {
                    handle.Die();
                    disconnected++;
                }
            return disconnected;
        }

        public Int32 KillAll() {
            Int32 disconnected = 0;
            foreach (SocketSession handle in this._handles) {
                handle.Die();
                disconnected++;
            }
            return disconnected;
        }

        private void Listen() {
            this._socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this._socket.Bind(new IPEndPoint(IPAddress.Any, this._port));
            this._socket.Listen(3);
            while (true) {
                Socket client;
                try {
                    client = this._socket.Accept();
                }
                catch (Exception e) {
                    continue;
                }
                SocketSession handle = new SocketSession(client);
                handle.OnDeath(e => this._handles.Remove(e));
                handle.Start();
                this._handles.Add(handle);
            }
        }

    }

}