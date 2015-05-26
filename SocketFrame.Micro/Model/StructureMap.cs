using System;
using Kana.Ikimi.SocketFrame.Micro.Exceptions;
using Kana.Ikimi.SocketFrame.Micro.Model.Interfaces;

namespace Kana.Ikimi.SocketFrame.Micro.Model {

    public class StructureMap : IStructureMap {

        private Type _default;
        private Type _using;
        public Type For { get; private set; }
        public Type[] Injecting { get; private set; }

        public Type Using {
            get { return this._using; }
            private set {
                if (value.IsSubclassOf(this.For)) this._using = value;
                else throw new InvalidConfigurationSubTypeException();
            }
        }

        private Type Default {
            get { return this._default; }
            set {
                if (value.IsSubclassOf(this.For)) this._default = value;
                else throw new InvalidConfigurationSubTypeException();
            }
        }

        internal StructureMap(Type type) {
            this.For = type;
        }

        internal StructureMap(Type type, Type def) : this(type) {
            this.Default = def;
        }

        public StructureMap Use(Type mapping) {
            this.Using = mapping;
            return this;
        }

        public StructureMap Inject(params Type[] typeList) {
            this.Injecting = typeList;
            return this;
        }

        public void UseDefault() {
            this.Using = this.Default;
        }

    }

}