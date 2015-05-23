using Kana.Ikimi.SocketFrame.Micro.Model;
using System;

namespace Kana.Ikimi.SocketFrame.Micro.Scaffolding {
    public class StructureBroker : IStructureBroker {

        private ArrayList _mappings;

        private static readonly StructureBroker _instance = new StructureBroker();

        public static StructureBroker Instance {
            get { return _instance; }
        }

        StructureBroker() {
            _mappings = new ArrayList();
        }

        public StructureMap For(Type type) {
            StructureMap found = this.Find(type);
            if (found != null) return found;
            var mapping = new TypeMap(type);
            _mappings.Add(mapping);
            return mapping;
        }
        
        public StructureMap Find(Type type) {
            foreach (object mapobject in _mappings) {
                StructureMap mapping = mapobject as StructureMap;
                if (mapping == null) continue;
                if (mapping.Type = type) return mapping; 
            }
            return null;
        }

    }
}