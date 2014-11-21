using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using NETSocketMF.Commands;
using NETSocketMF.Controllers;

namespace NETSocketMF {
    internal class ServerHandle {

        public delegate void OnDeathEvent(ServerHandle handle);
        public event OnDeathEvent OnDeath;
        public IPEndPoint EndPoint;

        private readonly Type _socketController;
        private readonly Socket _client;
        private Boolean _ended = false;

        public ServerHandle(SocketController socketController, Socket client) {
            _socketController = socketController;
            _client = client;
            EndPoint = client.RemoteEndPoint as IPEndPoint;
        }

        public void Start() {
            var handler = new Thread(Handle);
            handler.Start();
        }

        private void Handle() {
            _socketController.GetConstructor(new Type[0]).Invoke(new Object[0]);
            if (EndPoint == null) {
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
                var command = CommandPattern.Create(message);
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
