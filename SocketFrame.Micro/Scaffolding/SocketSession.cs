using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Kana.Ikimi.SocketFrame.Micro.Model;

namespace Kana.Ikimi.SocketFrame.Micro.Scaffolding {
    internal class SocketSession {

        public delegate void OnDeathEvent(SocketSession handle);
        public event OnDeathEvent OnDeath;
        public IPEndPoint RemoteEndPoint;
        public IPEndPoint LocalEndPoint;

        private readonly Socket _client;
        private Boolean _ended = false;

        public SocketSession(Socket client) {
            this._client = client;
            this.RemoteEndPoint = client.RemoteEndPoint as IPEndPoint;
            this.LocalEndPoint = client.LocalEndPoint as IPEndPoint;
        }

        public void Start() {
            var handler = new Thread(this.Handle);
            handler.Start();
        }

        private void Handle() {
            
            if (this.RemoteEndPoint == null) {
                this._client.Close();
                return;
            }
            
            while (!this._ended) {
                var messageBytes = new Byte[1024];
                var bytesRead = 0;
                try {
                    bytesRead = this._client.Receive(messageBytes, 0, messageBytes.Length, SocketFlags.None);
                } catch {
                    break; 
                }
                var message = new string(Encoding.UTF8.GetChars(messageBytes));
                var command = SocketCommand.Create(message);
                _socketController.InvokeAction(command, this._client);
            }

            this._client.Close();
            this.OnDeath.Invoke(this);

        }

        public void Die() {
            this._ended = true;
            this._client.Close();
        }

    }
}
