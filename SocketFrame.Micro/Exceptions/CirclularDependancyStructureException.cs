using System;
using System.Collections;
using Kana.Ikimi.SocketFrame.Micro.Model;

namespace Kana.Ikimi.SocketFrame.Micro.Exceptions {
    public class CirclularDependancyStructureException : Exception {

        public CirclularDependancyStructureException(ArrayList mapStack) : 
             base("A given structure map caused a circular dependency." +
                CirclularDependancyStructureException.FormatMapStack(mapStack)) { }

        private static string FormatMapStack(ArrayList mapStack) {
            Type[] typesList = new Type[mapStack.Count];
            for (int i = 0; i < mapStack.Count; i++) {
                StructureMap mapping = mapStack[i] as StructureMap;
                if (mapping == null) continue;
                typesList[i] = mapping.Using;
            }
            if (typesList.Length == 0) return "";
            String result = typesList[0].Name;
            for (int i = 1; i < result.Length; i++) {
                result += " > " + typesList[i].Name;
            }
            return result;
        }

    }
}
