using System;
using System.Collections;
using System.Reflection;
using Kana.Ikimi.SocketFrame.Micro.Exceptions;
using Kana.Ikimi.SocketFrame.Micro.Model;
using Kana.Ikimi.SocketFrame.Micro.Model.Interfaces;
using Kana.Ikimi.SocketFrame.Micro.Scaffolding.Interfaces;
using Microsoft.SPOT;

namespace Kana.Ikimi.SocketFrame.Micro.Scaffolding {

    public class StructureBroker : IStructureBroker {

        private static readonly StructureBroker _instance = new StructureBroker();
        private readonly ArrayList _mappings;

        public static StructureBroker Instance {
            get { return _instance; }
        }

        public StructureBroker() {
            this._mappings = new ArrayList();
        }

        public IStructureMap For(Type type) {
            IStructureMap found = this.Find(type);
            if (found != null) return found;
            StructureMap mapping = new StructureMap(type);
            this._mappings.Add(mapping);
            return mapping;
        }

        public IStructureMap Find(Type type) {
            foreach (object mapobject in this._mappings) {
                StructureMap mapping = mapobject as StructureMap;
                if (mapping == null) continue;
                if (mapping.For == type) return mapping;
            }
            return null;
        }

        public void CheckForUninstanciables() {
            foreach (Object mappingobj in _mappings) {
                IStructureMap mapping = mappingobj as IStructureMap;
                if (mapping == null) continue;
                ConstructorInfo constructor = null;
                if (mapping.Injecting.Length > 0)
                    constructor = mapping.Using.GetConstructor(mapping.Injecting);
                if (constructor == null)
                    constructor = mapping.Using.GetConstructor(new[] { typeof(IStructureBroker) });
                if (constructor == null)
                    constructor = mapping.Using.GetConstructor(new Type[0]);
                if (constructor == null)
                    throw new UninstanciableStructureException(mapping.Using);
            }
        }

        public void CheckForCircularDependencies() {
            foreach (Object mappingObj in _mappings) {
                IStructureMap mapping = mappingObj as IStructureMap;
                if (mapping == null) continue;
                this.CheckForCircularDependencies(mapping, new ArrayList());
            }
        }

        private void CheckForCircularDependencies(IStructureMap map, ArrayList upperMapStack) {
            ArrayList mapStack = upperMapStack.Clone() as ArrayList;
            if (mapStack == null) return;
            if (mapStack.Contains(map)) {
                mapStack.Add(map);
                throw new CirclularDependancyStructureException(mapStack);
            } 
            mapStack.Add(map);
            foreach (Type type in map.Injecting) {
                IStructureMap mapping = this.Find(type);
                if (mapping == null) continue;
                this.CheckForCircularDependencies(mapping, mapStack);
            }
        }

        public Object InstanciateFor(Type type) {
            IStructureMap mapping = this.Find(type);
            if (mapping == null) return null;
            ConstructorInfo constructor = null;
            if (mapping.Injecting.Length > 0) {
                constructor = mapping.Using.GetConstructor(mapping.Injecting);
                if (constructor != null) constructor.Invoke(this.InstanciateFor(mapping.Injecting));
            }
            if (constructor == null)
                constructor = mapping.Using.GetConstructor(new[] { typeof (IStructureBroker) });
            if (constructor == null) {
                constructor = mapping.Using.GetConstructor(new Type[0]);
            }
            return constructor.Invoke(new object[0]);
        }

        public Object[] InstanciateFor(Type[] types) {
            ArrayList objects = new ArrayList();
            foreach (Type type in types)
                objects.Add(this.InstanciateFor(type));
            return objects.ToArray();
        }

    }

}