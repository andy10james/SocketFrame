using System;

public class StructureMap {
	
	public Type For { get; private set; }
	private Type _using;
	public Type Using { 
		get { return _using; }
		private set {
			if (value.IsSubclassOf(this.For)) {
	            this._using = value;
	        } else {
	            throw new InvalidConfigurationSubTypeException();
	        }
    	}
	}
	private Type _default {
		private set {
			if (value.IsSubclassOf(this.For)) {
	            this._using = value;
	        } else {
	            throw new InvalidConfigurationSubTypeException();
	        }
		}
	};
	
	StructureMap(Type type) {
		this.For = type;
	}
	
	internal StructureMap(Type type, Type def) : (type) {
		this.Using = this.Default = def;
	}
	
	public void Use(Type mapping) {
		this.Using = mapping;
	}
	
	public void UseDefault() {
		this.Using = _Default;
	}
	
}