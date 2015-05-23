using System;

namespace Kana.Ikimi.SocketFrame.Micro.Exceptions {
    public class InvalidConfigurationSubTypeException : Exception {

        public InvalidConfigurationSubTypeException() : base("The type given is not a valid subtype for the configuration.") { }

    }
}
