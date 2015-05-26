using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Kana.Ikimi.SocketFrame.Micro.Delegates;
using Kana.Ikimi.SocketFrame.Micro.Scaffolding.Interfaces;

namespace Kana.Ikimi.SocketFrame.Micro.Scaffolding {

    internal class SocketSession : ISocketSession {

        private event OnSocketSessionDeath _onDeath;
        private readonly Socket _client;
        private Boolean _ended;

        public IPEndPoint LocalEndPoint;
        public IPEndPoint RemoteEndPoint;

        public SocketSession(Socket client) {
            this._client = client;
            this.RemoteEndPoint = client.RemoteEndPoint as IPEndPoint;
            this.LocalEndPoint = client.LocalEndPoint as IPEndPoint;
        }

        public void Start() {
            Thread handler = new Thread(this.Handle);
            handler.Start();
        }

        public void Send(String message) {
            this._client.Send(Encoding.UTF8.GetBytes(message));
        }

        public void OnDeath(OnSocketSessionDeath listener) {
            this._onDeath += listener;
        }

        public void Die() {
            this._ended = true;
            this._client.Close();
            this._onDeath.Invoke(this);
        }

        private void Handle() {
            if (this.RemoteEndPoint == null) {
                this._client.Close();
                return;
            }
            while (!this._ended) {
                Byte[] messageBytes = new Byte[1024];
                Int32 bytesRead = 0;
                try {
                    bytesRead = this._client.Receive(messageBytes, 0, messageBytes.Length, SocketFlags.None);
                } catch {
                    break;
                }
                String message = new string(Encoding.UTF8.GetChars(messageBytes));
            }
            this._client.Close();
            this._onDeath.Invoke(this);
        }

    }

}