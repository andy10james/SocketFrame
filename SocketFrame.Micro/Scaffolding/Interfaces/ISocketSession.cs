using System;
using Kana.Ikimi.SocketFrame.Micro.Delegates;

namespace Kana.Ikimi.SocketFrame.Micro.Scaffolding.Interfaces {

    public interface ISocketSession {

        void Start();

        void Die();

        void Send(String message);

        void OnDeath(OnSocketSessionDeath listener);

    }

}