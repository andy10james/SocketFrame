using System;
using System.Collections;
using Microsoft.SPOT;

namespace NETSocketMF.Controllers {

    internal class SocketControllerIndex {

        public IEnumerable GetControllersForSocket(Int16 port) {
            var types = new ArrayList();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) {
                foreach (var type in assembly.GetTypes()) {
                    yield return type;
                }
            }
        }

    }
}
