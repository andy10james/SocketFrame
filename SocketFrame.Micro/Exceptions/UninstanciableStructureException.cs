using System;

namespace Kana.Ikimi.SocketFrame.Micro.Exceptions {
    public class UninstanciableStructureException : Exception {

        public UninstanciableStructureException(Type type)
            : base("The structure map with using of type '" + type.Name + "' is not instanciable with the given configuration.") { }

    }
}
