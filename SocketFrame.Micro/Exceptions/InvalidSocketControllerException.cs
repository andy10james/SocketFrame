using System;

namespace Kana.Ikimi.SocketFrame.Micro.Exceptions {
    public class InvalidSocketControllerException : Exception {

        public InvalidSocketControllerException() : base("The type given is a valid subtype of SocketController.") { }

    }
}
