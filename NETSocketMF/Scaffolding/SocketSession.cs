using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using SocketFrame.Micro.Model;

namespace SocketFrame.Micro.Scaffolding {
    internal class SocketSession {

        public delegate void OnDeathEvent(SocketSession handle);
        public event OnDeathEvent OnDeath;
        public IPEndPoint RemoteEndPoint;
        public IPEndPoint LocalEndPoint;

        private readonly Socket _client;
        private Boolean _ended = false;

        public SocketSession(Socket client) {
            _client = client;
            RemoteEndPoint = client.RemoteEndPoint as IPEndPoint;
            LocalEndPoint = client.LocalEndPoint as IPEndPoint;
        }

        public void Start() {
            var handler = new Thread(Handle);
            handler.Start();
        }

        private void Handle() {
            
            if (RemoteEndPoint == null) {
                _client.Close();
                return;
            }
            
            while (!_ended) {
                var messageBytes = new Byte[1024];
                var bytesRead = 0;
                try {
                    bytesRead = _client.Receive(messageBytes, 0, messageBytes.Length, SocketFlags.None);
                } catch {
                    break; 
                }
                var message = new string(Encoding.UTF8.GetChars(messageBytes));
                var command = SocketCommand.Create(message);
                _socketController.InvokeAction(command, _client);
            }

            _client.Close();
            OnDeath.Invoke(this);

        }

        public void Die() {
            _ended = true;
            _client.Close();
        }

    }
}
