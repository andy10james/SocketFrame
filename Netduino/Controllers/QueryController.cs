using System;
using System.Net.Sockets;
using System.Text;
using 

namespace Netduino.Controllers {
    internal class QueryController : SocketController {

        public QueryController() : base() {
            ActionDictionary["HELLO"] = (ActionDelegate) CheckHashAction;
            RemoteActionDictionary["HELLO"] =  (RemoteActionDelegate) CheckHashAction;
        }

        public override String GetName() {
            return "Query Controller";
        }

        private Boolean CheckHashAction(String[] parameters) {
            return true;
        }

        private Boolean CheckHashAction(String[] parameters, Socket client) {
            client.Send(Encoding.UTF8.GetBytes(messageBytes))
            return true;
        }

        protected override void DefaultAction(CommandPattern command, Socket client) {
        }
        
    }
}
