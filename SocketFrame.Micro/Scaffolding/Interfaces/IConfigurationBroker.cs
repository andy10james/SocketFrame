using System;

namespace Kana.Ikimi.SocketFrame.Micro.Scaffolding.Interfaces {

    public interface IConfigurationBroker {

        void SetBacklog(Int16 backlog);
        void SetSessionTimeout(Int64 timeout);
        void SetMaxCommandLength(Int64 maxLength);
        Int16 GetBacklog();
        Int64 GetSessionTimeout();
        Int64 GetMaxCommandLength();

    }

}