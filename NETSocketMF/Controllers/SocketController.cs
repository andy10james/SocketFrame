using System;
using System.Collections;
using System.Net.Sockets;

namespace NETSocketMF.Controllers {
    internal abstract class SocketController {

        protected delegate Boolean RemoteActionDelegate(String[] parameters, Socket client = null);
        protected Hashtable RemoteActionDictionary;
        protected Socket Client;

        protected SocketController() {
            RemoteActionDictionary = new Hashtable();
        }

        public Boolean InvokeAction(CommandPattern command, Socket client) {
            var action = RemoteActionDictionary[command.Command] as RemoteActionDelegate;
            if (action != null) {
                action.Invoke(command.Parameters, client);
                return true;
            }
            DefaultAction(command, client);
            return false;
        }

        protected abstract void DefaultAction(CommandPattern command);

    }
}

